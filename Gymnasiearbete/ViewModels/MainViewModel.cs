using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using Gymnasiearbete.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;

namespace Gymnasiearbete.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DispatcherTimer PhysicsTickTimer = new DispatcherTimer();
        private Stopwatch DeltaTimer = new Stopwatch();
        public EventHandler PhysicsTicked;

        private long LastTimeElapsed = 0;
        private long CurrentTimeElapsed { get; set; }

        public List<PhysicsObject> PhysicsShapes { get; set; }
        //public ObservableCollection<Shape> RenderShapes { get; set; }

        public MainViewModel()
        {
            PhysicsShapes = new List<PhysicsObject>();

            PhysicsTickTimer.Tick += PhysicsTickTimer_Tick;

            // Add test square
            PhysicsShapes.Add(new Models.Rectangle()
            {
                Height = 20,
                Width = 20,
                Position = new Point(20, 20),
                Color = new Color(255, 255, 0, 0),
                Velocity = new Vector2(20, 0) // px/s
            });

            // TODO: Add a start button to do this (Also a stop button)

            // Start Stopwatch for DeltaTime (Should be redundant since program is light but might as well)
            DeltaTimer.Start();

            // Set FPS to 60 and start timer
            PhysicsTickTimer.Interval = new TimeSpan(10000000 / 60);
            PhysicsTickTimer.Start();
        }

        private void PhysicsTickTimer_Tick(object? sender, EventArgs e)
        {
            MoveShapes();

            PhysicsTicked?.Invoke(this, EventArgs.Empty);
        }

        private void MoveShapes()
        {
            CurrentTimeElapsed = DeltaTimer.ElapsedMilliseconds;
            long DeltaTime = CurrentTimeElapsed - LastTimeElapsed;
            LastTimeElapsed = CurrentTimeElapsed;

            // Move every shape 1px to the right
            foreach (PhysicsObject PhysicsShape in PhysicsShapes)
            {
                double nextX = PhysicsShape.Position.X;
                double nextY = PhysicsShape.Position.Y;

                PhysicsShape.Velocity += PhysicsShape.Acceleration;

                nextX += PhysicsShape.Velocity.X * DeltaTime / 1000;
                nextY += PhysicsShape.Velocity.Y * DeltaTime / 1000;

                PhysicsShape.Position = new Point(nextX, nextY);
            }
        }
    }
}
