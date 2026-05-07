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
    public partial class frmEquipos : Form
    {
        public frmEquipos()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Ingrese un código.");
                    return;
                }

                Conexion cn = new Conexion();

                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"SELECT id_equipo, nombre, marca, estado, modelo
                         FROM EQUIPO
                         WHERE codigo_equipo = @codigo";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value =
                            txtCodigo.Text.Trim();

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox2.Text = reader["nombre"].ToString();
                                textBox3.Text = reader["marca"].ToString();
                                textBox4.Text = reader["estado"].ToString();
                                textBox5.Text = reader["modelo"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Equipo no encontrado.");

                                textBox2.Clear();
                                textBox3.Clear();
                                textBox4.Clear();
                            }
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el equipo: " + ex.Message);
            }
        }
    }
}
