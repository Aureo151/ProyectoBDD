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
    public partial class frmRegistros : Form
    {
        Conexion con = new Conexion();
        public frmRegistros()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = con.ObtenerConexion())
                {
                    
                    string query = @"
            SELECT 
                ME.id_mantenimiento_espacio,
                ME.fecha,
                ME.responsable,
                
                E.nombre,
                E.tipo
                
            FROM MANTENIMIENTO_ESPACIO ME
            INNER JOIN ESPACIO E 
                ON ME.id_espacio = E.id_espacio";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar el mantenimiento de espacios: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Opción A: Desvincular por completo (deja el DataGridView totalmente en blanco)
            dataGridView1.DataSource = null;

            // Opción B: Si quieres conservar los nombres de las columnas (cabeceras) pero vaciar las filas
            if (dataGridView1.DataSource is DataTable dt)
            {
                dt.Clear();
            }
        }
    }
}
