using Avalonia;

namespace Gymnasiearbete.Models
{
    public class Line : DrawablePhysicsObject
    {
        public int Width { get; set; }
        Point _offset;
        public Point Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }
        public int XOffset
        {
            get
            {
                return (int)_offset.X;
            }
        }
        public int YOffset
        {
            get
            {
                return (int)_offset.Y;
            }
        }
    }
}
