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
    public partial class frmEspacios : Form
    {
        public frmEspacios()
        {
            InitializeComponent();
        }

        private void cargarCentrosSalud()
        {
            try
            {
                Conexion cn = new Conexion();

                using (SqlConnection conexion = cn.ObtenerConexion())
                {
                    string sql = "SELECT id_centro, nombre FROM CENTRO_SALUD";

                    using(SqlDataAdapter adapter = new SqlDataAdapter(sql, conexion))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        
                        cmbCentro.DataSource = null; // Limpiar cualquier enlace previo

                        cmbCentro.DataSource = dataTable;
                        cmbCentro.DisplayMember = "nombre";
                        cmbCentro.ValueMember = "id_centro";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar centros de salud: " + ex.Message);
            }
        }

        private void mostrarEspacios()
        {
            try
            {
                Conexion cn = new Conexion();
                using (SqlConnection conexion = cn.ObtenerConexion())
                {
                    string sql = "SELECT e.id_espacio, e.nombre, c.nombre AS centro FROM ESPACIO e JOIN CENTRO_SALUD c ON e.id_centro = c.id_centro";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexion))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar espacios: " + ex.Message);
            }
        }
        private void frmEspacios_Load(object sender, EventArgs e)
        {
            cargarCentrosSalud();
            mostrarEspacios();
            cmbTipo.SelectedIndex = 0;
        }
        private void LimpiarCampos()
        {
            txtNombreBuscar.Clear();
            cmbTipo.SelectedIndex = 0;
            cmbCentro.SelectedIndex = 0;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexion cn = new Conexion();

                using(SqlConnection conexion = cn.ObtenerConexion())
                {
                    conexion.Open();

                    SqlTransaction transaction = conexion.BeginTransaction();

                    try
                    {
                        string tipoEspacio = cmbTipo.SelectedItem.ToString();

                        int idCentro = Convert.ToInt32(cmbCentro.SelectedValue);

                        string sqlEspacio = "INSERT INTO ESPACIO (nombre, tipo, " +
                            "id_centro) VALUES (@nombre, @tipo, @id_centro); " +
                            "SELECT SCOPE_IDENTITY()";

                        int idEspacioNuevo;

                        using (SqlCommand cmdEspacio = new SqlCommand(sqlEspacio, conexion, transaction))
                        {
                            cmdEspacio.Parameters.AddWithValue("@nombre", txtNombreBuscar.Text.Trim());
                            cmdEspacio.Parameters.AddWithValue("@tipo", tipoEspacio);
                            cmdEspacio.Parameters.AddWithValue("@id_centro", idCentro);
                            idEspacioNuevo = Convert.ToInt32(cmdEspacio.ExecuteScalar());
                        }

                        string tablaTipo = "";

                        if(tipoEspacio == "Almacenes")
                        tablaTipo = "ALMACENES";
                        else if(tipoEspacio == "Consultorios")
                        tablaTipo = "CONSULTORIOS"; 
                        else if(tipoEspacio == "Laboratorio")
                        tablaTipo = "LABORATORIO"; 
                        else if(tipoEspacio =="Oficinas")
                        tablaTipo = "OFICINAS"; 
                        else if(tipoEspacio == "Sala de Emergencia")
                        tablaTipo = "SALA_EMERGENCIA"; 

                        string sqlTipo = $"INSERT INTO {tablaTipo} (id_espacio) VALUES (@id_espacio)";

                        using(SqlCommand cmdTipo = new SqlCommand(sqlTipo, conexion, transaction))
                        {
                            cmdTipo.Parameters.Add("@id_espacio", SqlDbType.Int).Value = idEspacioNuevo;
                            cmdTipo.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Espacio guardado exitosamente.");
                        mostrarEspacios();
                        LimpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error al guardar el espacio: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
