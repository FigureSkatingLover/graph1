using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace WindowsFormsApp1._1
{
    public partial class Form1 : Form
    {
        /*private List<double> x_dots;
        private List<double> y_plus;
        private List<double> y_minus;
        private List<double> y_all;*/
        private double x_start = -10;
        private double x_end = 10;
        private double y_start = -10;
        private double y_end = 10;
        private double step;
        private double a;

        private void yAxis()
        {
            Pen myPen = new Pen(Color.Black, 2f);
            Graphics g;
            g = pictureBox1.CreateGraphics();
            g.DrawLine(myPen, 275, 0, 275, 550);
            g.DrawLine(myPen, 275, 0, 268, 7);
            g.DrawLine(myPen, 275, 0, 282, 7);
            Font drawFont = new Font("Arial", 9);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString("Y", drawFont, drawBrush, 255, 10);
        }

        private void xAxis()
        {
            Pen myPen = new Pen(Color.Black, 2f);
            Graphics g;
            g = pictureBox1.CreateGraphics();
            g.DrawLine(myPen, 0, 275, 550, 275);
            g.DrawLine(myPen, 548, 275, 541, 268);
            g.DrawLine(myPen, 548, 275, 541, 282);
            Font drawFont = new Font("Arial", 9);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString("X", drawFont, drawBrush, 535, 285);
        }

        private void grid()
        {
            Pen myPen = new Pen(Color.Black, 1f);
            Graphics g;
            g = pictureBox1.CreateGraphics();
            for (int i=0; i<20; i++)
            {
                g.DrawLine(myPen, (int)(0 + i * 27.5), 0, (int)(0 + i * 27.5), 550);
            }

            for (int i=0; i<20; i++)
            {
                g.DrawLine(myPen, 0, (int)(0+i*27.5), 550, (int)(0+i*27.5));
            }     
        }

        private void ruler()
        {
            Pen myPen = new Pen(Color.Black, 2f);
            Graphics g;
            g = pictureBox1.CreateGraphics();
            for (int i = 0; i < 20; i++)
            {
                g.DrawLine(myPen, (int)(0 + i * 27.5), 270, (int)(0 + i * 27.5), 280);
            }

            for (int i = 0; i < 20; i++)
            {
                g.DrawLine(myPen, 270, (int)(0 + i * 27.5), 280, (int)(0 + i * 27.5));
            }
            int scale = Convert.ToInt32(numericUpDown2.Value);
            Font drawFont = new Font("Arial", 9);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            for (int i=0; i<10; i++)
            {

                g.DrawString(num(i, scale), drawFont, drawBrush, (int)(275+i*27.5), 285); //x+
                g.DrawString(num(i-10, scale), drawFont, drawBrush, (int)(0 + i * 27.5), 285); //x-

                if (i == 0)
                {
                    continue;
                }
                g.DrawString(num(i, scale), drawFont, drawBrush, 260, (int)(275-i*27.5)); //y+
                g.DrawString(num(-i, scale), drawFont, drawBrush, 255, (int)(275+i*27.5)); //y-
            }
        }

        private String num(int i, int scale)
        {
            return Convert.ToString(scale * i);
        }

        private int xc(double p)
        {
            int x = (int)((550 / (x_end - (x_start))) * p - (550 / (x_end - (x_start))) * (x_start));
            return x;
        }

        private int yc(double p)
        {

            int y = (int)((-550 / (y_end - (y_start))) * p + (550 / (y_end - (y_start))) * y_end);
            return y;
        }

        private double myRound(double x, int precision)
        {
            return ((int)(x * Math.Pow(10.0, precision)) / Math.Pow(10.0, precision));
        }

        private void plot()
        {
            label9.Text = "";
            Pen myPen = new Pen(Color.Blue, 1.5f);
            Graphics g;
            g = pictureBox1.CreateGraphics();
            g.Clear(SystemColors.Control);
            //int sc = Convert.ToInt16(numericUpDown2.Value);
            x_start = x_start * (Convert.ToDouble(numericUpDown2.Value));
            x_end = x_end * (Convert.ToDouble(numericUpDown2.Value));
            y_start = y_start * (Convert.ToDouble(numericUpDown2.Value));
            y_end = y_end * (Convert.ToDouble(numericUpDown2.Value));
            if (!(checkBox1.Checked))
            {
                xAxis();
            }
            if (!(checkBox2.Checked))
            {
                yAxis();
            }
            if (!(checkBox3.Checked))
            {
                grid();
            }
            if (!(checkBox4.Checked))
            {
                ruler();
            }
            a = Convert.ToDouble(numericUpDown1.Value);
            string stepstr=textBox4.Text;
            step = 0.1;
            if (stepstr != "") {
                step = Convert.ToDouble(stepstr);
            }
            if (step < 0.03)
            {
                label9.Text = "Укажите значение шага больше, чем 0,03";
            }
            int count = 0;
                for (double i = -Math.Abs(a); i < Math.Abs(a); i += step) ///////////////
                {
                    if (a != 0)
                    {
                        count++;
                    }
                }
            //count *= 2;
            Point[] points = new Point[count];
            Point[] points2 = new Point[count];
            int num_point = 0;
            bool flag = false;
            if (a == 0)
            {
                label9.Text = "Значение параметра а не может быть равно 0!";
            }
            if ((a > 0)&& (step >= 0.03))
            {
                for (double i = -a; i < a; i = myRound(i+step, 2))
                {
                    if (a != 0)
                    {
                        if (num_point == count)
                        {
                            break;
                        }

                        points[num_point] = new Point(xc(i), yc(i * Math.Sqrt((a + i) / (a - i))));
                        points2[num_point] = new Point(xc(i), yc(-i * Math.Sqrt((a + i) / (a - i))));
                        num_point++;
                        flag = true;
                        //label5.Text = Convert.ToString(step)+" "+Convert.ToString(a);
                    }
                }

                points[count - 1] = new Point((xc(a - 0.001)), (yc((((a - 0.001) * Math.Sqrt(a + a - 0.001)) / (Math.Sqrt(a - a + 0.001)))))/*/sc*/);
                points2[count - 1] = new Point((xc(a - 0.001)), /*-*/(yc(((-(a - 0.001) * Math.Sqrt(a + a - 0.001)) / (Math.Sqrt(a - a + 0.001)))))/*/sc*/);
            }
            if ((a < 0)&&(step>=0.03))
            {
                a = -a;
                for (double i = -a; i < a; i = myRound(i + step, 2))
                {
                    if (a != 0)
                    {
                        if (num_point == count)
                        {
                            break;
                        }
                        points[num_point] = new Point(xc(-i), yc(i * Math.Sqrt((a + i) / (a - i))));
                        points2[num_point] = new Point(xc(-i), yc(-i * Math.Sqrt((a + i) / (a - i))));

                        num_point++;
                        flag = true;
                    }
                }
                points[count - 1] = new Point((xc(a + 0.0001)), (yc((a + 0.0001) * Math.Sqrt((a + a + 0.0001) / (a - a + 0.0001))))/*/sc*/);
                points2[count - 1] = new Point((xc(a + 0.0001)), (yc(-(a + 0.0001) * Math.Sqrt((a + a + 0.0001) / (a - a + 0.0001))))/*/sc*/);
            }


            if (flag)
    {
        g.DrawLines(myPen, points);
        g.DrawLines(myPen, points2);
                //g.DrawLine(myPen, points[0], points2[0]);
                x_start = x_start / (Convert.ToDouble(numericUpDown2.Value));
                x_end = x_end / (Convert.ToDouble(numericUpDown2.Value));
                y_start = y_start / (Convert.ToDouble(numericUpDown2.Value));
                y_end = y_end / (Convert.ToDouble(numericUpDown2.Value));
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e) //step
        {
            string tb4 = textBox4.Text;
            if (tb4 != "")
            {
                step = Convert.ToDouble(tb4);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            yAxis();
            xAxis();
            grid();
            ruler();
            plot();
        }
        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        }
        private void settings()
        {
            Graphics g;
            g = pictureBox1.CreateGraphics();
            g.Clear(SystemColors.Control);
            if (!(checkBox1.Checked))
            {
                xAxis();
            }
            if (!(checkBox2.Checked))
            {
                yAxis();
            }
            if (!(checkBox3.Checked))
            {
                grid();
            }
            if (!(checkBox4.Checked))
            {
                ruler();
            }
            if (textBox4.Text != "")
            {
                plot();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) //hide Ox
        {
            settings();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) //hide Oy
        {
            settings();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) //hide grid
        {
            settings();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) //hide ruler
        {
            settings();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            yAxis();
            xAxis();
            grid();
            ruler();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            /*x_start = x_start * (Convert.ToDouble(numericUpDown2.Value));
            x_end = x_end * (Convert.ToDouble(numericUpDown2.Value));
            y_start = y_start * (Convert.ToDouble(numericUpDown2.Value));
            y_end = y_end * (Convert.ToDouble(numericUpDown2.Value));*/
            settings();
            /*x_start = x_start / (Convert.ToDouble(numericUpDown2.Value));
            x_end = x_end / (Convert.ToDouble(numericUpDown2.Value));
            y_start = y_start / (Convert.ToDouble(numericUpDown2.Value));
            y_end = y_end / (Convert.ToDouble(numericUpDown2.Value));*/
        }
    }
}
