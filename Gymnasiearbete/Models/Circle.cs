namespace Gymnasiearbete.Models
{
    public class Circle : DrawablePhysicsObject
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