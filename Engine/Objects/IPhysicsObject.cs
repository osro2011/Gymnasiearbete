using System.Numerics;

namespace Engine.Objects
{
    public interface IPhysicsObject
    {
        public Vector2 ResultantForce { get; set; }
        public Point Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public int Mass { get; set; }
    }
}
