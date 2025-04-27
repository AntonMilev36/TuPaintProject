using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuPaintProject.Shapes.cs
{
    public class Circle : Shape
    {
        public int Radius { get; private set; }

        [JsonConstructor]
        public Circle(int x, int y, int radius, Color color)
            : base(x, y, color)
        {
            Radius = radius;
        }

        public Circle(int centerX, int centerY, int radius, Color color, bool center = true)
            : base(center ? centerX - radius : centerX, center ? centerY - radius : centerY, color)
        {
            Radius = radius;
        }

        public override void Draw(Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(Color))
            {
                graphics.FillEllipse(brush, X, Y, Radius * 2, Radius * 2);
            }
        }

        public override string GetInfo()
        {
            return $"Circle at ({X + Radius}, {Y + Radius}), Radius: {Radius}";
        }
    }
}
