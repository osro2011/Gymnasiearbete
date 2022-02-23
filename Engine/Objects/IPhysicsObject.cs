using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Objects
{
    public interface IPhysicsObject
    {
        public List<Vector2> Forces { get; set; }
        public Point Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public int Mass { get; set; }
    }
}
