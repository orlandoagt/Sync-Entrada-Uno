
namespace Sync_Entrada_Uno
{
    partial class Principal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.cbox_evento = new System.Windows.Forms.ComboBox();
            this.lbl_evento = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbl_estado = new System.Windows.Forms.Label();
            this.lbl_estado2 = new System.Windows.Forms.Label();
            this.txt_apikey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.btn_Descargar = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_reportar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_cantidad_subida = new System.Windows.Forms.TextBox();
            this.txt_url_subida = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbox_evento
            // 
            this.cbox_evento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_evento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbox_evento.FormattingEnabled = true;
            this.cbox_evento.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbox_evento.Location = new System.Drawing.Point(21, 85);
            this.cbox_evento.Name = "cbox_evento";
            this.cbox_evento.Size = new System.Drawing.Size(537, 24);
            this.cbox_evento.TabIndex = 1;
            this.cbox_evento.SelectedIndexChanged += new System.EventHandler(this.cbox_evento_SelectedIndexChanged);
            // 
            // lbl_evento
            // 
            this.lbl_evento.AutoSize = true;
            this.lbl_evento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_evento.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_evento.Location = new System.Drawing.Point(21, 66);
            this.lbl_evento.Name = "lbl_evento";
            this.lbl_evento.Size = new System.Drawing.Size(50, 16);
            this.lbl_evento.TabIndex = 2;
            this.lbl_evento.Text = "Evento";
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.progressBar1.Location = new System.Drawing.Point(21, 264);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(537, 63);
            this.progressBar1.TabIndex = 5;
            // 
            // lbl_estado
            // 
            this.lbl_estado.AutoSize = true;
            this.lbl_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_estado.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_estado.Location = new System.Drawing.Point(18, 342);
            this.lbl_estado.Name = "lbl_estado";
            this.lbl_estado.Size = new System.Drawing.Size(51, 16);
            this.lbl_estado.TabIndex = 6;
            this.lbl_estado.Text = "Estado";
            // 
            // lbl_estado2
            // 
            this.lbl_estado2.AutoSize = true;
            this.lbl_estado2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_estado2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_estado2.Location = new System.Drawing.Point(18, 365);
            this.lbl_estado2.Name = "lbl_estado2";
            this.lbl_estado2.Size = new System.Drawing.Size(51, 16);
            this.lbl_estado2.TabIndex = 7;
            this.lbl_estado2.Text = "Estado";
            // 
            // txt_apikey
            // 
            this.txt_apikey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_apikey.Location = new System.Drawing.Point(21, 134);
            this.txt_apikey.Name = "txt_apikey";
            this.txt_apikey.Size = new System.Drawing.Size(537, 22);
            this.txt_apikey.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "ApiKey";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(21, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "URL Bajada";
            // 
            // txt_url
            // 
            this.txt_url.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_url.Location = new System.Drawing.Point(21, 178);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(537, 22);
            this.txt_url.TabIndex = 11;
            this.txt_url.Text = "https://api-servicios-showticket.entradauno.com/v1/Accesos/cFechaDesde/0";
            // 
            // btn_Descargar
            // 
            this.btn_Descargar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_Descargar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btn_Descargar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btn_Descargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Descargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Descargar.ForeColor = System.Drawing.Color.White;
            this.btn_Descargar.Location = new System.Drawing.Point(408, 364);
            this.btn_Descargar.Name = "btn_Descargar";
            this.btn_Descargar.Size = new System.Drawing.Size(150, 38);
            this.btn_Descargar.TabIndex = 12;
            this.btn_Descargar.Text = "Descargar";
            this.btn_Descargar.UseVisualStyleBackColor = false;
            this.btn_Descargar.Click += new System.EventHandler(this.btn_Descargar_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(344, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(214, 77);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // btn_reportar
            // 
            this.btn_reportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_reportar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btn_reportar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btn_reportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reportar.ForeColor = System.Drawing.Color.White;
            this.btn_reportar.Location = new System.Drawing.Point(408, 414);
            this.btn_reportar.Name = "btn_reportar";
            this.btn_reportar.Size = new System.Drawing.Size(150, 39);
            this.btn_reportar.TabIndex = 62;
            this.btn_reportar.Text = "Reportar";
            this.btn_reportar.UseVisualStyleBackColor = false;
            this.btn_reportar.Click += new System.EventHandler(this.btn_reportar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(273, 412);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 16);
            this.label4.TabIndex = 61;
            this.label4.Text = "Cantidad a Reportar";
            // 
            // txt_cantidad_subida
            // 
            this.txt_cantidad_subida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cantidad_subida.Location = new System.Drawing.Point(276, 431);
            this.txt_cantidad_subida.Name = "txt_cantidad_subida";
            this.txt_cantidad_subida.Size = new System.Drawing.Size(126, 22);
            this.txt_cantidad_subida.TabIndex = 60;
            this.txt_cantidad_subida.Text = "100";
            // 
            // txt_url_subida
            // 
            this.txt_url_subida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_url_subida.Location = new System.Drawing.Point(21, 225);
            this.txt_url_subida.Name = "txt_url_subida";
            this.txt_url_subida.Size = new System.Drawing.Size(537, 22);
            this.txt_url_subida.TabIndex = 64;
            this.txt_url_subida.Text = "https://api-servicios-showticket.entradauno.com/v1/Accesos/clector";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(21, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "URL Subida";
            // 
            // timer2
            // 
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(583, 465);
            this.Controls.Add(this.txt_url_subida);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_reportar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_cantidad_subida);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_Descargar);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_apikey);
            this.Controls.Add(this.lbl_estado2);
            this.Controls.Add(this.lbl_estado);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lbl_evento);
            this.Controls.Add(this.cbox_evento);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sync EntradaUno V2.0";
            this.Load += new System.EventHandler(this.Principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbox_evento;
        private System.Windows.Forms.Label lbl_evento;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbl_estado;
        private System.Windows.Forms.Label lbl_estado2;
        private System.Windows.Forms.TextBox txt_apikey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.Button btn_Descargar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_reportar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_cantidad_subida;
        private System.Windows.Forms.TextBox txt_url_subida;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer2;
    }
}

