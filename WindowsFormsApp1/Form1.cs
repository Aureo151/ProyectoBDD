using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int velocidadX = 5;
        int velocidadY = 2;

        public Form1()
        {
            InitializeComponent();

            KeyPreview = true;
            timer1.Interval = 20;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label2.Left += velocidadX;
            label2.Top += velocidadY;

           
            if (label2.Left <= 0 || label2.Right >= ClientSize.Width)
            {
                velocidadX = -velocidadX;
            }

           
            if (label2.Top <= 0)
            {
                velocidadY = -velocidadY;
            }

            
            if (label2.Bounds.IntersectsWith(label1.Bounds))
            {
                velocidadY = -velocidadY;
            }

            
            if (label2.Bottom >= ClientSize.Height)
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                label1.Left -= 15;
            }

            if (e.KeyCode == Keys.Right)
            {
                label1.Left += 15;
            }

            
            if (label1.Left < 0)
            {
                label1.Left = 0;
            }

            if (label1.Right > ClientSize.Width)
            {
                label1.Left = ClientSize.Width - label1.Width;
            }
        }
    }

}
