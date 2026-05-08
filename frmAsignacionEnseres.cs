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
    public partial class frmAsignacionEnseres : Form
    {
        Conexion con = new Conexion();
        public frmAsignacionEnseres()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CargarEspacios()
        {
            using (SqlConnection conn = con.ObtenerConexion())
            {
                string query = "SELECT id_espacio, nombre FROM ESPACIO";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                cmbEspacio.DataSource = dt;
                cmbEspacio.DisplayMember = "nombre";
                cmbEspacio.ValueMember = "id_espacio";
            }
        }



        private void frmAsignacionEnseres_Load(object sender, EventArgs e)
        {
            CargarEspacios();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigoEquipo.Text.Trim()))
                {
                    MessageBox.Show("Complete el campo del codigo de quipo a asignar.");
                    return;
                }
                if (cmbEspacio.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un espacio para asignar el equipo.");
                    return;
                }
                using (SqlConnection conn = con.ObtenerConexion())
                {
                    string query = @"UPDATE EQUIPO SET nombre = @nombre, marca = @marca, modelo = @modelo, numero_serie = @serie, id_espacio = @id_espacio, estado = @estado
                         WHERE codigo_equipo = @codigo";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value =
                            txtCodigoEquipo.Text.Trim();

                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value =
                            txtNombre.Text.Trim();

                        cmd.Parameters.Add("@marca", SqlDbType.VarChar).Value =
                            txtMarca.Text.Trim();

                        cmd.Parameters.Add("@modelo", SqlDbType.VarChar).Value =
                            txtModelo.Text.Trim();

                        cmd.Parameters.Add("@serie", SqlDbType.VarChar).Value =
                            txtNumero.Text.Trim();


                        cmd.Parameters.Add("@id_espacio", SqlDbType.Int).Value =
                            cmbEspacio.SelectedValue;


                        cmd.Parameters.Add("@estado", SqlDbType.VarChar).Value =
                            txtEstado.Text.Trim();

                        conn.Open();

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("Equipo registrado correctamente.",
                                        "Registro",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                            Limpiar();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar el equipo. Verifique el código.",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el equipo: " + ex.Message);
            }
        }

       private void Limpiar()
        {
            txtCodigoEquipo.Clear();
            txtNombre.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtNumero.Clear();
            cmbEspacio.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(txtCodigoEquipo.Text))
                {
                    MessageBox.Show("Ingrese un código.");
                    return;
                }

                using (SqlConnection conn = con.ObtenerConexion())
                {
                    string query = @"SELECT id_equipo, nombre, marca, estado, modelo,numero_serie
                         FROM EQUIPO
                         WHERE codigo_equipo = @codigo";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value =
                            txtCodigoEquipo.Text.Trim();
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtNumero.Text = reader["numero_serie"].ToString();
                                txtNombre.Text = reader["nombre"].ToString();
                                txtMarca.Text = reader["marca"].ToString();
                                txtEstado.Text = reader["estado"].ToString();
                                txtModelo.Text = reader["modelo"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Equipo no encontrado.");
                                Limpiar();
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
    }
}

