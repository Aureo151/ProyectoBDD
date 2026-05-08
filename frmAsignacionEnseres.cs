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
                using (SqlConnection conn = con.ObtenerConexion())
                {
                    string query = @"INSERT INTO EQUIPO
                    (
                        codigo_equipo,
                        nombre,
                        marca,
                        modelo,
                        numero_serie,
                        estado,
                        id_espacio
                    )
                    VALUES
                    (
                        @codigo,
                        @nombre,
                        @marca,
                        @modelo,
                        @serie,
                        @estado,
                        @id_espacio
                    )";

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

                        cmd.ExecuteNonQuery();


                        string sqlEspacio = "";

                        switch (cmbEspacio.Text)
                        {
                            case "Laboratorio":

                                sqlEspacio = @"INSERT INTO LABORATORIO(nombre)
                           VALUES(@nombre)";
                                break;

                            case "Oficina":

                                sqlEspacio = @"INSERT INTO OFICINAS(nombre)
                           VALUES(@nombre)";
                                break;

                            case "Consultorio":

                                sqlEspacio = @"INSERT INTO CONSULTORIO(nombre)
                           VALUES(@nombre)";
                                break;

                            case "Emergencia":

                                sqlEspacio = @"INSERT INTO SALA_EMERGENCIA(nombre)
                           VALUES(@nombre)";
                                break;
                        }

                        
                        if (sqlEspacio != "")
                        {
                            using (SqlCommand cmd2 = new SqlCommand(sqlEspacio, conn))
                            {
                                cmd2.Parameters.Add("@nombre", SqlDbType.VarChar).Value =
                                    txtNombre.Text.Trim();

                                cmd2.ExecuteNonQuery();
                            }


                            MessageBox.Show("Equipo registrado correctamente.",
                                        "Registro",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                            Limpiar();
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







    }
}

