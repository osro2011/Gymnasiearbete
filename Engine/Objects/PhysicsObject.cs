using System.Collections.Generic;
using System.Numerics;

namespace Engine.Objects
{
    // Everything relating to the physics should go in here
    public abstract class PhysicsObject: IPhysicsObject
    {
        protected List<Vector2> _forces = new List<Vector2>();
        protected Point _position = new Point(0, 0);
        protected Vector2 _velocity = new Vector2(0, 0);
        protected Vector2 _acceleration = new Vector2(0, 0);
        protected int _mass = 0;

        //TODO: Make conversions between pixels and meters.
        // Set properties
        public List<Vector2> Forces
        {
            get
            {
                return _forces;
            }
            set
            {
                _forces = value;
            }
        }
        virtual public Point Position 
        { 
            get 
            {
                return _position;
            }
            set 
            {
                _position = value;
            }
        }
        virtual public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }
        virtual public Vector2 Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                _acceleration = value;
            }
        }
        virtual public int Mass
        {
            get
            {
                return _mass;
            }
            set
            {
                _mass = value;
            }
        }

        
        public Vector2 GetMomentum()
        {
            return Mass * Velocity;
        }

        public void AddImpulse(Vector2 Impulse)
        {
            Velocity += (Impulse / Mass);
        }

        public void AddForce(Vector2 Force)
        {
            Forces.Add(Force);
        }
    }
}
