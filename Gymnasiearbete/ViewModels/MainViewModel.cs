﻿using Avalonia;
using Avalonia.Media;
using Avalonia.Threading;
using Gymnasiearbete.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Reactive;

namespace Gymnasiearbete.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DispatcherTimer PhysicsTickTimer = new DispatcherTimer();
        private Stopwatch DeltaTimer = new Stopwatch();
        public EventHandler? PhysicsTicked;
        public EventHandler? DrawShapes;

        private long LastTimeElapsed = 0;
        private long CurrentTimeElapsed { get; set; }
        public ObservableCollection<DrawablePhysicsObject> PhysicsShapes { get; set; }
        DrawablePhysicsObject? _selected;
        public DrawablePhysicsObject? Selected { 
            get
            {
                return _selected;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selected, value);
            }
        }

        public ReactiveCommand<Unit, Unit> Start { get; }
        public ReactiveCommand<Unit, Unit> Stop { get; }
        public ReactiveCommand<Unit, Unit> DrawOnce { get; }
        public ReactiveCommand<Unit, Unit> SetValues { get; }
        bool _allowInput = true;
        public bool AllowInput
        {
            get
            {
                return _allowInput;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _allowInput, value);
            }
        }

        

        public MainViewModel()
        {
            PhysicsShapes = new ObservableCollection<DrawablePhysicsObject>();

            PhysicsTickTimer.Tick += PhysicsTickTimer_Tick;

            // Add test square
            PhysicsShapes.Add(new Rectangle()
            {
                Height = 20,
                Width = 20,
                Position = new Point(20, 20),
                Color = new Color(255, 255, 0, 0),
                Velocity = new Vector2(20, 0), // px/s
                Acceleration = new Vector2(1, 1)
            });
            PhysicsShapes.Add(new Circle()
            {
                Radius = 10,
                Position = new Point(20, 50),
                Color = new Color(255, 0, 255, 0),
                Velocity = new Vector2(20, 0)
            });
            PhysicsShapes.Add(new Line()
            {
                Position = new Point(20, 80),
                Offset = new Point(20, 20),
                Color = new Color(255, 0, 0, 255),
                Velocity = new Vector2(20, 0),
                Width = 5
            });

            Selected = PhysicsShapes[0];

            // TODO: Add a start button to do this (Also a stop button)
            Start = ReactiveCommand.Create(() => 
            {
                // Start Stopwatch for DeltaTime (Should be redundant since program is light but might as well)
                DeltaTimer.Start();

                // Set FPS to 60 and start timer
                PhysicsTickTimer.Interval = new TimeSpan(10000000 / 60);
                PhysicsTickTimer.Start();

                AllowInput = false;
            });

            Stop = ReactiveCommand.Create(() =>
            {
                // Stop all timers
                PhysicsTickTimer.Stop();

                DeltaTimer.Stop();

                AllowInput = true;
            });

            DrawOnce = ReactiveCommand.Create(() =>
            {
                DrawShapes?.Invoke(this, EventArgs.Empty);
            });

            SetValues = ReactiveCommand.Create(() =>
            {
                Selected.Position = new Point();
            });
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
            foreach (PhysicsObject PhysicsShape in PhysicsShapes)
            {
                double nextX = PhysicsShape.Position.X;
                double nextY = PhysicsShape.Position.Y;

                // Change velocity
                PhysicsShape.Velocity += PhysicsShape.Acceleration * DeltaTime / 1000;

                // Set next position
                nextX += PhysicsShape.Velocity.X * DeltaTime / 1000;
                nextY += PhysicsShape.Velocity.Y * DeltaTime / 1000;

                // Change position
                PhysicsShape.Position = new Point(nextX, nextY);
            }
        }
    }
}
