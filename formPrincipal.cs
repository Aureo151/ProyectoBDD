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
    public partial class formPrincipal : Form
    {
        public formPrincipal()
        {
            InitializeComponent();
        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sedesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
            
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sesión cerrada");
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void centrosDeSaludToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCentroSalud frmCentroSalud = new frmCentroSalud();
            frmCentroSalud.ShowDialog();
        }

        private void espaciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEspacios frmEspacios = new frmEspacios();
            frmEspacios.ShowDialog();
        }

        private void equiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEquipos frmEquipos = new frmEquipos();
            frmEquipos.ShowDialog();
        }

        private void asignarEnseresYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAsignacionEnseres df = new frmAsignacionEnseres();
            df.ShowDialog();
        }

        private void verAsignacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmRegistros fg = new frmRegistros();
            fg.ShowDialog();
        }

        private void asignarIncidenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroIncidencias lñ = new frmRegistroIncidencias();
            lñ.ShowDialog();
        }

        private void asignarIncidenciasDeEquipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroIncidenciasEquipo hj = new frmRegistroIncidenciasEquipo();
            hj.ShowDialog();
        }
    }
}
