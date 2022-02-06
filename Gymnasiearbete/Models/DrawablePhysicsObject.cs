using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Gymnasiearbete.Models
{
    public class DrawablePhysicsObject : PhysicsObject 
    {
        public Shape ControlShape { get; set; }
        Color _color;
        public Color Color {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
        public string ColorString
        {
            get
            {
                return _color.ToString();
            }
        }
    }
}