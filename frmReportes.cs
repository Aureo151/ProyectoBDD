using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProyectoBDD
{
    public partial class frmReportes : Form
    {
        Conexion cn = new Conexion();

        public frmReportes()
        {
            InitializeComponent();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            Estilos.AplicarEstilo(this);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void EjecutarReporte(string consulta, params SqlParameter[] parametros)
        {
            try
            {
                using (SqlConnection conn = cn.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    lblTotal.Text = "Registros encontrados: " + dt.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message,
                    "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 1. Listado por centro de salud: número de espacios agrupados por categorías.
        private void btnEspaciosPorCentro_Click(object sender, EventArgs e)
        {
            string consulta = @"
                SELECT
                    CS.nombre AS [Centro de Salud],
                    E.tipo AS [Categoria del Espacio],
                    COUNT(E.id_espacio) AS [Cantidad]
                FROM CENTRO_SALUD CS
                INNER JOIN ESPACIO E
                    ON CS.id_centro = E.id_centro
                GROUP BY CS.nombre, E.tipo
                ORDER BY CS.nombre, E.tipo";

            EjecutarReporte(consulta);
        }

        // 2. Listado entre dos fechas: incidencias y mantenimientos registrados.
        private void btnEntreFechas_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = dtpInicio.Value.Date;
            DateTime fechaFin = dtpFin.Value.Date;

            if (fechaInicio > fechaFin)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.");
                return;
            }

            string consulta = @"
                SELECT
                    'Incidencia de Equipo' AS [Registro],
                    IE.fecha AS [Fecha],
                    IE.tipo AS [Tipo],
                    IE.descripcion AS [Descripcion],
                    EQ.codigo_equipo AS [Codigo Equipo],
                    EQ.nombre AS [Equipo o Espacio],
                    ES.nombre AS [Espacio],
                    CS.nombre AS [Centro de Salud]
                FROM INCIDENCIA_EQUIPO IE
                INNER JOIN EQUIPO EQ ON IE.id_equipo = EQ.id_equipo
                LEFT JOIN ESPACIO ES ON EQ.id_espacio = ES.id_espacio
                LEFT JOIN CENTRO_SALUD CS ON ES.id_centro = CS.id_centro
                WHERE IE.fecha BETWEEN @inicio AND @fin

                UNION ALL

                SELECT
                    'Incidencia de Espacio' AS [Registro],
                    IE.fecha AS [Fecha],
                    IE.tipo AS [Tipo],
                    IE.descripcion AS [Descripcion],
                    NULL AS [Codigo Equipo],
                    ES.nombre AS [Equipo o Espacio],
                    ES.nombre AS [Espacio],
                    CS.nombre AS [Centro de Salud]
                FROM INCIDENCIA_ESPACIO IE
                INNER JOIN ESPACIO ES ON IE.id_espacio = ES.id_espacio
                INNER JOIN CENTRO_SALUD CS ON ES.id_centro = CS.id_centro
                WHERE IE.fecha BETWEEN @inicio AND @fin

                UNION ALL

                SELECT
                    'Mantenimiento de Equipo' AS [Registro],
                    ME.fecha AS [Fecha],
                    ME.tipo AS [Tipo],
                    ME.descripcion AS [Descripcion],
                    EQ.codigo_equipo AS [Codigo Equipo],
                    EQ.nombre AS [Equipo o Espacio],
                    ES.nombre AS [Espacio],
                    CS.nombre AS [Centro de Salud]
                FROM MANTENIMIENTO_EQUIPO ME
                INNER JOIN EQUIPO EQ ON ME.id_equipo = EQ.id_equipo
                LEFT JOIN ESPACIO ES ON EQ.id_espacio = ES.id_espacio
                LEFT JOIN CENTRO_SALUD CS ON ES.id_centro = CS.id_centro
                WHERE ME.fecha BETWEEN @inicio AND @fin

                UNION ALL

                SELECT
                    'Mantenimiento de Espacio' AS [Registro],
                    ME.fecha AS [Fecha],
                    ME.tipo AS [Tipo],
                    ME.descripcion AS [Descripcion],
                    NULL AS [Codigo Equipo],
                    ES.nombre AS [Equipo o Espacio],
                    ES.nombre AS [Espacio],
                    CS.nombre AS [Centro de Salud]
                FROM MANTENIMIENTO_ESPACIO ME
                INNER JOIN ESPACIO ES ON ME.id_espacio = ES.id_espacio
                INNER JOIN CENTRO_SALUD CS ON ES.id_centro = CS.id_centro
                WHERE ME.fecha BETWEEN @inicio AND @fin

                ORDER BY [Fecha], [Registro]";

            EjecutarReporte(consulta,
                new SqlParameter("@inicio", SqlDbType.Date) { Value = fechaInicio },
                new SqlParameter("@fin", SqlDbType.Date) { Value = fechaFin });
        }

        // 3. Listado de los 3 consultorios mejor equipados por cada centro de salud.
        private void btnTopConsultorios_Click(object sender, EventArgs e)
        {
            string consulta = @"
                SELECT
                    [Centro de Salud],
                    [Consultorio],
                    [Cantidad de Equipos]
                FROM
                (
                    SELECT
                        CS.nombre AS [Centro de Salud],
                        ES.nombre AS [Consultorio],
                        COUNT(EQ.id_equipo) AS [Cantidad de Equipos],
                        ROW_NUMBER() OVER
                        (
                            PARTITION BY CS.id_centro
                            ORDER BY COUNT(EQ.id_equipo) DESC, ES.nombre
                        ) AS Numero
                    FROM CENTRO_SALUD CS
                    INNER JOIN ESPACIO ES ON CS.id_centro = ES.id_centro
                    LEFT JOIN EQUIPO EQ ON ES.id_espacio = EQ.id_espacio
                    WHERE ES.tipo = 'Consultorio'
                    GROUP BY CS.id_centro, CS.nombre, ES.id_espacio, ES.nombre
                ) X
                WHERE Numero <= 3
                ORDER BY [Centro de Salud], [Cantidad de Equipos] DESC, [Consultorio]";

            EjecutarReporte(consulta);
        }

        // 4. Al ingresar el código/id del consultorio, laboratorio o sala, mostrar todos sus equipos/enseres.
        private void btnEquiposPorEspacio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigoEspacio.Text))
            {
                MessageBox.Show("Ingrese el código del espacio. En este sistema corresponde al id_espacio.");
                return;
            }

            int idEspacio;
            if (!int.TryParse(txtCodigoEspacio.Text.Trim(), out idEspacio))
            {
                MessageBox.Show("El código del espacio debe ser numérico.");
                return;
            }

            string consulta = @"
                SELECT
                    CS.nombre AS [Centro de Salud],
                    ES.id_espacio AS [Codigo Espacio],
                    ES.nombre AS [Espacio],
                    ES.tipo AS [Categoria],
                    EQ.codigo_equipo AS [Codigo Inventario],
                    EQ.numero_serie AS [Numero de Serie],
                    EQ.nombre AS [Equipo / Enser],
                    EQ.marca AS [Marca],
                    EQ.modelo AS [Modelo],
                    EQ.estado AS [Estado]
                FROM ESPACIO ES
                INNER JOIN CENTRO_SALUD CS ON ES.id_centro = CS.id_centro
                LEFT JOIN EQUIPO EQ ON ES.id_espacio = EQ.id_espacio
                WHERE ES.id_espacio = @idEspacio
                ORDER BY EQ.nombre";

            EjecutarReporte(consulta,
                new SqlParameter("@idEspacio", SqlDbType.Int) { Value = idEspacio });
        }

        // 5. Buscar por código de inventario o número de serie y mostrar dónde está instalado.
        private void btnBuscarEquipo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarEquipo.Text))
            {
                MessageBox.Show("Ingrese el código de inventario o número de serie del equipo.");
                return;
            }

            string consulta = @"
                SELECT
                    EQ.codigo_equipo AS [Codigo Inventario],
                    EQ.numero_serie AS [Numero de Serie],
                    EQ.nombre AS [Equipo],
                    EQ.marca AS [Marca],
                    EQ.modelo AS [Modelo],
                    EQ.estado AS [Estado],
                    CS.nombre AS [Centro de Salud],
                    ES.id_espacio AS [Codigo Espacio],
                    ES.nombre AS [Espacio],
                    ES.tipo AS [Categoria]
                FROM EQUIPO EQ
                LEFT JOIN ESPACIO ES ON EQ.id_espacio = ES.id_espacio
                LEFT JOIN CENTRO_SALUD CS ON ES.id_centro = CS.id_centro
                WHERE EQ.codigo_equipo = @busqueda
                   OR EQ.numero_serie = @busqueda";

            EjecutarReporte(consulta,
                new SqlParameter("@busqueda", SqlDbType.VarChar) { Value = txtBuscarEquipo.Text.Trim() });
        }
    }
}
