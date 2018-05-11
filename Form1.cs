using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analog_clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int width = 300, height = 300, bsecond = 140, bminutes = 110, bhour = 80;
        private int centerX, centerY;
        private Bitmap bitmap;
        private Graphics graphics;


        private int[] coordinate(int val1,int val2)
        {
            var coordi = new int[2];
            val1 *= 6;
            if (val1 >= 0 && val1 <= 180)
            {
                coordi[0] = centerX + (int)(val2 * Math.Sin(Math.PI * val1 / 180));
                coordi[1] = centerY - (int)(val2 * Math.Cos(Math.PI * val1 / 180));


            }
            else
            {
                coordi[0] = centerX - (int)(val2 * -Math.Sin(Math.PI * val1 / 180));
                coordi[1] = centerY - (int)(val2 * Math.Cos(Math.PI * val1 / 180));
            }
            return coordi;
        }


        private int[] coordinateorg(int val1, int val2,int val3)
        {
            var coordi = new int[2];
            int x =(int)((val1*30)+(val2+0.5));
            if (x >= 0 && x <= 180)
            {
                coordi[0] = centerX + (int)(val3 * Math.Sin(Math.PI * x / 180));
                coordi[1] = centerY - (int)(val3 * Math.Cos(Math.PI * x / 180));


            }
            else
            {
                coordi[0] = centerX - (int)(val3 * -Math.Sin(Math.PI * x / 180));
                coordi[1] = centerY - (int)(val3 * Math.Cos(Math.PI * x / 180));
            }
            return coordi;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(width + 1, height + 1);
            centerX = width / 2;
            centerY = height / 2;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics = Graphics.FromImage(bitmap);
            int second = DateTime.Now.Second;
            int minutes = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;
            var coordinate_minute = new int[2];
            graphics.Clear(Color.White);
            graphics.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, width, height);
            graphics.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2));
            graphics.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(282, 140));
            graphics.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            graphics.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));

            coordinate_minute = coordinate(second, bsecond);
            graphics.DrawLine(new Pen(Color.Red, 1f), new Point(centerX, centerY), new Point(coordinate_minute[0], coordinate_minute[1]));

            coordinate_minute = coordinate(minutes, bminutes);
            graphics.DrawLine(new Pen(Color.Black, 2f), new Point(centerX, centerY), new Point(coordinate_minute[0], coordinate_minute[1]));

            coordinate_minute = coordinateorg(hour%12,minutes, bhour);
            graphics.DrawLine(new Pen(Color.Blue, 3f), new Point(centerX, centerY), new Point(coordinate_minute[0], coordinate_minute[1]));

            graphics.Dispose();

            pictureBox1.Image = bitmap;
        }
    }
}
