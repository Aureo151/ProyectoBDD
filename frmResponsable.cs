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
    public partial class frmResponsable : Form
    {
        public frmResponsable()
        {
            InitializeComponent();
        }
        private void mostrarResponsables()
        {
            SqlDataAdapter adapter = new SqlDataAdapter(
                "SELECT codigo_responsable, nombre, telefono, correo FROM RESPONSABLE",
                new Conexion().ObtenerConexion()
            );

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }
        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
        }

        private void frmResponsable_Load(object sender, EventArgs e)
        {
            mostrarResponsables();
            Estilos.AplicarEstilo(this);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexion cn = new Conexion();

                using (SqlConnection connection = cn.ObtenerConexion())
                {
                    string sql = @"INSERT INTO RESPONSABLE
                           (codigo_responsable, nombre, telefono, correo)
                           VALUES
                           (@codigo, @nombre, @telefono, @correo)";

                    using (SqlCommand comando = new SqlCommand(sql, connection))
                    {
                        comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value =
                            txtCodigo.Text.Trim();

                        comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value =
                            txtNombre.Text.Trim();

                        comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value =
                            txtTelefono.Text.Trim();

                        comando.Parameters.Add("@correo", SqlDbType.VarChar).Value =
                            label6.Text.Trim();

                        connection.Open();
                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Responsable guardado correctamente.",
                                "Nuevo Responsable",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                mostrarResponsables();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    MessageBox.Show("Ingrese el código del responsable.");
                    return;
                }

                Conexion cn = new Conexion();

                using (SqlConnection connection = cn.ObtenerConexion())
                {
                    string sql = @"SELECT codigo_responsable, nombre, telefono, correo
                           FROM RESPONSABLE
                           WHERE codigo_responsable = @codigo";

                    using (SqlCommand comando = new SqlCommand(sql, connection))
                    {
                        comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value =
                            txtBuscar.Text.Trim();

                        connection.Open();

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtCodigo.Text = reader["codigo_responsable"].ToString();
                                txtNombre.Text = reader["nombre"].ToString();
                                txtTelefono.Text = reader["telefono"].ToString();
                                txtCorreo.Text = reader["correo"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Responsable no encontrado.");
                                LimpiarCampos();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar responsable: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Ingrese o busque el código del responsable.");
                    return;
                }

                Conexion cn = new Conexion();

                using (SqlConnection connection = cn.ObtenerConexion())
                {
                    string sql = @"UPDATE RESPONSABLE
                           SET nombre = @nombre,
                               telefono = @telefono,
                               correo = @correo
                           WHERE codigo_responsable = @codigo";

                    using (SqlCommand comando = new SqlCommand(sql, connection))
                    {
                        comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value =
                            txtCodigo.Text.Trim();

                        comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value =
                            txtNombre.Text.Trim();

                        comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value =
                            txtTelefono.Text.Trim();

                        comando.Parameters.Add("@correo", SqlDbType.VarChar).Value =
                            txtCorreo.Text.Trim();

                        connection.Open();

                        int filas = comando.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("Responsable modificado correctamente.");
                            mostrarResponsables();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el responsable.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar responsable: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Ingrese o busque el código del responsable.");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "¿Está seguro de eliminar este responsable?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    Conexion cn = new Conexion();

                    using (SqlConnection connection = cn.ObtenerConexion())
                    {
                        string sql = @"DELETE FROM RESPONSABLE
                               WHERE codigo_responsable = @codigo";

                        using (SqlCommand comando = new SqlCommand(sql, connection))
                        {
                            comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value =
                                txtCodigo.Text.Trim();

                            connection.Open();

                            int filas = comando.ExecuteNonQuery();

                            if (filas > 0)
                            {
                                MessageBox.Show("Responsable eliminado correctamente.");
                                mostrarResponsables();
                                LimpiarCampos();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el responsable.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar responsable: " + ex.Message);
            }
        }
    }
}
