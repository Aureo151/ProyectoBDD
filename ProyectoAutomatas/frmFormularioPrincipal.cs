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
using System.Web.Script.Serialization;

namespace ProyectoAutomatas
{
    
    public partial class frmFormularioPrincipal : Form
    {
        public class RespuestaTanques
        {
            public int t1 { get; set; }
            public int t2 { get; set; }
            public int t3 { get; set; }
        }

        public class RespuestaComando
        {
            public string status { get; set; }
        }

        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private string usuario;
        private HttpClient clienteHTTP = new HttpClient();
        private Timer timerSensores = new Timer();
        private string ipESP32 = "";

        private int nivelTanque1 = 0;
        private int nivelTanque2 = 0;
        private int nivelTanque3 = 0;

        public frmFormularioPrincipal(string usuarioActivo, string ip)
        {
            InitializeComponent();
            usuario = usuarioActivo;
            ipESP32 = ip;

            clienteHTTP.Timeout = TimeSpan.FromSeconds(3);

            timerSensores.Interval = 2000;
            timerSensores.Tick += TimerSensores_Tick;
        }

        private void frmFormularioPrincipal_Load(object sender, EventArgs e)
        {
            label4.Text = $"Usuario: {usuario}";

            if (!string.IsNullOrEmpty(ipESP32))
            {
                timerSensores.Start();
                label4.Text = $"Conectado al ESP32: {ipESP32}";
            }
            else
            {
                label4.Text = "Sin conexión WiFi — modo manual";
                nivelTanque1 = 100;
                nivelTanque2 = 100;
                nivelTanque3 = 100;
                ActualizarPantalla();
            }
        }

        // ─── POLLING DE SENSORES ──────────────────────────────────────────────────

        private async void TimerSensores_Tick(object sender, EventArgs e)
        {
            await LeerNivelesTanques();
        }

        private async Task LeerNivelesTanques()
        {
            try
            {
                string url = "http://" + ipESP32 + "/tanques";
                string respuesta = await clienteHTTP.GetStringAsync(url);

                RespuestaTanques datos = serializer.Deserialize<RespuestaTanques>(respuesta);

                if (datos != null)
                {
                    nivelTanque1 = Math.Max(0, Math.Min(100, datos.t1));
                    nivelTanque2 = Math.Max(0, Math.Min(100, datos.t2));
                    nivelTanque3 = Math.Max(0, Math.Min(100, datos.t3));
                }

                label4.Text = "Última actualización: " + DateTime.Now.ToString("HH:mm:ss");
                ActualizarPantalla();
            }
            catch (TaskCanceledException)
            {
                label4.Text = "ESP32 no responde (timeout).";
            }
            catch (HttpRequestException)
            {
                label4.Text = "Sin conexión con " + ipESP32 + ".";
            }
            catch (Exception ex)
            {
                label4.Text = "Error al leer sensores: " + ex.Message;
            }
        }

        // ─── PANTALLA ─────────────────────────────────────────────────────────────

        private void ActualizarPantalla()
        {
            label1.Text = $"Tanque 1 - Agua: {nivelTanque1}%";
            label2.Text = $"Tanque 2 - Jugo: {nivelTanque2}%";
            label3.Text = $"Tanque 3 - Gaseosa: {nivelTanque3}%";

            progressBar1.Value = Math.Max(0, Math.Min(100, nivelTanque1));
            progressBar2.Value = Math.Max(0, Math.Min(100, nivelTanque2));
            progressBar3.Value = Math.Max(0, Math.Min(100, nivelTanque3));

            button1.Enabled = nivelTanque1 > 0;
            button2.Enabled = nivelTanque2 > 0;
            button3.Enabled = nivelTanque3 > 0;
        }

        // ─── DISPENSAR ────────────────────────────────────────────────────────────

        private void button1_Click(object sender, EventArgs e) => Dispensar(1);
        private void button2_Click(object sender, EventArgs e) => Dispensar(2);
        private void button3_Click(object sender, EventArgs e) => Dispensar(3);

        private async void Dispensar(int tanque)
        {
            const int cantidad = 10;
            int nivel;
            string nombre;

            switch (tanque)
            {
                case 1: nivel = nivelTanque1; nombre = "Agua"; break;
                case 2: nivel = nivelTanque2; nombre = "Jugo"; break;
                default: nivel = nivelTanque3; nombre = "Gaseosa"; break;
            }

            if (nivel < cantidad)
            {
                MessageBox.Show($"Nivel insuficiente en Tanque {tanque} ({nombre}).\nNivel actual: {nivel}%",
                    "Sin líquido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Deshabilitar botones mientras se procesa
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            label4.Text = $"Dispensando {nombre}...";

            bool exito = await EnviarComandoDispensar(tanque, cantidad);

            if (exito)
            {
                switch (tanque)
                {
                    case 1: nivelTanque1 = Math.Max(0, nivelTanque1 - cantidad); break;
                    case 2: nivelTanque2 = Math.Max(0, nivelTanque2 - cantidad); break;
                    case 3: nivelTanque3 = Math.Max(0, nivelTanque3 - cantidad); break;
                }

                label4.Text = $"✓ {nombre} dispensado ({DateTime.Now:HH:mm:ss})";
            }
            else
            {
                label4.Text = $"✗ Error al dispensar {nombre}.";
                MessageBox.Show($"No se pudo enviar el comando al ESP32.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ActualizarPantalla();
        }

        private async Task<bool> EnviarComandoDispensar(int tanque, int cantidad)
        {
            // Sin WiFi: simular dispensado exitoso (modo manual)
            if (string.IsNullOrEmpty(ipESP32))
                return true;

            try
            {
                string url = $"http://{ipESP32}/dispensar?tanque={tanque}&cantidad={cantidad}";
                string respuesta = await clienteHTTP.GetStringAsync(url);

                RespuestaComando datos = serializer.Deserialize<RespuestaComando>(respuesta);
                return datos != null && datos.status?.Trim().ToUpper() == "OK";
            }
            catch
            {
                return false;
            }
        }

        // ─── BOTÓN ACTUALIZAR SENSORES ────────────────────────────────────────────

        private async void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ipESP32))
            {
                MessageBox.Show("No hay conexión WiFi activa.", "Sin conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            label4.Text = "Actualizando sensores...";
            button4.Enabled = false;

            await LeerNivelesTanques();

            button4.Enabled = true;
            MessageBox.Show("Sensores actualizados.", "Estado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ─── BOTÓN APAGAR ─────────────────────────────────────────────────────────

        private void button5_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Desea cerrar sesión?", "AutoDrink",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                timerSensores.Stop();
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerSensores.Stop();
            clienteHTTP.Dispose();
            base.OnFormClosing(e);
        }
    }
}
