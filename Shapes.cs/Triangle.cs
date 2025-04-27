using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TuPaintProject.Shapes.cs
{
    public class Triangle : Shape
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Triangle(int x, int y, int width, int height, Color color)
            : base(x, y, color) 
        {
            this.Width = width;
            this.Height = height;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = {
            new Point(X, Y + Height),  
            new Point(X + Width, Y + Height),
            new Point(X + Width / 2, Y)     
        };

            using (SolidBrush brush = new SolidBrush(Color))
            {
                g.FillPolygon(brush, points);
            }
            using (Pen pen = new Pen(Color, 2))
            {
                g.DrawPolygon(pen, points);
            }
        }

        public override string GetInfo()
        {
            return $"Triangle at ({X}, {Y}), Size: {Width}x{Height}";
        }
    }
}
