using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBDD
{
    public partial class FormAdministrador : Form
    {
        int rolUsuario;
        public FormAdministrador(int rol)
        {
            InitializeComponent();
            rolUsuario = rol;
            ConfigurarMenuPorRol();
        }
        private void ConfigurarMenuPorRol()
        {
            mantenimientosToolStripMenuItem.Visible = false;
            asignacionToolStripMenuItem.Visible = false;
            incidenciasToolStripMenuItem.Visible = false;
            reportesToolStripMenuItem.Visible = false;
            mantenimientosTecnicosToolStripMenuItem.Visible = false;
            sistemaToolStripMenuItem.Visible = true;

            if (rolUsuario == 1) // Administrador
            {
                // Mostrar todas las opciones
                mantenimientosToolStripMenuItem.Visible = true;
                asignacionToolStripMenuItem.Visible = true;
                incidenciasToolStripMenuItem.Visible = true;
                reportesToolStripMenuItem.Visible = true;
                mantenimientosTecnicosToolStripMenuItem.Visible = true;
                sistemaToolStripMenuItem.Visible = true;
            }
            else if (rolUsuario == 2) // Usuario
            {
                // Mostrar solo opciones de usuario
                mantenimientosToolStripMenuItem.Visible = true;
                asignacionToolStripMenuItem.Visible = true;
            }
            else if(rolUsuario == 1002) //Jefe enfermeras
            {
                incidenciasToolStripMenuItem.Visible = true;
            }
            else if(rolUsuario == 1003) //Director Centro
            {
                mantenimientosTecnicosToolStripMenuItem.Visible = true;
                asignacionToolStripMenuItem.Visible = true;
            }
            else if(rolUsuario == 1004) //Director Salud
            {
                reportesToolStripMenuItem.Visible = true;
            }
            else
            {
                MessageBox.Show("Rol de usuario no reconocido. No se mostrarán opciones.");
            }
        }
        private void centroSaludToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCentroSalud frmCentroSalud = new frmCentroSalud();
            frmCentroSalud.ShowDialog();
        }

        private void mantenimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void equiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEquipos frmEquipos = new frmEquipos();
            frmEquipos.ShowDialog();
        }

        private void espaciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEspacios frmEspacios = new frmEspacios();
            frmEspacios.ShowDialog();
        }

        private void asignacionDeEnseresYEquiposAEspaciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAsignacionEnseres frmAsignacionEnseres = new frmAsignacionEnseres();
            frmAsignacionEnseres.ShowDialog();
        }

        private void cerraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Close();
        }

        private void incidenciasDeEquiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroIncidenciasEquipo frmRegIncEqu = new frmRegistroIncidenciasEquipo();
            frmRegIncEqu.ShowDialog();
            
        }

        private void incidenciasDeEspaciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroIncidencias frmRegistro = new frmRegistroIncidencias();
            frmRegistro.ShowDialog();         
        }

        private void verReportesConsultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportes frmReportes = new frmReportes();
            frmReportes.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void asignacionDePersonasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAsignacionPersonas frmAsignacionPersonas = new frmAsignacionPersonas();
            frmAsignacionPersonas.ShowDialog();
        }

        private void mantenimientoSalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMantenimientoSala frmMantenimientoSala = new frmMantenimientoSala();
            frmMantenimientoSala.ShowDialog();
        }

        private void mantenimientoEquipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMantenimientoEquipos frmMantenimientoEquipos = new frmMantenimientoEquipos();
            frmMantenimientoEquipos.ShowDialog();
        }

        private void mantenimientosTecnicosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void estadisticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEstadisticas er = new frmEstadisticas();
            er.ShowDialog();
        }

        private void responsablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmResponsable frmResponsable = new frmResponsable();
            frmResponsable.ShowDialog();
        }

        private void FormAdministrador_Load(object sender, EventArgs e)
        {
            Estilos.AplicarEstilo(this);
        }

        private void usuariosRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
