using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBDD
{
    internal class Estilos
    {
        public static void AplicarEstilo(Form form)
        {
            form.BackColor = Color.WhiteSmoke;
            form.Font = new Font("Segoe UI", 10);
            form.StartPosition = FormStartPosition.CenterScreen;

            foreach (Control control in form.Controls)
            {
                AplicarEstiloControl(control);
            }
        }

        private static void AplicarEstiloControl(Control control)
        {
            if (control is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.Height = 35;
                btn.Width = 110;

                if (btn.Text.Contains("Guardar"))
                    btn.BackColor = Color.SeaGreen;
                else if (btn.Text.Contains("Modificar") || btn.Text.Contains("Actualizar"))
                    btn.BackColor = Color.SteelBlue;
                else if (btn.Text.Contains("Eliminar"))
                    btn.BackColor = Color.Firebrick;
                else if (btn.Text.Contains("Buscar"))
                    btn.BackColor = Color.DarkSlateGray;
                else if (btn.Text.Contains("Limpiar"))
                    btn.BackColor = Color.DimGray;
                else
                    btn.BackColor = Color.SlateGray;
            }

            if (control is DataGridView dgv)
            {
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.None;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.ReadOnly = true;
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
            }

            if (control is TextBox txt)
            {
                txt.BorderStyle = BorderStyle.FixedSingle;
            }

            if (control is ComboBox cmb)
            {
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            if (control is Label lbl)
            {
                lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }

            if (control is GroupBox gb)
            {
                gb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }

            foreach (Control child in control.Controls)
            {
                AplicarEstiloControl(child);
            }
        }
    }
}

