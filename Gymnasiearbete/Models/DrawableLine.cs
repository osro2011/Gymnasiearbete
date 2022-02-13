using Avalonia;

namespace Gymnasiearbete.Models
{
    public class DrawableLine : DrawablePhysicsObject
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
            set
            {
                _offset = new Point(value, _offset.Y);
            }
        }
        public int YOffset
        {
            get
            {
                return (int)_offset.Y;
            }
            set
            {
                _offset = new Point(_offset.X, value);
            }
        }
    }
}
