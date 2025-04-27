using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TuPaintProject.Shapes.cs;

namespace TuPaintProject
{
    public class AddShapeCommand : ICommand
    {
        private List<Shape> shapes;
        private Shape shape;

        public AddShapeCommand(List<Shape> shapes, Shape shape)
        {
            this.shapes = shapes;
            this.shape = shape;
        }

        public void Execute()
        {
            shapes.Add(shape);
        }

        public void Undo()
        {
            shapes.Remove(shape);
        }
    }
}
