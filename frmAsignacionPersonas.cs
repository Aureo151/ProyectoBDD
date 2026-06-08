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
    public partial class frmAsignacionPersonas : Form
    {
        public frmAsignacionPersonas()
        {
            InitializeComponent();
        }

        private void CargarResponsables()
        {
            try
            {
                Conexion cn = new Conexion();

                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"SELECT id_responsable,
                                    codigo_responsable + ' - ' + nombre AS Responsable
                             FROM RESPONSABLE";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    cmbResponsable.DataSource = dt;
                    cmbResponsable.DisplayMember = "Responsable";
                    cmbResponsable.ValueMember = "id_responsable";
                    cmbResponsable.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar responsables: " + ex.Message);
            }
        }

        private void CargarEspacios()
        {
            try
            {
                Conexion cn = new Conexion();

                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"SELECT id_espacio,
                                    nombre
                             FROM ESPACIO";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    cmbEspacio.DataSource = dt;
                    cmbEspacio.DisplayMember = "nombre";
                    cmbEspacio.ValueMember = "id_espacio";
                    cmbEspacio.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar espacios: " + ex.Message);
            }
        }
        private void MostrarAsignaciones()
        {
            try
            {
                Conexion cn = new Conexion();

                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"
            SELECT
                E.nombre AS Espacio,
                R.codigo_responsable,
                R.nombre AS Responsable
            FROM ESPACIO E
            LEFT JOIN RESPONSABLE R
                ON E.id_responsable = R.id_responsable";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar asignaciones: " + ex.Message);
            }
        }
        private void frmAsignacionPersonas_Load(object sender, EventArgs e)
        {
            CargarResponsables();
            CargarEspacios();
            MostrarAsignaciones();
            Estilos.AplicarEstilo(this);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbResponsable.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un responsable.");
                    return;
                }

                if (cmbEspacio.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un espacio.");
                    return;
                }

                Conexion cn = new Conexion();

                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"UPDATE ESPACIO
                             SET id_responsable = @id_responsable
                             WHERE id_espacio = @id_espacio";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@id_responsable", SqlDbType.Int).Value =
                            Convert.ToInt32(cmbResponsable.SelectedValue);

                        cmd.Parameters.Add("@id_espacio", SqlDbType.Int).Value =
                            Convert.ToInt32(cmbEspacio.SelectedValue);

                        conn.Open();

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("Responsable asignado correctamente al espacio.");

                            MostrarAsignaciones();                          
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el espacio seleccionado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asignar responsable: " + ex.Message);
            }
        }
    }
}
