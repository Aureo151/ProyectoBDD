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
        SqlConnection conexion = new SqlConnection("Server=localhost;Database=CentroMedico;Trusted_Connection=True;");

        int idCentro = 0;
        public frmCentroSalud()
        {
            InitializeComponent();
        }
        private void mostrarCentros()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM CENTRO_SALUD", conexion);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            dgvCentros.DataSource = dataTable;
        }
        private void frmCentroSalud_Load(object sender, EventArgs e)
        {

        }

        private void frmCentroSalud_Load_1(object sender, EventArgs e)
        {
            mostrarCentros();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO CENTRO_SALUD (nombre, direccion) VALUES (@nombre, @direccion)", conexion);
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);

                Conexion cn = new Conexion();
                cmd.ExecuteNonQuery();
                conexion.Close();

                MessageBox.Show("Centro de salud guardado correctamente.");

                mostrarCentros();

                Limpiar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el centro de salud: " + ex.Message);
                conexion.Close();
            }
        }
        private void Limpiar()
        {
            txtID.Clear();
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";

            idCentro = 0;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
