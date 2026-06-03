using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private  readonly HttpClient cliente = new HttpClient();

        private readonly Dictionary<string, string> usuarios = new Dictionary<string, string>()
    {
        { "admin", "1234" },
        { "operador", "2026" }
    };


        private string esp32Url = "http://172.17.220.44/datos";
        public Form1()
        {
            InitializeComponent();
            cliente.Timeout = TimeSpan.FromSeconds(8);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text.Trim();
            string contrasena = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Ingrese usuario y contrasena.");
                return;
            }

            string claveCorrecta;

            if (usuarios.TryGetValue(usuario, out claveCorrecta) && claveCorrecta == contrasena)
            {
                int conectadoESP32 = await LeerESP32();

                if (conectadoESP32 > 0)
                {
                    MessageBox.Show("ESP32 conectado");
                }

                frmFormularioPrincipal principal =
                        new frmFormularioPrincipal(usuario, esp32Url);

                    principal.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "Login correcto, pero no se pudo conectar con el ESP32.",
                        "ESP32 sin conexion",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            
        

        private async System.Threading.Tasks.Task<bool> EnviarEstadoSistema(int estado)
        {
            try
            {
                string url = esp32Url + "/control?estado=" + estado;

                MessageBox.Show("Intentando conectar a:\n" + url);

                string respuesta = await cliente.GetStringAsync(url);

                MessageBox.Show("ESP32 respondio:\n" + respuesta);

                respuesta = respuesta.Trim().ToUpper();

                return respuesta == "OK" || respuesta == estado.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con ESP32:\n" + ex.Message);
                return false;
            }
        }
        private async Task<int> LeerESP32()
        {
            try
            {
                string respuesta = await cliente.GetStringAsync(esp32Url);
                return Convert.ToInt32(respuesta.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
