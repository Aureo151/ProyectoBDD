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
    public partial class frmRegistroIncidenciasEquipo : Form
    {
        Conexion con = new Conexion();
        private int idUsuarioLogueado = 1; 
        public frmRegistroIncidenciasEquipo()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void frmRegistroIncidenciasEquipo_Load(object sender, EventArgs e)
        {
            CargarEquipos();
            CargarIncidenciasEquipo();
            Estilos.AplicarEstilo(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Ingrese una descripción de la incidencia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un equipo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            try
            {
                using (SqlConnection conn = con.ObtenerConexion())
                {
                   
                    string query = @"INSERT INTO INCIDENCIA_EQUIPO 
                                 (descripcion, fecha, tipo, id_equipo)
                                 VALUES 
                                 (@descripcion, @fecha, @tipo, @id_equipo)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox2.Text.Trim();
                        cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = dateTimePicker1.Value.Date;

                        
                        cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value =
                            string.IsNullOrWhiteSpace(textBox1.Text) ? (object)DBNull.Value : textBox1.Text.Trim();

                        

                        
                        cmd.Parameters.Add("@id_equipo", SqlDbType.VarChar).Value = comboBox1.SelectedValue.ToString();

                        conn.Open();
                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("Incidencia de equipo registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarIncidenciasEquipo();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar la incidencia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void CargarEquipos()
        {
            try
            {
                using (SqlConnection conn = con.ObtenerConexion())
                {
                    string query = "SELECT codigo_equipo, nombre FROM EQUIPO";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        comboBox1.DataSource = dt;
                        comboBox1.DisplayMember = "nombre";
                        comboBox1.ValueMember = "codigo_equipo";
                        comboBox1.SelectedIndex = -1;

                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar equipos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarIncidenciasEquipo()
        {
            try
            {
                using (SqlConnection conn = con.ObtenerConexion())
                {
                    string query = @"
            SELECT 
                I.descripcion,
                I.fecha,
                I.tipo,
                E.nombre AS equipo
            FROM INCIDENCIA_EQUIPO I
            INNER JOIN EQUIPO E
                ON I.id_equipo = E.id_equipo";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar incidencias: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
