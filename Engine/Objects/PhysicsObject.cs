using System.Collections.Generic;
using System.Numerics;

namespace Engine.Objects
{
    // Everything relating to the physics should go in here
    public abstract class PhysicsObject: IPhysicsObject
    {
        private Vector2 _resultantForce = new Vector2(0, 0);
        private Point _position = new Point(0, 0);
        private Vector2 _velocity = new Vector2(0, 0);
        private Vector2 _acceleration = new Vector2(0, 0);
        private int _mass = 0;

        //TODO: Make conversions between pixels and meters.
        // Set properties
        public List<PhysicsObject> Ignore { get; set; } = new List<PhysicsObject>(); // Objects to not interact with (Just collisions for now)
        public Vector2 ResultantForce
        {
            get
            {
                return _resultantForce;
            }
            set
            {
                _resultantForce = value;
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

        public void AddForce(Vector2 Force)
        {
            ResultantForce += Force;
        }
    }
}
