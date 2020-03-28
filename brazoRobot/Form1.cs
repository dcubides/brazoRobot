using brazoRobot.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace brazoRobot
{
    public partial class Form1 : Form
    {
        brazoUno brazouno;

        Bitmap bmp = new Bitmap(400, 400);
        Graphics g;
        int r = 200 / 2 - 70;
        int l = 10 * 5;
        int angle = 0;
        int center = 400 / 2;
        int R;
        int accuracy = 1;
        public Form1()
        {
            InitializeComponent();
            //dibujar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp); // Binds the graphics to the Bitmap
            pictureBox1.Image = bmp;
            R = (int)Math.Sqrt((Math.Pow(l / 2, 2) + Math.Pow(r, 2))); // Calculate the bigger radius
            Render();
        }



        private void dibujar()
        {
            brazouno = new brazoUno(pictureBox1, txtEjeuno);
            brazouno.graficarBrazo();
        }

        private void btnEjeunomas_Click(object sender, EventArgs e)
        {
            brazouno.graficarBrazo();
        }

        private void btnEjeunomenos_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int xcentro = pictureBox1.Width / 2;
            int ycentro = pictureBox1.Height / 2;

            Pen lapiz = new Pen(Color.Black, 2);

            e.Graphics.TranslateTransform(xcentro, ycentro);
            e.Graphics.ScaleTransform(1, -1); //convierte a coordenadas normales

            //dibujar ejes x y y 
            //linea horizontal
            e.Graphics.DrawLine(lapiz, xcentro * -1, 0, xcentro * 2, 0); //eje x
            //linea vertical
            e.Graphics.DrawLine(lapiz, 0, ycentro, 0, ycentro * -1); //eje y


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            l = (int)numericUpDown1.Value * 5; // As soon as the Value Changed store it in the l int
            Render(); // Execute render function
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            r = 400 / 4 + ((int)numericUpDown2.Value * 5);
            Render();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            angle = trackBar1.Value; // As soon as the Value Changed store it in the angle int
            Render(); // Execute render function
        }

        private int[] LineCoord(int angleIn, int radius, int center) // Get any point on the circle by the angle
        {
            int[] coord = new int[2]; // Setting up the int array for return
            angleIn %= (360 * accuracy);
            angleIn *= 1;

            if (angleIn >= 0 && angleIn <= (180 * accuracy))
            {
                coord[0] = center + (int)(radius * Math.Sin(Math.PI * angleIn / (180 * accuracy)));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / (180 * accuracy)));
            }
            else
            {
                coord[0] = center - (int)(radius * -Math.Sin(Math.PI * angleIn / (180 * accuracy)));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / (180 * accuracy)));
            }
            return coord;
        }

        // These functions converts the Degrees of an angle to Radians and v.v
        double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        private void Render()
        {
              g = Graphics.FromImage(bmp); // Binds the graphics to the Bitmap
           // g = pictureBox1.CreateGraphics();
            pictureBox1.Image = bmp;
            g.Clear(Color.White); // Clears the screen

            R = (int)Math.Sqrt((Math.Pow((l / 2), 2) + Math.Pow(r, 2))); // Calculate the bigger radius
            int theta = (int)RadianToDegree(Math.Atan((double)(l / 2) / r)); // Calculate the angle theta

            // This calculates the bigger radius' lines' coordinates
            int x0 = LineCoord(angle + theta, R, center)[0];
            int y0 = LineCoord(angle + theta, R, center)[1];
            int x1 = LineCoord(angle + 360 - theta, R, center)[0];
            int y1 = LineCoord(angle + 360 - theta, R, center)[1];

         
           
                //liena principal
                g.DrawLine(new Pen(Color.FromArgb(150, 0, 0), 50f), new Point(center, center*2), new Point(LineCoord(angle, r, center)[0], LineCoord(angle, r, center)[1])); // Draw the Handle
            
            //liena azul
              // g.DrawLine(new Pen(Color.FromArgb(0, 0, 230), 4f), new Point(x0, y0), new Point(x1, y1)); // Draw the Perpendicular Line
           
            //lineas a los lados
                //g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), 1f), new Point(center, center), new Point(x0, y0)); // From centar to points on the Perpendicular Line
                //g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), 1f), new Point(center, center), new Point(x1, y1)); // From centar to points on the Perpendicular Line
            

            g.Dispose();
        }



    }
}
