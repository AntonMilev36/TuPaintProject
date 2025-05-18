# 🎨 TuPaintProject

**TuPaintProject** is a Windows Forms drawing application built in C# using object-oriented principles 
such as inheritance, encapsulation, polymorphism, and interfaces. It supports creating, saving, loading, 
undoing, and redoing custom shapes like circles, rectangles, and triangles.

---

## 🛠 Features

- ✏️ Draw shapes (Rectangle, Circle, Triangle) using mouse interactions
- 🧠 Undo & Redo with Command Pattern
- 💾 Save and load drawings using JSON serialization
- 📐 Object-oriented hierarchy with shared and specific logic
- 🗂 Track and count shapes dynamically
- 🔄 Clear drawing board with one click
- 🧩 Easily extensible to add new shapes or commands

---

## 🧩 Object-Oriented Design

### ✅ Implemented Concepts

| Concept         | Description |
|----------------|-------------|
| **Inheritance** | All shapes inherit from the abstract `Shape` base class |
| **Encapsulation** | Properties use `protected set` to allow child-class updates only |
| **Polymorphism** | `Draw()` and `GetInfo()` methods are overridden in each shape |
| **Virtual Methods** | Base `GetInfo()` method is `abstract` and implemented in children |
| **Access Modifiers** | Combination of `public`, `protected`, and `private` for safe encapsulation |
| **Properties** | Used instead of public fields (e.g., `public int X { get; protected set; }`) |
| **Interfaces** | `ICommand` interface for command pattern operations |

---

## 📁 Project Structure

TuPaintProject/
│
├── Shapes/
│ ├── Shape.cs
│ ├── Rectangle.cs
│ ├── Circle.cs
│ └── Triangle.cs
│
├── Commands/
│ ├── ICommand.cs
│ └── AddShapeCommand.cs
│
├── Form1.cs # Main UI logic
├── Program.cs # Application entry point
└── README.md

---

## 🔧 How It Works

- **Drawing Shapes**:
  - Left-click: Draw Rectangle
  - Right-click: Draw Circle
  - Shift + Left-click: Draw Triangle

- **Undo/Redo**:
  - Each shape action is a command (`AddShapeCommand`) pushed onto `undoStack`
  - Undo: Pops from `undoStack` and undoes
  - Redo: Pops from `redoStack` and re-executes

- **Serialization**:
  - Shapes are saved/loaded as JSON (using `JsonConverter` annotations to preserve type)

---

## 💡 Technologies

- C# / .NET Framework
- Windows Forms
- System.Drawing
- System.Text.Json (for serialization)
- LINQ (for shape counts, filtering, etc.)

---

## 🚀 How to Run

1. Clone the repo  
   `git clone https://github.com/yourusername/TuPaintProject.git`

2. Open the solution in Visual Studio

3. Run the project (F5)

---

## 🧪 Possible Enhancements

- Add more shape types (Polygon, Ellipse, etc.)
- Add resizing/moving shapes
- Export as image
- Multi-level undo/redo preview

---

## 📜 License

MIT License

---

## 🙋‍♂️ Author

Created by Anton Milev as part of a university project.
