namespace ProyectoBDD
{
    partial class frmReportes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnEspaciosPorCentro = new System.Windows.Forms.Button();
            this.btnEntreFechas = new System.Windows.Forms.Button();
            this.btnTopConsultorios = new System.Windows.Forms.Button();
            this.btnEquiposPorEspacio = new System.Windows.Forms.Button();
            this.btnBuscarEquipo = new System.Windows.Forms.Button();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.lblInicio = new System.Windows.Forms.Label();
            this.lblFin = new System.Windows.Forms.Label();
            this.lblCodigoEspacio = new System.Windows.Forms.Label();
            this.txtCodigoEspacio = new System.Windows.Forms.TextBox();
            this.lblBuscarEquipo = new System.Windows.Forms.Label();
            this.txtBuscarEquipo = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 77);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(835, 445);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnEspaciosPorCentro
            // 
            this.btnEspaciosPorCentro.Location = new System.Drawing.Point(883, 77);
            this.btnEspaciosPorCentro.Name = "btnEspaciosPorCentro";
            this.btnEspaciosPorCentro.Size = new System.Drawing.Size(248, 55);
            this.btnEspaciosPorCentro.TabIndex = 1;
            this.btnEspaciosPorCentro.Text = "Espacios por Centro de Salud";
            this.btnEspaciosPorCentro.UseVisualStyleBackColor = true;
            this.btnEspaciosPorCentro.Click += new System.EventHandler(this.btnEspaciosPorCentro_Click);
            // 
            // btnEntreFechas
            // 
            this.btnEntreFechas.Location = new System.Drawing.Point(883, 232);
            this.btnEntreFechas.Name = "btnEntreFechas";
            this.btnEntreFechas.Size = new System.Drawing.Size(248, 55);
            this.btnEntreFechas.TabIndex = 2;
            this.btnEntreFechas.Text = "Incidencias y Mantenimientos entre Fechas";
            this.btnEntreFechas.UseVisualStyleBackColor = true;
            this.btnEntreFechas.Click += new System.EventHandler(this.btnEntreFechas_Click);
            // 
            // btnTopConsultorios
            // 
            this.btnTopConsultorios.Location = new System.Drawing.Point(883, 307);
            this.btnTopConsultorios.Name = "btnTopConsultorios";
            this.btnTopConsultorios.Size = new System.Drawing.Size(248, 55);
            this.btnTopConsultorios.TabIndex = 3;
            this.btnTopConsultorios.Text = "Top 3 Consultorios por Centro";
            this.btnTopConsultorios.UseVisualStyleBackColor = true;
            this.btnTopConsultorios.Click += new System.EventHandler(this.btnTopConsultorios_Click);
            // 
            // btnEquiposPorEspacio
            // 
            this.btnEquiposPorEspacio.Location = new System.Drawing.Point(883, 429);
            this.btnEquiposPorEspacio.Name = "btnEquiposPorEspacio";
            this.btnEquiposPorEspacio.Size = new System.Drawing.Size(248, 55);
            this.btnEquiposPorEspacio.TabIndex = 4;
            this.btnEquiposPorEspacio.Text = "Equipos / Enseres del Espacio";
            this.btnEquiposPorEspacio.UseVisualStyleBackColor = true;
            this.btnEquiposPorEspacio.Click += new System.EventHandler(this.btnEquiposPorEspacio_Click);
            // 
            // btnBuscarEquipo
            // 
            this.btnBuscarEquipo.Location = new System.Drawing.Point(883, 599);
            this.btnBuscarEquipo.Name = "btnBuscarEquipo";
            this.btnBuscarEquipo.Size = new System.Drawing.Size(248, 55);
            this.btnBuscarEquipo.TabIndex = 5;
            this.btnBuscarEquipo.Text = "Buscar Equipo por Código o Serie";
            this.btnBuscarEquipo.UseVisualStyleBackColor = true;
            this.btnBuscarEquipo.Click += new System.EventHandler(this.btnBuscarEquipo_Click);
            // 
            // dtpInicio
            // 
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Location = new System.Drawing.Point(883, 169);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(116, 22);
            this.dtpInicio.TabIndex = 6;
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(1015, 169);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(116, 22);
            this.dtpFin.TabIndex = 7;
            // 
            // lblInicio
            // 
            this.lblInicio.AutoSize = true;
            this.lblInicio.Location = new System.Drawing.Point(880, 146);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(82, 16);
            this.lblInicio.TabIndex = 8;
            this.lblInicio.Text = "Fecha inicio";
            // 
            // lblFin
            // 
            this.lblFin.AutoSize = true;
            this.lblFin.Location = new System.Drawing.Point(1012, 146);
            this.lblFin.Name = "lblFin";
            this.lblFin.Size = new System.Drawing.Size(62, 16);
            this.lblFin.TabIndex = 9;
            this.lblFin.Text = "Fecha fin";
            // 
            // lblCodigoEspacio
            // 
            this.lblCodigoEspacio.AutoSize = true;
            this.lblCodigoEspacio.Location = new System.Drawing.Point(880, 382);
            this.lblCodigoEspacio.Name = "lblCodigoEspacio";
            this.lblCodigoEspacio.Size = new System.Drawing.Size(204, 16);
            this.lblCodigoEspacio.TabIndex = 10;
            this.lblCodigoEspacio.Text = "Código / ID del espacio a revisar";
            // 
            // txtCodigoEspacio
            // 
            this.txtCodigoEspacio.Location = new System.Drawing.Point(883, 401);
            this.txtCodigoEspacio.Name = "txtCodigoEspacio";
            this.txtCodigoEspacio.Size = new System.Drawing.Size(248, 22);
            this.txtCodigoEspacio.TabIndex = 11;
            // 
            // lblBuscarEquipo
            // 
            this.lblBuscarEquipo.AutoSize = true;
            this.lblBuscarEquipo.Location = new System.Drawing.Point(880, 524);
            this.lblBuscarEquipo.Name = "lblBuscarEquipo";
            this.lblBuscarEquipo.Size = new System.Drawing.Size(236, 16);
            this.lblBuscarEquipo.TabIndex = 12;
            this.lblBuscarEquipo.Text = "Código de inventario o número de serie";
            // 
            // txtBuscarEquipo
            // 
            this.txtBuscarEquipo.Location = new System.Drawing.Point(883, 552);
            this.txtBuscarEquipo.Name = "txtBuscarEquipo";
            this.txtBuscarEquipo.Size = new System.Drawing.Size(248, 22);
            this.txtBuscarEquipo.TabIndex = 13;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(19, 24);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(214, 29);
            this.lblTitulo.TabIndex = 14;
            this.lblTitulo.Text = "Reportes del Sistema";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(21, 540);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(147, 16);
            this.lblTotal.TabIndex = 15;
            this.lblTotal.Text = "Registros encontrados: 0";
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 683);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtBuscarEquipo);
            this.Controls.Add(this.lblBuscarEquipo);
            this.Controls.Add(this.txtCodigoEspacio);
            this.Controls.Add(this.lblCodigoEspacio);
            this.Controls.Add(this.lblFin);
            this.Controls.Add(this.lblInicio);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.dtpInicio);
            this.Controls.Add(this.btnBuscarEquipo);
            this.Controls.Add(this.btnEquiposPorEspacio);
            this.Controls.Add(this.btnTopConsultorios);
            this.Controls.Add(this.btnEntreFechas);
            this.Controls.Add(this.btnEspaciosPorCentro);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnEspaciosPorCentro;
        private System.Windows.Forms.Button btnEntreFechas;
        private System.Windows.Forms.Button btnTopConsultorios;
        private System.Windows.Forms.Button btnEquiposPorEspacio;
        private System.Windows.Forms.Button btnBuscarEquipo;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.Label lblFin;
        private System.Windows.Forms.Label lblCodigoEspacio;
        private System.Windows.Forms.TextBox txtCodigoEspacio;
        private System.Windows.Forms.Label lblBuscarEquipo;
        private System.Windows.Forms.TextBox txtBuscarEquipo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblTotal;
    }
}
