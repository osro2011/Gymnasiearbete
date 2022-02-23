using Engine.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;

namespace Engine
{
    public class PhysicsEngine
    {
        private Timer PhysicsTickTimer = new Timer();
        private Stopwatch DeltaTimer = new Stopwatch();
        public EventHandler PhysicsTicked;

        private long LastTimeElapsed = 0;
        private long CurrentTimeElapsed { get; set; }

        public List<PhysicsObject> PhysicsObjects { get; set; }

        public PhysicsEngine()
        {
            PhysicsTickTimer = new Timer(1000 / 60);
            PhysicsTickTimer.Elapsed += PhysicsTickTimer_Tick;
            PhysicsTickTimer.AutoReset = true;

            PhysicsObjects = new List<PhysicsObject>();
        }

        public void Start()
        {
            DeltaTimer.Start();
            PhysicsTickTimer.Enabled = true;
        }

        public void Stop()
        {
            DeltaTimer.Stop();
            PhysicsTickTimer.Enabled = false;
        }

        private void PhysicsTickTimer_Tick(object? sender, EventArgs e)
        {
            MoveShapes();

            PhysicsTicked?.Invoke(this, EventArgs.Empty);
        }

        private void MoveShapes()
        {
            // Set up delta time
            CurrentTimeElapsed = DeltaTimer.ElapsedMilliseconds;
            long DeltaTime = CurrentTimeElapsed - LastTimeElapsed;
            LastTimeElapsed = CurrentTimeElapsed;

            // Move all shapes
            foreach (PhysicsObject PhysicsObject in PhysicsObjects)
            {
                double nextX = PhysicsObject.Position.X;
                double nextY = PhysicsObject.Position.Y;

                // Change velocity
                PhysicsObject.Velocity += PhysicsObject.Acceleration * DeltaTime / 1000;

                // Set next position
                nextX += PhysicsObject.Velocity.X * DeltaTime / 1000;
                nextY += PhysicsObject.Velocity.Y * DeltaTime / 1000;

                // Change position
                PhysicsObject.Position = new Point(nextX, nextY);
            }

            // Check for collisions
            foreach (PhysicsObject PhysicsObject in PhysicsObjects)
            {
                foreach (PhysicsObject CollisionObject in PhysicsObjects)
                {
                    if (CollisionObject != PhysicsObject)
                    {
                        if (CollisionObject is Rectangle && PhysicsObject is Rectangle)
                        {
                            // Cast general physics classes to specific rectangle class
                            Rectangle PhysicsRectangle = (Rectangle)PhysicsObject;
                            Rectangle CollisionRectangle = (Rectangle)CollisionObject;
                            if (PhysicsRectangle.Position.X + PhysicsRectangle.Width >= CollisionRectangle.Position.X &&
                                PhysicsRectangle.Position.X <= CollisionRectangle.Position.X + CollisionRectangle.Width &&
                                PhysicsRectangle.Position.Y + PhysicsRectangle.Height >= CollisionRectangle.Position.Y &&
                                PhysicsRectangle.Position.Y <= CollisionRectangle.Position.Y + CollisionRectangle.Height)
                            {
                                // Collision between PhysicsRectangle and CollisionRectangle
                                PhysicsRectangle.Position.X = 500;
                            }
                        }
                        else if (CollisionObject is Circle && PhysicsObject is Circle)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
}
