using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuPaintProject.Shapes.cs
{
    public abstract class Shape
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Color Color { get; protected set; }

        public Shape(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public abstract void Draw(Graphics graphics);

        public virtual string GetInfo()
        {
            return $"{this.GetType().Name} at ({X}, {Y})";
        }
    }

}
