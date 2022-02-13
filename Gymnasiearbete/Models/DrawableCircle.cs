using Engine.Objects;

namespace Gymnasiearbete.Models
{
    public class DrawableCircle : DrawablePhysicsObject, ICircle
    {
        int _radius;
        public int Radius 
        { 
            get
            {
                return _radius;
            } 
            set
            {
                _radius = value;
            }
        }
    }
}