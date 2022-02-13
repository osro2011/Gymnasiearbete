using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Objects
{
    public abstract class PhysicsObject
    {
        protected Point _position = new Point(0, 0);
        protected Vector2 _velocity = new Vector2(0, 0);
        protected Vector2 _acceleration = new Vector2(0, 0);
        protected int _mass = 0;

        //TODO: Make conversions between pixels and meters.
        // Set properties
        public Point Position 
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
        public Vector2 Velocity
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
        public Vector2 Acceleration
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
        public int Mass
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
    }
}
