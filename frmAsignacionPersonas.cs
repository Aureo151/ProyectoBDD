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
    public partial class frmAsignacionPersonas : Form
    {
        public frmAsignacionPersonas()
        {
            InitializeComponent();
        }

        private void frmAsignacionPersonas_Load(object sender, EventArgs e)
        {

        }

        private void CargarUsuarios()
        {
            

            
            comboBox1.DisplayMember = "NombreUsuario";
            comboBox1.ValueMember = "IdUsuario";
        }
    }
}
