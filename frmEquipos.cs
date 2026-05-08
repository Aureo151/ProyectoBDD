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
                                txtNoSerie.Text = reader["numero_serie"].ToString();
                                txtNombre.Text = reader["nombre"].ToString();
                                txtMarca.Text = reader["marca"].ToString();
                                txtEstado.Text = reader["estado"].ToString();
                                txtModelo.Text = reader["modelo"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Equipo no encontrado.");
                                txtNoSerie.Clear();
                                txtNombre.Clear();
                                txtMarca.Clear();
                                txtEstado.Clear();
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
                            txtNombre.Text.Trim();

                        comando.Parameters.Add("@marca", SqlDbType.VarChar).Value =
                            txtMarca.Text.Trim();

                        comando.Parameters.Add("@modelo", SqlDbType.VarChar).Value =
                            txtModelo.Text.Trim();

                        comando.Parameters.Add("@numero_serie", SqlDbType.VarChar).Value =
                            txtNoSerie.Text.Trim();

                        comando.Parameters.Add("@estado", SqlDbType.VarChar).Value =
                            txtEstado.Text.Trim();

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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Ingrese el codigo del equipo a eliminar.");
                    return;
                }

                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el equipo?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(result == DialogResult.Yes)
                {
                    Conexion cn = new Conexion();
                    using (SqlConnection connection = cn.ObtenerConexion())
                    {
                        string sql = @"DELETE FROM EQUIPO WHERE codigo_equipo = @codigo";
                        using (SqlCommand comando = new SqlCommand(sql, connection))
                        {
                            comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value =
                            txtCodigo.Text.Trim();
                            connection.Open();
                            int rowsAffected = comando.ExecuteNonQuery();                      
                            MessageBox.Show("Equipo eliminado correctamente.");
                            mostrarEquipos();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el equipo: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Ingrese el codigo del equipo a actualizar.");
                    return;
                }

                Conexion cn = new Conexion();

                using(SqlConnection connection = cn.ObtenerConexion())
                {
                    string sql = @"UPDATE EQUIPO
                                   SET nombre = @nombre,
                                       marca = @marca,
                                       modelo = @modelo,
                                       numero_serie = @numero_serie,
                                       estado = @estado
                                   WHERE codigo_equipo = @codigo";
                    using (SqlCommand comando = new SqlCommand(sql, connection))
                    {
                        comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value =
                            txtCodigo.Text.Trim();
                        comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value =
                            txtNombre.Text.Trim();
                        comando.Parameters.Add("@marca", SqlDbType.VarChar).Value =
                            txtMarca.Text.Trim();
                        comando.Parameters.Add("@modelo", SqlDbType.VarChar).Value =
                            txtModelo.Text.Trim();
                        comando.Parameters.Add("@numero_serie", SqlDbType.VarChar).Value =
                            txtNoSerie.Text.Trim();
                        comando.Parameters.Add("@estado", SqlDbType.VarChar).Value =
                            txtEstado.Text.Trim();
                        connection.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        if(rowsAffected > 0)
                        {
                            MessageBox.Show("Equipo actualizado correctamente.");
                            mostrarEquipos();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el equipo para actualizar.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el equipo: " + ex.Message);
            }
        }
    }
}
