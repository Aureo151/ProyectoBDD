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
            form.BackColor = Color.FromArgb(245, 247, 250);
            form.Font = new Font("Segoe UI", 10);
            form.StartPosition = FormStartPosition.CenterScreen;

            foreach (Control control in form.Controls)
                AplicarEstiloControl(control);
        }

        private static void AplicarEstiloControl(Control control)
        {
            if (control is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ForeColor = Color.White;
                btn.Height = 38;
                btn.Width = 120;
                btn.Cursor = Cursors.Hand;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                if (btn.Text.Contains("Guardar"))
                    btn.BackColor = Color.FromArgb(22, 163, 74);
                else if (btn.Text.Contains("Modificar") || btn.Text.Contains("Actualizar") || btn.Text.Contains("Asignar"))
                    btn.BackColor = Color.FromArgb(37, 99, 235);
                else if (btn.Text.Contains("Eliminar"))
                    btn.BackColor = Color.FromArgb(220, 38, 38);
                else if (btn.Text.Contains("Buscar"))
                    btn.BackColor = Color.FromArgb(30, 41, 59);
                else if (btn.Text.Contains("Limpiar"))
                    btn.BackColor = Color.FromArgb(100, 116, 139);
                else
                    btn.BackColor = Color.FromArgb(71, 85, 105);
            }

            if (control is ComboBox cmb)
            {
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.FlatStyle = FlatStyle.Standard; // más estable que Flat
                cmb.Font = new Font("Segoe UI", 10);
                cmb.BackColor = Color.White;
                cmb.ForeColor = Color.FromArgb(15, 23, 42);
            }

            if (control is TextBox txt)
            {
                txt.BorderStyle = BorderStyle.FixedSingle;
                txt.Font = new Font("Segoe UI", 10);
                txt.BackColor = Color.White;
                txt.ForeColor = Color.FromArgb(15, 23, 42);
            }

            if (control is Label lbl)
            {
                lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lbl.ForeColor = Color.FromArgb(30, 41, 59);
            }

            if (control is GroupBox gb)
            {
                gb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                gb.ForeColor = Color.FromArgb(30, 41, 59);
                gb.BackColor = Color.FromArgb(245, 247, 250);
            }

            if (control is DataGridView dgv)
            {
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.FixedSingle;

                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgv.GridColor = Color.FromArgb(203, 213, 225);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.ReadOnly = true;
                dgv.RowHeadersVisible = false;
                dgv.EnableHeadersVisualStyles = false;

                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgv.ColumnHeadersHeight = 36;

                dgv.DefaultCellStyle.BackColor = Color.White;
                dgv.DefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(191, 219, 254);
                dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);

                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            }

            if (control is MenuStrip menu)
            {
                menu.BackColor = Color.FromArgb(30, 41, 59);
                menu.ForeColor = Color.White;
                menu.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                menu.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
            }

            foreach (Control child in control.Controls)
                AplicarEstiloControl(child);
        }
    }

    public class MenuColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(51, 65, 85); }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(51, 65, 85); }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(51, 65, 85); }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.FromArgb(15, 23, 42); }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.FromArgb(15, 23, 42); }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return Color.White; }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return Color.White; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Color.White; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Color.White; }
        }
    }
}

