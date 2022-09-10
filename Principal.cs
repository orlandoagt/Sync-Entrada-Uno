using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Threading;
using RestSharp;
using System.Configuration;



namespace Sync_Entrada_Uno
{
    public partial class Principal : Form
    {
        static string ConexionString = ConfigurationManager.AppSettings["_ConexionString"];
        Int32 _CommandTimeOut = Convert.ToInt32( ConfigurationManager.AppSettings["commandTimeOut"]);
        int _AddMiliseconds = int.Parse(ConfigurationManager.AppSettings["AddMiliseconds"]);
        Int32 TimerDelay = Convert.ToInt32(ConfigurationManager.AppSettings["_TimerDelay"]);
        SqlConnection conexion = new SqlConnection(ConexionString);
        
        string EventoCodigo;
        string EventoNombre;
        int CantTicketsUtilizados;

        bool Boton_Descargar = false;
        bool Boton_Reportar = false;

        public Principal()
        {
            InitializeComponent();
            
        }
       
        private  void Principal_Load(object sender, EventArgs e)
        {
            ConsultaEventos();
            cbox_evento.SelectedIndex = -1;
            btn_Descargar.Enabled = false;
            btn_reportar.Enabled = false;
            lbl_estado.Visible = false;
            lbl_estado2.Visible = false;
            timer1.Interval = TimerDelay;
            timer2.Interval = TimerDelay;

        }


        private void ConsultaEventos()
        {
            try
            {
                conexion.Open();
                string QueryConsulta = "select codigo, nombre from evento where eliminado ='0' order by nombre";
                SqlCommand comando = new SqlCommand(QueryConsulta, conexion);
                SqlDataAdapter data = new SqlDataAdapter(comando);

                DataTable Tabla = new DataTable();
                data.Fill(Tabla);
                conexion.Close();

                List<Evento> oEvento = new List<Evento>();

                for (int i = 0; i < Tabla.Rows.Count; i++)
                {
                    oEvento.Add(new Evento
                    {
                        EventoCodigo = Tabla.Rows[i]["codigo"].ToString(),
                        EventoNombre = Tabla.Rows[i]["nombre"].ToString(),
                    
                    });
                   
                }
                cbox_evento.DataSource = oEvento;
                cbox_evento.DisplayMember = "EventoNombre";
                cbox_evento.ValueMember = "EventoCodigo";

            }
            catch (Exception ex)
            {
                conexion.Close();
                MessageBox.Show(ex.Message);

            }

            
        }

        public static void MyDelay(int ms)
        {

            //int milisegundosdelay = int.Parse(ConfigurationManager.AppSettings["miliSegundosdelay"]);

            DateTime dt = DateTime.Now + TimeSpan.FromMilliseconds(ms);

            do
            {
                Application.DoEvents();

            } while (DateTime.Now < dt);
        }

