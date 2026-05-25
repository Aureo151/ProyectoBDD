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
            try
            {
                using (SqlConnection conn = con.ObtenerConexion())
                {
                    string query = @"SELECT 
                            id_equipo,
                            codigo_equipo,
                            nombre,
                            marca,
                            estado,
                            modelo,
                            numero_serie
                         FROM EQUIPO";

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
                MessageBox.Show("Error al mostrar los registros: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = con.ObtenerConexion())
                {
                    
                    string query = @"
            SELECT 
                M.id_mantenimiento_equipo,
                M.fecha,
                M.responsable,
                M.id_equipo,
                E.codigo_equipo,
                E.nombre,
                E.marca,
                E.modelo,
                E.numero_serie,
                E.estado,
                E.id_espacio,
                E.id_tipo
            FROM MANTENIMIENTO_EQUIPO M
            INNER JOIN EQUIPO E 
                ON M.id_equipo = E.id_equipo";

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
                MessageBox.Show("Error al mostrar el mantenimiento de equipos: " + ex.Message);
            }
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
                ME.id_espacio,
                E.nombre,
                E.tipo,
                E.id_centro
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
    }
}
