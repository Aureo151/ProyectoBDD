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
            txtID.Clear();
            txtNombre.Clear();
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
                            cmdEspacio.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                            cmdEspacio.Parameters.AddWithValue("@tipo", tipoEspacio);
                            cmdEspacio.Parameters.AddWithValue("@id_centro", idCentro);
                            idEspacioNuevo = Convert.ToInt32(cmdEspacio.ExecuteScalar());
                        }

                        string tablaTipo = "";

                        if(tipoEspacio == "Almacenes")
                        tablaTipo = "ALMACENES";
                        else if(tipoEspacio == "Consultorio")
                        tablaTipo = "CONSULTORIO"; 
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtNombreBuscar.Text.Trim()))
                {
                    MessageBox.Show("Ingrese un nombre para buscar.");
                    return;
                }

                Conexion cn = new Conexion();

                using(SqlConnection connection = cn.ObtenerConexion())
                {
                    string sql = "SELECT id_espacio, nombre, tipo, id_centro FROM ESPACIO WHERE nombre LIKE @nombre";
                    
                    using(SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@nombre", "%" + txtNombreBuscar.Text.Trim() + "%");
                        connection.Open();
                        
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                txtID.Text = reader["id_espacio"].ToString();
                                txtNombre.Text = reader["nombre"].ToString();
                                cmbTipo.SelectedItem = reader["tipo"].ToString();
                                cmbCentro.SelectedValue = Convert.ToInt32(reader["id_centro"]);
                            }
                            else
                            {
                                MessageBox.Show("No se encontró ningún espacio con ese nombre.");
                                LimpiarCampos();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar espacios: " + ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtNombreBuscar.Text.Trim()))
                {
                    MessageBox.Show("Ingrese un nombre para eliminar.");
                    return;
                }
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el espacio?", 
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    Conexion cn = new Conexion();

                    using (SqlConnection conexion = cn.ObtenerConexion())
                    {
                        string sql = "DELETE FROM ESPACIO WHERE nombre = @nombre";

                        using (SqlCommand cmd = new SqlCommand(sql, conexion))
                        {
                            conexion.Open();

                            SqlTransaction transaction = conexion.BeginTransaction();

                            try
                            {
                                int IdEspacio = Convert.ToInt32(txtID.Text);
                                string TipoEspacio = cmbTipo.SelectedItem.ToString();

                                string tablaTipo = "";

                                if (TipoEspacio == "Almacenes")
                                    tablaTipo = "ALMACENES";
                                else if (TipoEspacio == "Consultorio")
                                    tablaTipo = "CONSULTORIO";
                                else if (TipoEspacio == "Laboratorio")
                                    tablaTipo = "LABORATORIO";
                                else if (TipoEspacio == "Oficinas")
                                    tablaTipo = "OFICINAS";
                                else if (TipoEspacio == "Sala de Emergencia")
                                    tablaTipo = "SALA_EMERGENCIA";

                                if (tablaTipo == "")
                                {
                                    MessageBox.Show("Tipo de espacio no válido: " + TipoEspacio);
                                    transaction.Rollback();
                                    return;
                                }

                                string sqlTipo = $"DELETE FROM {tablaTipo} WHERE id_espacio = @id_espacio";

                                using(SqlCommand cmdTipo = new SqlCommand(sqlTipo, conexion, transaction))
                                {
                                    cmdTipo.Parameters.Add("@id_espacio", SqlDbType.Int).Value = IdEspacio;
                                    cmdTipo.ExecuteNonQuery();
                                }

                                string sqlEspacio = "DELETE FROM ESPACIO WHERE id_espacio = @id_espacio";

                                using (SqlCommand cmdEspacio = new SqlCommand(sqlEspacio, conexion, transaction))
                                {
                                    cmdEspacio.Parameters.Add("@id_espacio", SqlDbType.Int).Value = IdEspacio;
                                    cmdEspacio.ExecuteNonQuery();
                                }

                                transaction.Commit();

                                MessageBox.Show("Espacio eliminado correctamente.");

                                mostrarEspacios();
                                LimpiarCampos();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Error al eliminar el espacio: " + ex.Message);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el espacio: " + ex.Message);
            }
        }
    }
}
