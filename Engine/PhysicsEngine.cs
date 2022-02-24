using Engine.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
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

        private void Collide(PhysicsObject Primary, PhysicsObject Secondary)
        {
            // 100% stolen from the source code of http://www.sciencecalculators.org/mechanics/collisions/ lmao
            // Fråga mig inte hur matten funkar för jag har ingen aning
            float x1 = (float)Primary.Position.X;
            float y1 = (float)Primary.Position.Y;
            float x2 = (float)Secondary.Position.X;
            float y2 = (float)Secondary.Position.Y;
            float vx1 = Primary.Velocity.X;
            float vy1 = Primary.Velocity.Y;
            float vx2 = Secondary.Velocity.X;
            float vy2 = Secondary.Velocity.Y;
            float m1 = Primary.Mass;
            float m2 = Secondary.Mass;

            float v1xnew = vx1 - 2 * m2 / (m1 + m2) * ((vx1 - vx2) * (x1 - x2) + (vy1 - vy2) * (y1 - y2)) / ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)) * (x1 - x2);
            float v1ynew = vy1 - 2 * m2 / (m1 + m2) * ((vx1 - vx2) * (x1 - x2) + (vy1 - vy2) * (y1 - y2)) / ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)) * (y1 - y2);
            Primary.Velocity = new Vector2(v1xnew, v1ynew);

            float v2xnew = vx2 - 2 * m1 / (m1 + m2) * ((vx2 - vx1) * (x2 - x1) + (vy2 - vy1) * (y2 - y1)) / ((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)) * (x2 - x1);
            float v2ynew = vy2 - 2 * m1 / (m1 + m2) * ((vx2 - vx1) * (x2 - x1) + (vy2 - vy1) * (y2 - y1)) / ((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)) * (y2 - y1);
            Secondary.Velocity = new Vector2(v2xnew, v2ynew);

            Secondary.Ignore.Add(Primary);
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

            List<PhysicsObject> NextPhysicsObjects = new List<PhysicsObject>();

            // Check for collisions
            foreach (PhysicsObject PhysicsObject in PhysicsObjects)
            {
                foreach (PhysicsObject CollisionObject in PhysicsObjects)
                {
                    if (CollisionObject != PhysicsObject && !PhysicsObject.Ignore.Contains(CollisionObject))
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
                                Collide(PhysicsRectangle, CollisionRectangle);
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
                // Add the processed object to new list
                NextPhysicsObjects.Add(PhysicsObject);
            }
            // Update old list
            foreach (PhysicsObject PhysicsObject in NextPhysicsObjects)
            {
                PhysicsObject.Ignore = new List<PhysicsObject>();
            }
            PhysicsObjects = NextPhysicsObjects;
        }
    }
}
