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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();
            SqlConnection conn = cn.ObtenerConexion();

            string query = @"SELECT id_usuario, id_rol
                     FROM USUARIO
                     WHERE nombre_usuario = @usuario 
                     AND contrasena = @password";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@usuario", textBox1.Text);
            cmd.Parameters.AddWithValue("@password", textBox2.Text);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int rol = Convert.ToInt32(dr["id_rol"]);

                FormAdministrador adminForm = new FormAdministrador(rol);
                adminForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }

        }
    }
}
