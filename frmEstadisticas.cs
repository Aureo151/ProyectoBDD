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
    public partial class frmEstadisticas : Form
    {


        Conexion cn = new Conexion();
        public frmEstadisticas()
        {
            InitializeComponent();
        }

        private void frmEstadisticas_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(
     @"SELECT responsable,
             COUNT(id_mantenimiento_equipo) AS total_mantenimientos
      FROM MANTENIMIENTO_EQUIPO
      GROUP BY responsable
      ORDER BY total_mantenimientos DESC",
     new Conexion().ObtenerConexion());

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(
    @"SELECT responsable,
             COUNT(id_mantenimiento_espacio) AS total_mantenimientos
      FROM MANTENIMIENTO_ESPACIO
      GROUP BY responsable
      ORDER BY total_mantenimientos DESC",
    new Conexion().ObtenerConexion());

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(
    @"SELECT tipo,
             COUNT(tipo) AS total_incidencias
      FROM INCIDENCIA_EQUIPO
      GROUP BY tipo
      ORDER BY total_incidencias DESC",
    new Conexion().ObtenerConexion());

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
