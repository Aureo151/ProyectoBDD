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
    public partial class frmRegistroIncidencias : Form
    {
        Conexion con = new Conexion();
        public frmRegistroIncidencias()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Ingrese una descripción.");
                    return;
                }

                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un espacio.");
                    return;
                }

                

                using (SqlConnection conn = con.ObtenerConexion())
                {
                    string query = @"INSERT INTO INCIDENCIA_ESPACIO
                                    (
                                        descripcion,
                                        fecha,
                                        tipo,
                                        
                                        id_espacio
                                    )
                                    VALUES
                                    (
                                        @descripcion,
                                        @fecha,
                                        @tipo,
                                       
                                        @id_espacio
                                    )";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value =
                            textBox1.Text.Trim();

                        cmd.Parameters.Add("@fecha", SqlDbType.Date).Value =
                            dateTimePicker1.Value.Date;

                        cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value =
                            textBox2.Text;

                        

                        cmd.Parameters.Add("@id_espacio", SqlDbType.Int).Value =
                            comboBox1.SelectedValue;

                        conn.Open();

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("Incidencia registrada correctamente.");

                          
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void frmRegistroIncidencias_Load(object sender, EventArgs e)
        {
            CargarEspacios();
            CargarIncidencias();
        }

        private void CargarEspacios()
        {
            using (SqlConnection conn = con.ObtenerConexion())
            {
                string query = "SELECT id_espacio, nombre FROM ESPACIO";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "nombre";
                comboBox1.ValueMember = "id_espacio";
                comboBox1.SelectedIndex = -1;
            }
        }

        private void CargarIncidencias()
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
                E.nombre AS espacio
            FROM INCIDENCIA_ESPACIO I
            INNER JOIN ESPACIO E
                ON I.id_espacio = E.id_espacio";

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

    }
}
