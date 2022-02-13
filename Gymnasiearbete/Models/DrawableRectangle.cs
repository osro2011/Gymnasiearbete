using Engine.Objects;

namespace Gymnasiearbete.Models
{
    public class DrawableRectangle : DrawablePhysicsObject, IRectangle
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public float Angle { get; set; }
    }
}