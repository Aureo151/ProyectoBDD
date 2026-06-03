using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Linq.Expressions;

namespace ProyectoAutomatas
{
    public partial class Form1 : Form
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private HttpClient clienteHTTP = new HttpClient();
        private Timer timerRFIDWifi = new Timer();
        private string ipESP32 = "";
        private bool peticionEnCurso = false;
        private SerialPort puertoRFID;

        private readonly Dictionary<string, string> usuarios = new Dictionary<string, string>()
    {
        { "admin", "1234" },
        { "operador", "2026" }
    };

        private readonly HashSet<string> tarjetasAutorizadas = new HashSet<string>()
    {
        "A1B2C3",
        "RFID2026",
        "CARD01"
    };

        public Form1()
        {
            InitializeComponent();
            timerRFIDWifi.Interval = 1000;
            timerRFIDWifi.Tick += TimerRFIDWifi_Tick;
            clienteHTTP.Timeout = TimeSpan.FromSeconds(3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarPuertos();
            ActualizarBotones(modoWifi: false, conectado: false);
        }

        // ─── LOGIN ────────────────────────────────────────────────────────────────

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text.Trim();
            string contraseña = textBox2.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                label5.Text = "Estado: ingrese usuario y contraseña.";
                return;
            }

            if (usuarios.TryGetValue(usuario, out string claveCorrecta) && claveCorrecta == contraseña)
            {
                label5.Text = $"Estado: acceso concedido ({usuario})";
                AbrirFormularioPrincipal(usuario);
            }
            else
            {
                label5.Text = "Estado: usuario o contraseña incorrectos.";
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ─── RFID SERIAL ─────────────────────────────────────────────────────────

        private void CargarPuertos()
        {
            comboBox1.Items.Clear();
            string[] puertos = SerialPort.GetPortNames();
            foreach (string p in puertos)
                comboBox1.Items.Add(p);

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (puertoRFID != null && puertoRFID.IsOpen)
            {
                puertoRFID.Close();
                puertoRFID.Dispose();
                puertoRFID = null;
                label5.Text = "Estado: lector RFID desconectado.";
                button2.Text = "Conectar RFID Serial";
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un puerto COM.");
                return;
            }

            try
            {
                string puerto = comboBox1.SelectedItem.ToString();
                puertoRFID = new SerialPort(puerto, 9600) { NewLine = "\n" };
                puertoRFID.DataReceived += PuertoRFID_DataReceived;
                puertoRFID.Open();
                label5.Text = $"Estado: lector RFID conectado en {puerto}";
                button2.Text = "Desconectar RFID Serial";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar al lector RFID.\n\n" + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PuertoRFID_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string codigoRFID = puertoRFID.ReadLine().Trim().ToUpper();
                this.Invoke(new Action(() => ValidarRFID(codigoRFID)));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                    label5.Text = "Error al leer RFID: " + ex.Message));
            }
        }

        // ─── RFID WIFI (ESP32) ───────────────────────────────────────────────────

        private async void button4_Click(object sender, EventArgs e)
        {
            if (timerRFIDWifi.Enabled)
            {
                DetenerWifi();
                return;
            }

            string ip = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("Ingrese la IP del ESP32.", "IP requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EsIPValida(ip))
            {
                MessageBox.Show("La IP ingresada no tiene un formato válido.\nEjemplo: 192.168.1.100",
                    "IP inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ipESP32 = ip;

            if (puertoRFID != null && puertoRFID.IsOpen)
            {
                puertoRFID.Close();
                puertoRFID.Dispose();
                puertoRFID = null;
                button2.Text = "Conectar RFID Serial";
            }

            try
            {
                using (HttpClient clientePrueba = new HttpClient())
                {
                    clientePrueba.Timeout = TimeSpan.FromSeconds(3);

                    // ✅ Usa /datos que es la única ruta que tiene tu ESP32
                    string respuesta = await clientePrueba.GetStringAsync($"http://{ipESP32}/datos");

                    if (!string.IsNullOrEmpty(respuesta))
                    {
                        timerRFIDWifi.Start();
                        button4.Text = "Detener WiFi";
                        label5.Text = $"Estado: conectado a {ipESP32}";
                    }
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("No se pudo conectar: tiempo de espera agotado.",
                    "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("No se pudo conectar al ESP32:\n" + ex.Message,
                    "Error de red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void TimerRFIDWifi_Tick(object sender, EventArgs e)
        {
            if (peticionEnCurso) return;
            peticionEnCurso = true;

            try
            {
                // ✅ Consulta /datos cada segundo para mantener conexión activa
                string respuesta = await clienteHTTP.GetStringAsync($"http://{ipESP32}/datos");
                label5.Text = $"ESP32 activo ({DateTime.Now:HH:mm:ss})";
            }
            catch (TaskCanceledException) { label5.Text = "ESP32 no responde (timeout)."; }
            catch (HttpRequestException) { label5.Text = "Sin conexión con " + ipESP32 + "."; }
            catch (Exception ex) { label5.Text = "Error WiFi: " + ex.Message; }
            finally
            {
                peticionEnCurso = false;
            }
        }

        // ─── VALIDACIÓN RFID Y ACCESO ─────────────────────────────────────────────

        private void ValidarRFID(string codigoRFID)
        {
            label5.Text = $"Estado: tarjeta detectada → {codigoRFID}";

            if (tarjetasAutorizadas.Contains(codigoRFID))
            {
                label5.Text = $"Estado: acceso concedido ({codigoRFID})";
                AbrirFormularioPrincipal("RFID");
            }
            else
            {
                label5.Text = $"Estado: tarjeta no autorizada ({codigoRFID})";
                MessageBox.Show($"Tarjeta RFID no autorizada:\n{codigoRFID}", "Acceso denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (!string.IsNullOrEmpty(ipESP32))
                    timerRFIDWifi.Start();
            }
        }

        private void AbrirFormularioPrincipal(string usuarioOrigen)
        {
            bool wifiActivo = timerRFIDWifi.Enabled;

            frmFormularioPrincipal formulario = new frmFormularioPrincipal(usuarioOrigen, ipESP32);

            formulario.FormClosed += (s, ev) =>
            {
                this.Show();
                if (wifiActivo && !string.IsNullOrEmpty(ipESP32))
                {
                    timerRFIDWifi.Start();
                    button4.Text = "Detener WiFi";
                    label5.Text = $"Estado: conectado a {ipESP32}";
                }
            };

            formulario.Show();
            this.Hide();
        }

        private void DetenerWifi()
        {
            timerRFIDWifi.Stop();
            button4.Text = "Conectar WiFi ESP32";
            label5.Text = "Estado: monitoreo WiFi detenido.";
        }

        private bool EsIPValida(string ip)
        {
            if (System.Net.IPAddress.TryParse(ip, out _)) return true;
            return System.Text.RegularExpressions.Regex.IsMatch(ip,
                @"^[a-zA-Z0-9]([a-zA-Z0-9\-\.]{0,61}[a-zA-Z0-9])?$");
        }

        private void ActualizarBotones(bool modoWifi, bool conectado)
        {
            button2.Text = conectado && !modoWifi ? "Desconectar RFID Serial" : "Conectar RFID Serial";
            button4.Text = conectado && modoWifi ? "Detener WiFi" : "Conectar WiFi ESP32";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerRFIDWifi.Stop();
            puertoRFID?.Close();
            clienteHTTP.Dispose();
            base.OnFormClosing(e);
        }
    }
}
