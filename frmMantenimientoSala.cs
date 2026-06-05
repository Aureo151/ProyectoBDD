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
    public partial class frmMantenimientoSala : Form
    {
        Conexion cn = new Conexion();
        public frmMantenimientoSala()
        {
            InitializeComponent();
        }
        private void CargarEspacios()
        {
            try
            {
                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"SELECT id_espacio, nombre FROM ESPACIO";
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);
                        cmbEspacio.DataSource = dt;
                        cmbEspacio.DisplayMember = "nombre";
                        cmbEspacio.ValueMember = "id_espacio";
                        cmbEspacio.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los espacios: " + ex.Message);
            }
        }
        private void CargarMantenimientos()
        {
            try
            {
                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"SELECT m.id_mantenimiento_espacio, e.nombre AS espacio, m.fecha_mantenimiento, m.descripcion 
                                     FROM MANTENIMIENTO_ESPACIO  m
                                     INNER JOIN ESPACIO e ON m.id_espacio = e.id_espacio";
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);
                        dgvMantenimientosEspacios.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los mantenimientos: " + ex.Message);
            }
        }
        private void LimpiarCampos()
        {
            txtDescripcion.Clear();
            txtResponsable.Clear();
            txtTipo.Clear();
        }
        private void frmMantenimientoSala_Load(object sender, EventArgs e)
        {
            CargarEspacios();
           
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbEspacio.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un espacio.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || string.IsNullOrWhiteSpace(txtResponsable.Text) || string.IsNullOrEmpty(txtTipo.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            try
            {
                using(SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"INSERT INTO MANTENIMIENTO_ESPACIO  (id_espacio, fecha, descripcion, responsable, tipo) 
                                     VALUES (@id_espacio, @fecha, @descripcion, @responsable, @tipo)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_espacio", cmbEspacio.SelectedValue);
                        cmd.Parameters.AddWithValue("@fecha", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                        cmd.Parameters.AddWithValue("@responsable", txtResponsable.Text);
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Mantenimiento agregado exitosamente");
                    CargarMantenimientos();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el mantenimiento: " + ex.Message);
            }
        }
    }
}
