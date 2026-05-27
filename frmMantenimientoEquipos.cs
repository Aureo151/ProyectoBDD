using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ProyectoBDD
{
    public partial class frmMantenimientoEquipos : Form
    {
        Conexion cn = new Conexion();

        public frmMantenimientoEquipos()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CargarEquipos()
        {
            try
            {
                using(SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"SELECT id_equipo, nombre FROM EQUIPO";

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);
                        
                        cmbEquipo.DataSource = dt;
                        cmbEquipo.DisplayMember = "nombre";
                        cmbEquipo.ValueMember = "id_equipo";
                        cmbEquipo.SelectedIndex = -1;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los equipos: " + ex.Message);
            }
        }

        private void frmMantenimientoEquipos_Load(object sender, EventArgs e)
        {
            CargarEquipos();
        }
    }
}
