using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Gymnasiearbete.Models
{
    public class DrawablePhysicsObject : PhysicsObject 
    {
        public Shape? ControlShape { get; set; }
        public Color Color { get; set; }
    }
}