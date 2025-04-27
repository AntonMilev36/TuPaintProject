using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuPaintProject.Shapes.cs
{
    public class Rectangle : Shape
    {
        public int Width { get; }
        public int Height { get; }

        public Rectangle(int x, int y, int width, int height, Color color)
            : base(x, y, color)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(Color))
            {
                graphics.FillRectangle(brush, X, Y, Width, Height);
            }
        }

        public override string GetInfo()
        {
            return $"Rectangle at ({X}, {Y}), Size: {Width}x{Height}";
        }
    }
}
