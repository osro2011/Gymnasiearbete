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
        public EventHandler? PhysicsTicked;

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

            // Loop through each physics object
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
        }
    }
}
