using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class frmFormularioPrincipal : Form
    {

        private Timer timerTanques = new Timer();
        private readonly HttpClient cliente = new HttpClient();

        private string usuario;
        private string esp32BaseUrl;

        private int nivelTanque1 = 100;
        private int nivelTanque2 = 100;
        private int nivelTanque3 = 100;
        private string usuarioActivo;
        private string urlESP32;

        public frmFormularioPrincipal(string usuario, string esp32Url)
        {
            InitializeComponent();
            usuarioActivo = usuario;
            esp32BaseUrl = esp32Url;

            cliente.Timeout = TimeSpan.FromSeconds(8);

            label4.Text = "Usuario: " + usuarioActivo + " | Sistema encendido";

            ActualizarPantalla();



        }

        private void frmFormularioPrincipal_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
           await Dispensar(1);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await Dispensar(2);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await Dispensar(3);
        }

        private async System.Threading.Tasks.Task Dispensar(int tanque)
        {
            int nivelActual = ObtenerNivel(tanque);

            if (nivelActual <= 0)
            {
                MessageBox.Show("El tanque seleccionado esta vacio.");
                return;
            }

            try
            {
                string url = esp32BaseUrl + "/dispensar?tanque=" + tanque;
                string respuesta = await cliente.GetStringAsync(url);

                respuesta = respuesta.Trim().ToUpper();

                if (respuesta == "OK" || respuesta == "1")
                {
                    MessageBox.Show("Dispensado del tanque " + tanque + " realizado.");

                    await LeerTanques();
                }
                else
                {
                    MessageBox.Show("El ESP32 no pudo dispensar. Respuesta: " + respuesta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al dispensar: " + ex.Message);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await LeerTanques();
        }
        private async System.Threading.Tasks.Task LeerTanques()
        {
            try
            {
                string url = esp32BaseUrl + "/tanques";
                string respuesta = await cliente.GetStringAsync(url);

                respuesta = respuesta.Trim();

                string[] partes = respuesta.Split(',');

                if (partes.Length == 3)
                {
                    int t1;
                    int t2;
                    int t3;

                    if (int.TryParse(partes[0], out t1) &&
                        int.TryParse(partes[1], out t2) &&
                        int.TryParse(partes[2], out t3))
                    {
                        nivelTanque1 = t1;
                        nivelTanque2 = t2;
                        nivelTanque3 = t3;

                        ActualizarPantalla();

                        label4.Text = "Usuario: " + usuario +
                                      " | Ultima actualizacion: " +
                                      DateTime.Now.ToString("HH:mm:ss");
                    }
                    else
                    {
                        MessageBox.Show("La respuesta de tanques no contiene numeros validos.");
                    }
                }
                else
                {
                    MessageBox.Show("Formato incorrecto. El ESP32 debe responder: 100,80,60");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer tanques: " + ex.Message);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            DialogResult opcion = MessageBox.Show(
            "Desea apagar el sistema?",
            "AutoDrink",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

            if (opcion == DialogResult.Yes)
            {
                bool apagado = await EnviarEstadoSistema(0);

                if (apagado)
                {
                    MessageBox.Show("Sistema apagado correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo apagar el sistema.");
                }
            }
        }

            private async System.Threading.Tasks.Task<bool> EnviarEstadoSistema(int estado)
        {
            try
            {
                string url = esp32BaseUrl + "/control?estado=" + estado;
                string respuesta = await cliente.GetStringAsync(url);

                respuesta = respuesta.Trim().ToUpper();

                return respuesta == "OK" || respuesta == estado.ToString();
            }
            catch
            {
                return false;
            }
        }

        private int ObtenerNivel(int tanque)
        {
            if (tanque == 1)
            {
                return nivelTanque1;
            }

            if (tanque == 2)
            {
                return nivelTanque2;
            }

            return nivelTanque3;
        }

        private void ActualizarPantalla()
        {
            label1.Text = "Tanque 1 - Agua: " + nivelTanque1 + "%";
            label2.Text = "Tanque 2 - Jugo: " + nivelTanque2 + "%";
            label3.Text = "Tanque 3 - Gaseosa: " + nivelTanque3 + "%";

            progressBar1.Value = Limitar(nivelTanque1);
            progressBar2.Value = Limitar(nivelTanque2);
            progressBar3.Value = Limitar(nivelTanque3);

            button1.Text = "Dispensar T1";
            button2.Text = "Dispensar T2";
            button3.Text = "Dispensar T3";
            button4.Text = "Actualizar";
            button5.Text = "Apagar";

            button1.Enabled = nivelTanque1 > 0;
            button2.Enabled = nivelTanque2 > 0;
            button3.Enabled = nivelTanque3 > 0;
        }

        private int Limitar(int valor)
        {
            if (valor < 0)
            {
                return 0;
            }

            if (valor > 100)
            {
                return 100;
            }

            return valor;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cliente.Dispose();
            base.OnFormClosing(e);
        }


        private void IniciarTimerTanques()
        {
            timerTanques.Stop();
            timerTanques.Tick -= TimerTanques_Tick;

            timerTanques.Interval = 10000;
            timerTanques.Tick += TimerTanques_Tick;
            timerTanques.Start();
        }

        private async void TimerTanques_Tick(object sender, EventArgs e)
        {
            await LeerTanques();
        }

        private void DetenerTimerTanques()
        {
            timerTanques.Stop();
            timerTanques.Tick -= TimerTanques_Tick;
        }

    }
}
