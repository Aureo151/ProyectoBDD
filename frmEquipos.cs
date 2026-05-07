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
                    string query = @"SELECT id_equipo, nombre, marca, estado, modelo,numero_serie
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
                                textBox1.Text = reader["numero_serie"].ToString();
                                textBox2.Text = reader["nombre"].ToString();
                                textBox3.Text = reader["marca"].ToString();
                                textBox4.Text = reader["estado"].ToString();
                                textBox5.Text = reader["modelo"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Equipo no encontrado.");
                                textBox1.Clear();
                                textBox2.Clear();
                                textBox3.Clear();
                                textBox4.Clear();
                            }
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el equipo: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexion cn = new Conexion();

                using (SqlConnection connection = cn.ObtenerConexion())
                {
                    string sql = @"INSERT INTO EQUIPO
                       (codigo_equipo ,nombre, marca, modelo, numero_serie, estado)
                       VALUES
                       (@codigo_equipo, @nombre, @marca, @modelo, @numero_serie, @estado)";

                    using (SqlCommand comando = new SqlCommand(sql, connection))
                    {
                        comando.Parameters.Add("@codigo_equipo", SqlDbType.VarChar).Value =
                            txtCodigo.Text.Trim();

                        comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value =
                            textBox2.Text.Trim();

                        comando.Parameters.Add("@marca", SqlDbType.VarChar).Value =
                            textBox3.Text.Trim();

                        comando.Parameters.Add("@modelo", SqlDbType.VarChar).Value =
                            textBox5.Text.Trim();

                        comando.Parameters.Add("@numero_serie", SqlDbType.VarChar).Value =
                            textBox1.Text.Trim();

                        comando.Parameters.Add("@estado", SqlDbType.VarChar).Value =
                            textBox4.Text.Trim();

                        connection.Open();
                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Equipo registrado correctamente.",
                                "Nuevo Equipo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                mostrarEquipos();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void mostrarEquipos()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM EQUIPO", new Conexion().ObtenerConexion());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void frmEquipos_Load(object sender, EventArgs e)
        {
            mostrarEquipos();
        }
    }
}
