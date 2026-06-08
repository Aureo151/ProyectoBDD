using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProyectoBDD
{
    public partial class frmCentroSalud : Form
    {
        public frmCentroSalud()
        {
            InitializeComponent();
        }

        private void mostrarCentros()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM CENTRO_SALUD", new Conexion().ObtenerConexion());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvCentros.DataSource = dt;
        }
        private void frmCentroSalud_Load(object sender, EventArgs e)
        {
            mostrarCentros();
            Estilos.AplicarEstilo(this);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(txtNombreBuscar.Text))
                {
                    MessageBox.Show("Ingrese un nombre de centro a buscar");
                    return;
                }

                Conexion cn = new Conexion();

                using(SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"SELECT id_centro, nombre, direccion, 
                    telefono from CENTRO_SALUD WHERE nombre = @nombre";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(@"nombre", SqlDbType.VarChar).Value 
                        = txtNombreBuscar.Text.Trim();

                        conn.Open();

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                txtNombre.Text = reader["nombre"].ToString();
                                txtDireccion.Text = reader["direccion"].ToString();
                                txtTelefono.Text = reader["telefono"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Centro no encontrado");
                                txtNombre.Clear();
                                txtDireccion.Clear();
                                txtTelefono.Clear();

                                return;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el centro: " + ex.Message);   
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexion cn = new Conexion();

                using(SqlConnection connection = cn.ObtenerConexion())
                {
                    string sql = @"INSERT INTO CENTRO_SALUD
                    (nombre, direccion, telefono) VALUES (@nombre, @direccion, @telefono)";

                    using (SqlCommand comando = new SqlCommand(sql, connection))
                    {
                        comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = txtNombre.Text.Trim();
                        comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = txtDireccion.Text.Trim();
                        comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = txtTelefono.Text.Trim();
                        connection.Open();
                        comando.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Centro de salud guardado exitosamente", "Nuevo Centro de Salud"
                                , MessageBoxButtons.OK, MessageBoxIcon.Information);
                mostrarCentros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreBuscar.Text.Trim() == "")
                {
                    MessageBox.Show("Ingrese el nombre del centro a eliminar");
                    return;
                }

                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el centro de salud?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Conexion cn = new Conexion();
                    using (SqlConnection connection = cn.ObtenerConexion())
                    {
                        string sql = @"DELETE FROM CENTRO_SALUD WHERE nombre = @nombre";
                        using (SqlCommand comando = new SqlCommand(sql, connection))
                        {
                            comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = txtNombreBuscar.Text.Trim();
                            connection.Open();
                            int rowsAffected = comando.ExecuteNonQuery();
                            
                            MessageBox.Show("Centro de salud eliminado exitosamente", "Eliminar Centro de Salud",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mostrarCentros();
                            txtNombreBuscar.Clear();
                            txtNombre.Clear();
                            txtDireccion.Clear();
                            txtTelefono.Clear();                         
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el centro: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtNombreBuscar.Text.Trim()))
                {
                    MessageBox.Show("Ingrese el nombre del centro a actualizar");
                    return;
                }

                Conexion cn = new Conexion();

                using(SqlConnection connection = cn.ObtenerConexion())
                {
                    string sql = @"UPDATE CENTRO_SALUD SET nombre = @nombre, direccion = @direccion, 
                    telefono = @telefono WHERE nombre = @nombreBuscar";
                    using (SqlCommand comando = new SqlCommand(sql, connection))
                    {
                        comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = txtNombre.Text.Trim();
                        comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = txtDireccion.Text.Trim();
                        comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = txtTelefono.Text.Trim();
                        comando.Parameters.Add("@nombreBuscar", SqlDbType.VarChar).Value = txtNombreBuscar.Text.Trim();
                        connection.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        if(rowsAffected > 0)
                        {
                            MessageBox.Show("Centro de salud actualizado exitosamente", "Actualizar Centro de Salud",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mostrarCentros();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el centro para actualizar");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el centro: " + ex.Message);
            }
        }
    }
}
