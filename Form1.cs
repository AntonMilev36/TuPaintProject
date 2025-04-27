using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using TuPaintProject.Shapes.cs;
using System.IO;
using Newtonsoft.Json;


namespace TuPaintProject
{
    public partial class Form1 : Form
    {
        private List<Shape> shapes = new List<Shape>(); 
        private Color currentColor = Color.HotPink;  
        private Button btnChangeColor;
        private Label lblShapeInfo;
        private ColorDialog colorDialog;
        private Stack<ICommand> undoStack = new Stack<ICommand>();
        private Stack<ICommand> redoStack = new Stack<ICommand>();

        public Form1()
        {
            InitializeComponent();
            currentColor = Color.HotPink;
            SetupUI();
        }

        private void SetupUI()
        {
            this.Load += Form1_Load;
            this.MouseDown += Form1_MouseDown;

            FlowLayoutPanel topPanel = new FlowLayoutPanel();
            topPanel.FlowDirection = FlowDirection.LeftToRight;
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 80; 
            topPanel.Padding = new Padding(5);
            topPanel.AutoSize = true;
            this.Controls.Add(topPanel);

            btnChangeColor = new Button();
            btnChangeColor.Text = "Color";
            btnChangeColor.Width = 80;
            btnChangeColor.Height = 30;
            btnChangeColor.BackColor = currentColor;
            btnChangeColor.Click += BtnChangeColor_Click;
            topPanel.Controls.Add(btnChangeColor);

            Button btnClear = new Button();
            btnClear.Text = "Clear all";
            btnClear.Width = 80;
            btnClear.Height = 30;
            btnClear.Click += BtnClear_Click;
            topPanel.Controls.Add(btnClear);

            Button btnUndo = new Button();
            btnUndo.Text = "Undo";
            btnUndo.Width = 80;
            btnUndo.Height = 30;
            btnUndo.Click += BtnUndo_Click;
            topPanel.Controls.Add(btnUndo);

            Button btnRedo = new Button();
            btnRedo.Text = "Redo";
            btnRedo.Width = 80;
            btnRedo.Height = 30;
            btnRedo.Click += BtnRedo_Click;
            topPanel.Controls.Add(btnRedo);

            Button btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.Width = 80;
            btnSave.Height = 30;
            btnSave.Click += BtnSave_Click;
            topPanel.Controls.Add(btnSave);

            Button btnLoad = new Button();
            btnLoad.Text = "Load";
            btnLoad.Width = 80;
            btnLoad.Height = 30;
            btnLoad.Click += BtnLoad_Click;
            topPanel.Controls.Add(btnLoad);

            lblShapeInfo = new Label();
            lblShapeInfo.Dock = DockStyle.Top;
            lblShapeInfo.Height = 30;
            //lblShapeInfo.Margin = new Padding(100, 0, 0, 0);
            lblShapeInfo.Text = "No shape created yet.";
            lblShapeInfo.TextAlign = ContentAlignment.MiddleRight;
            topPanel.Controls.Add(lblShapeInfo);

            colorDialog = new ColorDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Shape shape = null;

            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                shape = new Triangle(e.X, e.Y, 60, 60, currentColor);
            }
            else if (e.Button == MouseButtons.Left)
            {
                shape = new Shapes.cs.Rectangle(e.X, e.Y, 60, 60, currentColor);
            }
            else if (e.Button == MouseButtons.Right)
            {
                shape = new Circle(e.X, e.Y, 30, currentColor);
            }

            if (shape != null)
            {
                var cmd = new AddShapeCommand(shapes, shape);
                cmd.Execute();
                undoStack.Push(cmd);
                redoStack.Clear();

                var latestShape = shapes.LastOrDefault();
                if (latestShape != null)
                {
                    lblShapeInfo.Text = latestShape.GetInfo();
                }

            }

            Invalidate();
        }



        private void BtnChangeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
                btnChangeColor.BackColor = currentColor; 
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (var shape in shapes.OrderBy(s => s.X))
            {
                shape.Draw(e.Graphics);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            shapes.Clear();      
            Invalidate();        
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (undoStack.Any())
            {
                var cmd = undoStack.Pop();
                cmd.Undo();
                redoStack.Push(cmd);
                Invalidate();
            }
        }

        private void BtnRedo_Click(object sender, EventArgs e)
        {
            if (redoStack.Any())
            {
                var cmd = redoStack.Pop();
                cmd.Execute();
                undoStack.Push(cmd);
                Invalidate();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(shapes, Formatting.Indented,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            File.WriteAllText("shapes.json", json);
            MessageBox.Show("Shapes saved.");
        }



        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists("shapes.json"))
            {
                string json = File.ReadAllText("shapes.json");

                try
                {
                    var loadedShapes = JsonConvert.DeserializeObject<List<Shape>>(json,
                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                    if (loadedShapes != null && loadedShapes.Count > 0)
                    {
                        shapes = loadedShapes;
                        MessageBox.Show($"Shapes loaded successfully.\n{shapes.Count} shapes loaded.");
                    }
                    else
                    {
                        MessageBox.Show("No valid shapes found in the saved file.");
                    }

                    Invalidate();
                }
                catch (JsonSerializationException ex)
                {
                    MessageBox.Show($"Error deserializing shapes: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Saved file not found.");
            }

            int circleCount = shapes.Count(s => s is Circle);
            int rectangleCount = shapes.Count(s => s is Shapes.cs.Rectangle);
            int triangleCount = shapes.Count(s => s is Triangle);

            Console.WriteLine($"Loaded: {circleCount} Circles, {rectangleCount} Rectangles, {triangleCount} Triangles");
        }
    }
}