        private void cbox_evento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbox_evento.SelectedIndex>=0)
            {
                EventoCodigo = cbox_evento.SelectedValue.ToString();
                EventoNombre = cbox_evento.Text.ToString();
                btn_Descargar.Enabled = true;
                btn_reportar.Enabled = true;
            }
            
        }

        private void Descargar()
        {
           

            timer1.Stop();
            timer1.Enabled = false;
            btn_Descargar.Enabled = false;
            cbox_evento.Enabled = false;
            txt_apikey.Enabled = false;
            txt_url.Enabled = false;

            string estado;
            string fechallamada = DateTime.Now.ToString("yyyyMMddHHmmss");
            //DateTime _FechaConsulta;
            string FechaConsulta;

            try
            {
                //Consulta Last Update

                conexion.Open();
                SqlCommand Comando = new SqlCommand();
                Comando.Connection = conexion;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "Sp_consultalastupdateEntradaUno";
                Comando.Parameters.Add(new SqlParameter("@apikey", SqlDbType.VarChar, 50)).Value = txt_apikey.Text.Trim();
                Comando.Parameters.Add(new SqlParameter("@Evento", SqlDbType.VarChar, 50)).Value = EventoCodigo;
                var a= Comando.ExecuteScalar();
                conexion.Close();


                DateTime b = Convert.ToDateTime(a);
                b = b.AddMilliseconds(_AddMiliseconds);
                FechaConsulta = b.ToString("yyyyMMddHHmmssfff");
                //FechaConsulta = FechaConsulta.PadRight(17, '0');

                lbl_estado.Visible = true;
                lbl_estado.Text = "Solicitando Tickets...";
                MyDelay(1000);
                string url = txt_url.Text.Trim().Replace("cFechaDesde", FechaConsulta);
                var client = new RestClient(url);
                client.Timeout = -1;
                client.FollowRedirects = false;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", txt_apikey.Text.Trim());
                IRestResponse response = client.Execute(request);

                
                List<Ticket> ticketsList = JsonConvert.DeserializeObject<List<Ticket>>(response.Content.ToString());

                if (ticketsList == null)
                {
                    btn_Descargar.Enabled = true;
                    lbl_estado.Visible = false;
                    lbl_estado2.Visible = false;
                    MessageBox.Show("Evento Vacio o Inexistente");
                    return;

                }

                if (ticketsList.Count == 0)
                {
                    progressBar1.Value = 0;
                    lbl_estado.Visible = true;
                 
                    lbl_estado.Text = "Sin Tickets para Descargar...";
                    MyDelay(1000);
                    lbl_estado.Visible = false;
                    lbl_estado2.Visible = false;

                    if (Boton_Reportar == true)
                    {
                        timer2.Enabled = true;
                        timer2.Start();
                        return;
                    }
                    else
                    {
                        timer1.Enabled = true;
                        timer1.Start();
                        return;
                        
                    }

                    
                }
                progressBar1.Maximum = ticketsList.Count;
                progressBar1.Minimum = 0;



                //IMPORTANDO A TICKETSENTRADAUNO

                for (int i = 0; i < ticketsList.Count; i++)
                {
                    lbl_estado.Visible = true;
                    lbl_estado.Text = "Descargando:";
                    progressBar1.Value = i + 1;
                    lbl_estado2.Visible = true;
                    lbl_estado2.Text = progressBar1.Value.ToString() + " / " + ticketsList.Count.ToString();

                    conexion.Open();
                    SqlCommand ComandoTicketsInsert = new SqlCommand();
                    ComandoTicketsInsert.Connection = conexion;
                    ComandoTicketsInsert.CommandType = CommandType.StoredProcedure;
                    ComandoTicketsInsert.CommandText = "TicketsEntradaUnoInsert";
                    
                   
                    ComandoTicketsInsert.Parameters.Add(new SqlParameter("@ApiKey", SqlDbType.VarChar, 50)).Value = txt_apikey.Text.Trim();
                    ComandoTicketsInsert.Parameters.Add(new SqlParameter("@CodigoEvento", SqlDbType.VarChar, 50)).Value = EventoCodigo;
                    
                    if (ticketsList[i].idImpresionTicket== null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@idImpresionTicket", SqlDbType.VarChar, 50)).Value = "";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@idImpresionTicket", SqlDbType.VarChar, 50)).Value = ticketsList[i].idImpresionTicket;
                    }
                    
                    
                    if (ticketsList[i].idLectorTipo== null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@idLectorTipo", SqlDbType.VarChar, 50)).Value ="";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@idLectorTipo", SqlDbType.VarChar, 50)).Value = ticketsList[i].idLectorTipo;
                    }
                    
                    if (ticketsList[i].cLectorCodigo==null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@cLectorCodigo", SqlDbType.VarChar, 50)).Value = "";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@cLectorCodigo", SqlDbType.VarChar, 50)).Value = ticketsList[i].cLectorCodigo;
                    }
                    
                    if (ticketsList[i].cCodigoImpresion ==null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@cCodigoImpresion", SqlDbType.VarChar, 50)).Value ="";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@cCodigoImpresion", SqlDbType.VarChar, 50)).Value = ticketsList[i].cCodigoImpresion;
                    }
                    
                    if (ticketsList[i].idImpresionTicketEstado==null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@idImpresionTicketEstado", SqlDbType.VarChar, 50)).Value = "";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@idImpresionTicketEstado", SqlDbType.VarChar, 50)).Value = ticketsList[i].idImpresionTicketEstado;
                    }

                    if (ticketsList[i].dModificacion==null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@dModificacion", SqlDbType.VarChar, 50)).Value = "";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@dModificacion", SqlDbType.VarChar, 50)).Value = ticketsList[i].dModificacion;
                    }

                    if (ticketsList[i].cSector==null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@cSector", SqlDbType.VarChar, 50)).Value = "Sin Descripcion";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@cSector", SqlDbType.VarChar, 50)).Value = ticketsList[i].cSector;
                    }

                    if (ticketsList[i].cNombre==null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@cNombre", SqlDbType.VarChar, 50)).Value = "Sin Descripcion";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@cNombre", SqlDbType.VarChar, 50)).Value = ticketsList[i].cNombre;
                    }

                    if (ticketsList[i].idFuncion==null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@idFuncion", SqlDbType.VarChar, 50)).Value = "";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@idFuncion", SqlDbType.VarChar, 50)).Value = ticketsList[i].idFuncion;
                    }

                    if (ticketsList[i].nCodigoImpresion == null)
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@nCodigoImpresion", SqlDbType.VarChar, 50)).Value = "";
                    }
                    else
                    {
                        ComandoTicketsInsert.Parameters.Add(new SqlParameter("@nCodigoImpresion", SqlDbType.VarChar, 50)).Value = ticketsList[i].nCodigoImpresion;
                    }

                    ComandoTicketsInsert.Parameters.Add(new SqlParameter("@FechaConsulta", SqlDbType.VarChar, 50)).Value = FechaConsulta;
                    ComandoTicketsInsert.Parameters.Add(new SqlParameter("@FechaLlamada", SqlDbType.VarChar, 50)).Value = fechallamada;
                    ComandoTicketsInsert.CommandTimeout = _CommandTimeOut;
                    ComandoTicketsInsert.ExecuteNonQuery();
                    conexion.Close();

                    
                    MyDelay(int.Parse(ConfigurationManager.AppSettings["miliSegundosdelay"]));
                }

                lbl_estado.Text = "Importando en Evento...";
                

                //IMPORTANDO EN TERCERO
                conexion.Open();

                SqlCommand ComandoTerceroInsert = new SqlCommand();
                ComandoTerceroInsert.Connection = conexion;
                ComandoTerceroInsert.CommandType = CommandType.StoredProcedure;
                ComandoTerceroInsert.CommandText = "Sp_InsertaTicketEntradaUnoTercero";
                ComandoTerceroInsert.Parameters.Add(new SqlParameter("@evento", SqlDbType.VarChar, 50)).Value = EventoCodigo;
                ComandoTerceroInsert.CommandTimeout = _CommandTimeOut;
                ComandoTerceroInsert.ExecuteNonQuery();
                conexion.Close();

                estado = ticketsList.Count.ToString();
                lbl_estado.Text = "Tickets Importados: " + estado;
                lbl_estado2.Visible = true;
                lbl_estado2.Text = "Completado Exitosamente";
                
                if (Boton_Reportar == true)
                {
                    timer2.Enabled = true;
                    timer2.Start();
                }
                else
                {
                    timer1.Enabled = true;
                    timer1.Start();

                }



            }
            catch (Exception ex)
            {
                btn_Descargar.Enabled = false;
                conexion.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Descargar_Click(object sender, EventArgs e)
        {
            if (txt_apikey.Text == "")
            {
                MessageBox.Show("Ingrese ApiKey");
            }
            else
            {

                if (Boton_Reportar == true)
                {
                    btn_Descargar.Text = "Descargando";
                    btn_Descargar.BackColor = Color.Green;
                    Boton_Descargar = true;
                    btn_Descargar.Enabled = false;
                }
                else
                {
                    btn_Descargar.Text = "Descargando";
                    btn_Descargar.BackColor = Color.Green;
                    Boton_Descargar = true;
                    Descargar();
                }


                
                
            }


           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Descargar();
        }

       

        private void btn_reportar_Click(object sender, EventArgs e)
        {
            if (txt_apikey.Text == "")
            {
                MessageBox.Show("Ingrese ApiKey");
            }
            else
            {

                if (Boton_Descargar == true && Boton_Reportar == false)
                {
                    Boton_Reportar = true;
                    btn_reportar.Text = "Detener Reporte";
                    btn_reportar.BackColor = Color.Green;
                    txt_cantidad_subida.Enabled = false;
                    txt_url_subida.Enabled = false;
                    
                    return;
                    
                }
                
                if (Boton_Descargar==true && Boton_Reportar == true)
                {
                    Boton_Reportar = false;
                    btn_reportar.Text = "Reportar";
                    btn_reportar.BackColor = Color.FromArgb(60, 60, 60);
                    txt_cantidad_subida.Enabled = true;
                    return;
                }

                if (Boton_Descargar==false && Boton_Reportar == false)
                {
                    Boton_Reportar = true;
                    btn_reportar.Text = "Detener Reporte";
                    btn_reportar.BackColor = Color.Green;
                    txt_cantidad_subida.Enabled = false;
                    txt_url_subida.Enabled = false;
                    txt_apikey.Enabled = false;
                    txt_url.Enabled = false;
                    cbox_evento.Enabled = false;
                    Reportar();
                    return;
                }
                
                if (Boton_Descargar == false && Boton_Reportar == true)
                {
                    Boton_Reportar = false;
                    btn_reportar.Text = "Reportar";
                    btn_reportar.BackColor = Color.FromArgb(60, 60, 60);
                    txt_cantidad_subida.Enabled = true;
                    txt_apikey.Enabled = true;
                    cbox_evento.Enabled = true;
                    txt_url.Enabled = true;
                    txt_url_subida.Enabled = true;

                    timer2.Stop();
                    timer2.Enabled = false;
                    return;
                }



            }


           
        }

        private void  Reportar()
        {
            timer2.Enabled = false;
            timer2.Stop();
            lbl_estado.Visible = true;
            lbl_estado2.Visible = false;
            lbl_estado.Text = "Consultando tickets a reportar...";
            MyDelay(1000);

            try
            {


                if (int.Parse(ProcesaTicketsUtilizados(txt_apikey.Text)) > 0)
                {
                    conexion.Open();
                    SqlCommand Comando = new SqlCommand();
                    Comando.Connection = conexion;
                    Comando.CommandType = CommandType.StoredProcedure;
                    Comando.CommandText = "SP_ConsultaTicketsUtilizadosEntradaUno";
                    Comando.Parameters.Add(new SqlParameter("@apikey", SqlDbType.VarChar, 50)).Value = txt_apikey.Text;
                    SqlDataAdapter data = new SqlDataAdapter(Comando);
                    DataTable Tabla = new DataTable();
                    data.Fill(Tabla);
                    conexion.Close();

                    CantTicketsUtilizados = Tabla.Rows.Count;
                    TicketSyncro Syncro = new TicketSyncro();

                    progressBar1.Maximum = CantTicketsUtilizados;
                    progressBar1.Minimum = 0;
                    lbl_estado.Visible = true;
                    lbl_estado2.Visible = true;
                    lbl_estado.Text = "Reportando tickets";
                    

                    for (int i = 0; i < Tabla.Rows.Count; i++)
                    {
                        lbl_estado2.Text =(i+1).ToString()+" / "+ CantTicketsUtilizados.ToString() + " Tickets utilizados";
                        progressBar1.Value = i+1;
                        
                        Syncro.TicketSyncroID = Tabla.Rows[i]["id_ticket_EntradaUno_syncro"].ToString();
                        Syncro.cCodigoImpresion = Tabla.Rows[i]["ccodigoimpresion"].ToString();
                        Syncro.clector = Tabla.Rows[i]["clector"].ToString();
                        string url = txt_url_subida.Text.Trim().Replace("clector", Syncro.clector);
                        var client = new RestClient(url);
                        var body = @"[ {""cCodigoImpresion"":""codigosyncro""}]";
                        body = body.Replace("codigosyncro", Syncro.cCodigoImpresion);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Authorization", txt_apikey.Text.Trim());
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", body, ParameterType.RequestBody);

                        IRestResponse response = client.Execute(request);


                        conexion.Open();
                        SqlCommand Comando2 = new SqlCommand();
                        Comando2.Connection = conexion;
                        Comando2.CommandType = CommandType.StoredProcedure;
                        Comando2.CommandText = "SP_RegistraStatusTicketEntradaUno_Syncro";
                        Comando2.Parameters.Add(new SqlParameter("@id_ticket", SqlDbType.VarChar, 50)).Value = Syncro.TicketSyncroID;
                        Comando2.Parameters.Add(new SqlParameter("@resultado", SqlDbType.VarChar, 50)).Value = response.StatusCode.ToString();
                        Comando2.ExecuteNonQuery();
                        conexion.Close();
                        MyDelay(1);
                    }
                    MyDelay(1000);
                    lbl_estado.Text = "Reporte Finalizado";
                    lbl_estado2.Visible = false;
                    progressBar1.Value = 0;
                    MyDelay(1000);
                    lbl_estado.Visible = false;



                }
                else
                {
                    lbl_estado.Visible = true;
                    lbl_estado.Text = "Sin tickets a reportar";
                    MyDelay(1000);
                    lbl_estado.Visible = false;
                    
                }

                if (Boton_Descargar == true && Boton_Reportar==true)
                {
                    timer1.Enabled = true;
                    timer1.Start();
                }
                if (Boton_Descargar ==false && Boton_Reportar==true)
                {
                    timer2.Enabled = true;
                    timer2.Start();
                }
                if (Boton_Descargar == true  && Boton_Reportar == false)
                {
                    timer1.Enabled = true;
                    timer1.Start();
                }




            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                if (conexion.State.ToString()=="Open")
                {
                    conexion.Close();
                }
                
               
            }

        }


        private string ProcesaTicketsUtilizados(string apikey)
        {
            try
            {
                conexion.Open();
                SqlCommand Comando = new SqlCommand();
                Comando.Connection = conexion;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "SP_ProcesaTicketsUtilizadosEntradaUno";
                Comando.Parameters.Add(new SqlParameter("@apikey", SqlDbType.VarChar, 50)).Value = apikey;
                Comando.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int)).Value = Convert.ToInt32(txt_cantidad_subida.Text);
                var a = Comando.ExecuteScalar();
                conexion.Close();
                return a.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "0";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Reportar();
        }
    }
}
