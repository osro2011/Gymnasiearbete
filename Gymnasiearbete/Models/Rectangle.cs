using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Gymnasiearbete.Models
{
    public class Rectangle : PhysicsObject
    {
        public Shape ControlShape { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Color Color { get; set; }
    }
}
