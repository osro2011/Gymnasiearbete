using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gymnasiearbete.Models
{
    public abstract class PhysicsObject
    {
        //TODO: Make conversions between pixels and meters.
        public Vector2 Acceleration { get; set; }
        public Vector2 Velocity { get; set; }
        public int Mass { get; set; }
        public Point Position { get; set; }
    }
}
