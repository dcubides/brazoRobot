using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace brazoRobot.Class
{
    public class brazoUno
    {
        private Pen lapiz;
        private Graphics vector;
        private int xuno, xdos, xtres;
        private PictureBox picturebox;
        private int xcentro, ycentro;
        private TextBox txtEjeuno;
        private double x, y,z, x1, y1, z1;

        public brazoUno(PictureBox picturebox, TextBox txtEjeuno)
        {
            this.picturebox = picturebox;
            this.txtEjeuno = txtEjeuno;
            lapiz = new Pen(Color.Red, 30);
            xcentro = picturebox.Width / 2;
            ycentro = picturebox.Height / 2;
        }

        public void graficarBrazo()
        {
            this.dibujar();
        }

        private void dibujar()
        {
            x = Convert.ToDouble(xcentro - 10);
            y = Convert.ToDouble(ycentro + 10);
           

            x1 = Convert.ToDouble(xcentro - 10);
            y1 = Convert.ToDouble(ycentro * 2 - 10);
            
            vector = picturebox.CreateGraphics();
            lapiz = new Pen(Color.Red);
            lapiz.Color = Color.OrangeRed;
            lapiz.Width = 50;

            vector.DrawLine(lapiz, Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(x1), Convert.ToInt32(y1));

            lapiz.Dispose();
            vector.Dispose();
        }

       
    }
}
