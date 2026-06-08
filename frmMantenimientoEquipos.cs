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
    public partial class frmMantenimientoEquipos : Form
    {
        Conexion cn = new Conexion();

        public frmMantenimientoEquipos()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CargarEquipos()
        {
            try
            {
                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"SELECT id_equipo, nombre FROM EQUIPO";

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);

                        cmbEquipo.DataSource = dt;
                        cmbEquipo.DisplayMember = "nombre";
                        cmbEquipo.ValueMember = "id_equipo";
                        cmbEquipo.SelectedIndex = -1;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los equipos: " + ex.Message);
            }
        }

        private void frmMantenimientoEquipos_Load(object sender, EventArgs e)
        {
            CargarEquipos();
            CargarMantenimientos();
            Estilos.AplicarEstilo(this);
        }
        private void LimpiarCampos()
        {
            txtResponsable.Clear();
            txtDescripcion.Clear();
            txtTipo.Clear();
            cmbEquipo.SelectedIndex = -1;
        }

        private void CargarMantenimientos()
        {
            try
            {
                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"SELECT m.id_mantenimiento_equipo, e.nombre AS equipo, m.fecha, m.responsable, m.descripcion, m.tipo
                                 FROM MANTENIMIENTO_EQUIPO m
                                 INNER JOIN EQUIPO e ON m.id_equipo = e.id_equipo";
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los mantenimientos: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbEquipo.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un equipo.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtResponsable.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }
            try
            {
                using (SqlConnection conn = cn.ObtenerConexion())
                {
                    string query = @"INSERT INTO MANTENIMIENTO_EQUIPO (id_equipo, fecha, responsable, descripcion, tipo) 
                                 VALUES (@id_equipo, @fecha, @responsable, @descripcion, @tipo)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_equipo", cmbEquipo.SelectedValue);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@responsable", txtResponsable.Text.Trim());
                        cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Mantenimiento agregado exitosamente.");
                LimpiarCampos();
                CargarMantenimientos();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el mantenimiento: " + ex.Message);

            }
        }
    }
}
