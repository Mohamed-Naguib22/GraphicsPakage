using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GraphicsProject
{
    public partial class Form1 : Form
    {
        
       
        public Form1()
        {
            InitializeComponent();

        }

        public void lineDDA(int x1, int y1, int x2, int y2)
        {
            ArrayList xElements = new ArrayList();
            ArrayList yElements = new ArrayList();

            float dx = x2 - x1;
            float dy = y2 - y1;

            var aBrush = Brushes.Black;
            var g = panel1.CreateGraphics();

            float steps;

            if (Math.Abs(dx) > Math.Abs(dy))
                steps = Math.Abs(dx);
            else
                steps = Math.Abs(dy);

            float xIncrement = dx / steps;
            float yIncrement = dy / steps;

            float x = x1;
            float y = y1;

            g.FillRectangle(aBrush, (int)Math.Round(x), (int)Math.Round(y), 2, 2);
            for (int i = 0; i < steps; i++)
            {
               
                x += xIncrement;
                y += yIncrement;

                xElements.Add((int)Math.Round(x));
                yElements.Add((int)Math.Round(y));

                var X = (int)Math.Round(x);
                var Y = (int)Math.Round(y);

                g.FillRectangle(aBrush, (int)Math.Round(x) , (int)Math.Round(y), 2, 2);
            }
        }

        public void lineBres(int x1, int y1, int x2, int y2)
        {
            var aBrush = Brushes.Black;
            var g = panel1.CreateGraphics();

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int x, y;
            int p = 2 * dy - dx;
   
            int twoDy = 2 * dy;
            int twoDyMinusDx = 2 * (dy - dx);

            if (x1 > x2){
                x = x2;
                y = y2; 
                x2 = x1;
            }

            else{
                x = x1;
                y = y1;
            }

            g.FillRectangle(aBrush, x + 190, y + 100, 2, 2);

            while (x < x2){
                x++;
                if (p < 0)
                    p += twoDy;

                else {
                    y++;
                    p += twoDyMinusDx;
                }
                g.FillRectangle(aBrush, x, y, 2, 2);
            }
        }


        public void CirclePlotPoints(int xCenter, int yCenter, int x, int y) {
            var aBrush = Brushes.IndianRed;
            var g = panel1.CreateGraphics();

            g.FillRectangle(aBrush, xCenter + x + 190, yCenter + y + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter - x + 190, yCenter + y + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter + x + 190, yCenter - y + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter - x + 190, yCenter - y + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter + y + 190, yCenter + x + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter - y + 190, yCenter + x + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter + y + 190, yCenter - x + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter - y + 190, yCenter - x + 100, 2, 2);

        }

        public void circle(int xCenter, int yCenter, int radius) {
            var xElements = new ArrayList();
            var yElements = new ArrayList();
            var pElements = new ArrayList();

            int x = 0;
            int y = radius;
            int p = 1 - radius;
            CirclePlotPoints(xCenter, yCenter, x, y);
            while (x < y) {
                pElements.Add(p);
                x++;
                if (p < 0)
                    p += 2 * x + 1;

                else {
                    y--;
                    p += 2 * (x-y) + 1;
                }
                xElements.Add(x);
                yElements.Add(y);
                
                CirclePlotPoints(xCenter, yCenter, x, y);
            }
        }

        public void ellipsePlotPoints(int xCenter, int yCenter, int x, int y)
        {
            var aBrush = Brushes.Black;
            var g = panel1.CreateGraphics();

            g.FillRectangle(aBrush, xCenter + x + 190, yCenter + y + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter - x + 190, yCenter + y + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter + x + 190, yCenter - y + 100, 2, 2);
            g.FillRectangle(aBrush, xCenter - x + 190, yCenter - y + 100, 2, 2);
        }

        public void ellipse(int xCenter, int yCenter, int xRadius, int yRadius) {
            int x, y;
            double dx, dy;
            x = 0;
            y = yRadius;
            double p1 = (yRadius* yRadius) - (xRadius * xRadius * yRadius) + (0.25 * xRadius * xRadius);

            dx = 2 * yRadius * yRadius * x;
            dy = 2 * xRadius * xRadius * y;

            while (dx < dy) {
                ellipsePlotPoints(xCenter, yCenter, x, y);
                if (p1 < 0) {
                    x++;
                    dx = dx + (2 * yRadius*  yRadius);
                    p1 = p1 + dx + (yRadius * yRadius);
                }

                else {
                    x++;
                    y--;
                    dx = dx + (2 * yRadius * yRadius);
                    dy = dy - (2 * xRadius * xRadius);
                    p1 = p1 + dx - dy + (yRadius * yRadius);
                }
            }

            double p2 = ((yRadius * yRadius) * ((x + 0.5) * (x + 0.5))) + ((xRadius * xRadius) * ((y - 1) * (y - 1))) - (xRadius * xRadius * yRadius* yRadius);

            while (y >= 0)
            {
                ellipsePlotPoints(xCenter, yCenter, x, y);

                if (p2 > 0)
                {
                    y--;
                    dy = dy - (2 * xRadius * xRadius);
                    p2 = p2 + (xRadius * xRadius) - dy;
                }

                else
                {
                    y--;
                    x++;
                    dx = dx + (2 * yRadius * yRadius);
                    dy = dy - (2 * xRadius * xRadius);
                    p2 = p2 + dx - dy + (xRadius * xRadius);
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nrows = 2;
            int ncol = 3;

            StreamWriter page = new StreamWriter(@"index.html");
            page.WriteLine("<!DOCTYPE html><html><body>");
            page.WriteLine("<table style = 'width = 100' 'height = 100' 'border:solid black;'>");
            for(int i = 0; i < nrows; i++)
            { 
                page.WriteLine("<tr>");
            
                for (int j = 0; j < ncol; j++)
                {
                    page.WriteLine("<td style = 'border:solid black;'>");
                    page.WriteLine("mohamed");
                    page.WriteLine("</td>");
                }

                page.WriteLine("</tr>");
            }
            page.WriteLine("</table>");

            page.WriteLine("</body></html>");
            page.Close();
            MessageBox.Show("<Done it>");


            string t1 = textBox1.Text;
            string t2 = textBox2.Text;
            string t3 = textBox3.Text;
            string t4 = textBox4.Text;

            int x1 = Int16.Parse(t1);
            int y1 = Int16.Parse(t2);
            int x2 = Int16.Parse(t3);
            int y2 = Int16.Parse(t4);

            lineDDA( x1,  y1,  x2,  y2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string t1 = textBox1.Text;
            string t2 = textBox2.Text;
            string t3 = textBox3.Text;
            string t4 = textBox4.Text;

            int x1 = Int16.Parse(t1);
            int y1 = Int16.Parse(t2);
            int x2 = Int16.Parse(t3);
            int y2 = Int16.Parse(t4);

            lineBres(x1, y1, x2, y2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string t1 = textBox5.Text;
            string t2 = textBox6.Text;
            string t3 = textBox7.Text;

            int x = Int16.Parse(t1);
            int y = Int16.Parse(t2);
            int r = Int16.Parse(t3);

            circle(x, y, r);

        }
        private void button4_Click(object sender, EventArgs e)
        {
            string t1 = textBox8.Text;
            string t2 = textBox9.Text;
            string t3 = textBox10.Text;
            string t4 = textBox11.Text;

            int xCenter = Int16.Parse(t1);
            int yCenter = Int16.Parse(t2);
            int xRadius = Int16.Parse(t3);
            int yRadius = Int16.Parse(t4);

            ellipse(xCenter, yCenter, xRadius, yRadius);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen blackpen = new Pen(Color.Black, 3);

            Graphics g = e.Graphics;
            Pen pen = new Pen(ForeColor);
            g.DrawLine(pen, 0, 200, 500, 200);
            g.DrawLine(pen, 190, 0, 190, 500);

            g.Dispose();
        }

        public void tableCircle(int n)
        {
            int nrows = n;
            int ncol = 6;
            
            StreamWriter page = new StreamWriter(@"index.html");
            page.WriteLine("<!DOCTYPE html><html><body>");
            page.WriteLine("<table>");
            page.WriteLine("<thead>");
            page.WriteLine("<tr>");
            page.WriteLine("<th>K</th>");
            page.WriteLine("<th>PK</th>");
            page.WriteLine("<th>X+1</th>");
            page.WriteLine("<th>Y+1</th>");
            page.WriteLine("<th>2X+1</th>");
            page.WriteLine("<th>2Y+1</th>");
            page.WriteLine("</tr>");
            page.WriteLine("</thead>");

            page.WriteLine("<tbody>");
            for (int i = 0; i < nrows; i++)
            {
                page.WriteLine("<tr>");

                for (int j = 0; j < ncol; j++)
                {
                    page.WriteLine("<td style = 'border:solid black;'>");
                    page.WriteLine("mohamed");
                    page.WriteLine("</td>");
                }

                page.WriteLine("</tr>");
            }
            page.WriteLine("</tbody>");
            page.WriteLine("</table>");

            page.WriteLine("</body></html>");

            page.WriteLine("<style>");
            page.WriteLine("table {border: 1px solid black; margin-left: auto; margin-right: auto; border-collapse: collapse;  width: 1000px; text-align: center; font-size: 20px;}");
            page.WriteLine("table thead th {background-color: #54585d; color: #ffffff; font-weight: bold; font-size: 13px; border: 1px solid #54585d;}");
            page.WriteLine("table tbody td {color: #636363; border: 1px solid #dddfe1;}");
            page.WriteLine("table tbody tr {background-color: #f9fafb;}");
            page.WriteLine("table tbody tr:nth-child(odd) {background-color: #ffffff;}");

            page.WriteLine("</style>");

            page.Close();
            MessageBox.Show("Done it");
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            int nrows = 3;
            int ncol = 2;
            int[,] a = {{ 1, 2, 3 }, { 5, 6, 7} };

            StreamWriter page = new StreamWriter(@"index.html");
            page.WriteLine("<!DOCTYPE html><html><body>");
            page.WriteLine("<table>");
            page.WriteLine("<thead>");
            page.WriteLine("<tr>");
            page.WriteLine("<th>K</th>");
            page.WriteLine("<th>PK</th>");
            //page.WriteLine("<th>X+1</th>");
            //page.WriteLine("<th>Y+1</th>");
            //page.WriteLine("<th>2X+1</th>");
            //page.WriteLine("<th>2Y+1</th>");
            page.WriteLine("</tr>");
            page.WriteLine("</thead>");

            page.WriteLine("<tbody>");
            for (int i = 0; i < nrows; i++)
            {
                page.WriteLine("<tr>");

                for (int j = 0; j < ncol; j++)
                {
                    page.WriteLine("<td style = 'border:solid black;'>");
                    page.WriteLine( a[j,i]);
                    page.WriteLine("</td>");
                }

                page.WriteLine("</tr>");
            }
            page.WriteLine("</tbody>");
            page.WriteLine("</table>");

            page.WriteLine("</body></html>");

            page.WriteLine("<style>");
            page.WriteLine("table {border: 1px solid black; margin-left: auto; margin-right: auto; border-collapse: collapse;  width: 1000px; text-align: center; font-size: 20px;}");
            page.WriteLine("table thead th {background-color: #54585d; color: #ffffff; font-weight: bold; font-size: 13px; border: 1px solid #54585d;}");
            page.WriteLine("table tbody td {color: #636363; border: 1px solid #dddfe1;}");
            page.WriteLine("table tbody tr {background-color: #f9fafb;}");
            page.WriteLine("table tbody tr:nth-child(odd) {background-color: #ffffff;}");

            page.WriteLine("</style>");

            page.Close();
            MessageBox.Show("Done it");
        }
    }
}
