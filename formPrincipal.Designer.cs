namespace ProyectoBDD
{
    partial class formPrincipal
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
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sedesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenimientosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centrosDeSaludToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espaciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equiposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignarEnseresYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignarIncidenciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verAsignacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignarIncidenciasDeEquipoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(606, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sistema de Control de Espacios, Equipos e Inventario";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionToolStripMenuItem,
            this.mantenimientosToolStripMenuItem,
            this.asignacionesToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sedesToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.configuracionToolStripMenuItem.Text = "Sistema";
            this.configuracionToolStripMenuItem.Click += new System.EventHandler(this.configuracionToolStripMenuItem_Click);
            // 
            // sedesToolStripMenuItem
            // 
            this.sedesToolStripMenuItem.Name = "sedesToolStripMenuItem";
            this.sedesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.sedesToolStripMenuItem.Text = "Iniciar Sesión";
            this.sedesToolStripMenuItem.Click += new System.EventHandler(this.sedesToolStripMenuItem_Click);
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // mantenimientosToolStripMenuItem
            // 
            this.mantenimientosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centrosDeSaludToolStripMenuItem,
            this.espaciosToolStripMenuItem,
            this.equiposToolStripMenuItem});
            this.mantenimientosToolStripMenuItem.Name = "mantenimientosToolStripMenuItem";
            this.mantenimientosToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.mantenimientosToolStripMenuItem.Text = "Mantenimientos";
            // 
            // centrosDeSaludToolStripMenuItem
            // 
            this.centrosDeSaludToolStripMenuItem.Name = "centrosDeSaludToolStripMenuItem";
            this.centrosDeSaludToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.centrosDeSaludToolStripMenuItem.Text = "Centros De Salud";
            this.centrosDeSaludToolStripMenuItem.Click += new System.EventHandler(this.centrosDeSaludToolStripMenuItem_Click);
            // 
            // espaciosToolStripMenuItem
            // 
            this.espaciosToolStripMenuItem.Name = "espaciosToolStripMenuItem";
            this.espaciosToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.espaciosToolStripMenuItem.Text = "Espacios";
            this.espaciosToolStripMenuItem.Click += new System.EventHandler(this.espaciosToolStripMenuItem_Click);
            // 
            // equiposToolStripMenuItem
            // 
            this.equiposToolStripMenuItem.Name = "equiposToolStripMenuItem";
            this.equiposToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.equiposToolStripMenuItem.Text = "Equipos";
            this.equiposToolStripMenuItem.Click += new System.EventHandler(this.equiposToolStripMenuItem_Click);
            // 
            // asignacionesToolStripMenuItem
            // 
            this.asignacionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asignarEnseresYToolStripMenuItem,
            this.asignarIncidenciasToolStripMenuItem,
            this.asignarIncidenciasDeEquipoToolStripMenuItem});
            this.asignacionesToolStripMenuItem.Name = "asignacionesToolStripMenuItem";
            this.asignacionesToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.asignacionesToolStripMenuItem.Text = "Asignaciones";
            // 
            // asignarEnseresYToolStripMenuItem
            // 
            this.asignarEnseresYToolStripMenuItem.Name = "asignarEnseresYToolStripMenuItem";
            this.asignarEnseresYToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.asignarEnseresYToolStripMenuItem.Text = "Asignar Enseres a Equipos";
            this.asignarEnseresYToolStripMenuItem.Click += new System.EventHandler(this.asignarEnseresYToolStripMenuItem_Click);
            // 
            // asignarIncidenciasToolStripMenuItem
            // 
            this.asignarIncidenciasToolStripMenuItem.Name = "asignarIncidenciasToolStripMenuItem";
            this.asignarIncidenciasToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.asignarIncidenciasToolStripMenuItem.Text = "Asignar Incidencias";
            this.asignarIncidenciasToolStripMenuItem.Click += new System.EventHandler(this.asignarIncidenciasToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verAsignacionesToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // verAsignacionesToolStripMenuItem
            // 
            this.verAsignacionesToolStripMenuItem.Name = "verAsignacionesToolStripMenuItem";
            this.verAsignacionesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.verAsignacionesToolStripMenuItem.Text = "Ver Asignaciones";
            this.verAsignacionesToolStripMenuItem.Click += new System.EventHandler(this.verAsignacionesToolStripMenuItem_Click);
            // 
            // asignarIncidenciasDeEquipoToolStripMenuItem
            // 
            this.asignarIncidenciasDeEquipoToolStripMenuItem.Name = "asignarIncidenciasDeEquipoToolStripMenuItem";
            this.asignarIncidenciasDeEquipoToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.asignarIncidenciasDeEquipoToolStripMenuItem.Text = "Asignar Incidencias de Equipo";
            this.asignarIncidenciasDeEquipoToolStripMenuItem.Click += new System.EventHandler(this.asignarIncidenciasDeEquipoToolStripMenuItem_Click);
            // 
            // formPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "formPrincipal";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sedesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mantenimientosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centrosDeSaludToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espaciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem equiposToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignarEnseresYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verAsignacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignarIncidenciasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignarIncidenciasDeEquipoToolStripMenuItem;
    }
}

