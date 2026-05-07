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
        public FormAdministrador()
        {
            InitializeComponent();
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
    }
}
