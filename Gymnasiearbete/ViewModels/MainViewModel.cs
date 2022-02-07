using Avalonia;
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
        // Physics timers, events and properties
        private DispatcherTimer PhysicsTickTimer = new DispatcherTimer();
        private Stopwatch DeltaTimer = new Stopwatch();
        public EventHandler? PhysicsTicked;
        public EventHandler? DrawShapes;

        private long LastTimeElapsed = 0;
        private long CurrentTimeElapsed { get; set; }
        public ObservableCollection<DrawablePhysicsObject> PhysicsShapes { get; set; }

        // UI Properties
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
        public int SelectedShape { get; set; }

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

        // Button commands
        public ReactiveCommand<Unit, Unit> Start { get; }
        public ReactiveCommand<Unit, Unit> Stop { get; }
        public ReactiveCommand<Unit, Unit> DrawOnce { get; }
        public ReactiveCommand<Unit, Unit> CreateNewShape { get; }

        public MainViewModel()
        {
            PhysicsShapes = new ObservableCollection<DrawablePhysicsObject>();

            PhysicsTickTimer.Tick += PhysicsTickTimer_Tick;

            // Test shapes
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

            // Button commands implementations
            Start = ReactiveCommand.Create(() => 
            {
                // Start Stopwatch for DeltaTime
                DeltaTimer.Start();

                // Set FPS to 60 and start timer
                PhysicsTickTimer.Interval = new TimeSpan(10000000 / 60);
                PhysicsTickTimer.Start();

                // Disable input while physics are running
                AllowInput = false;
            });

            Stop = ReactiveCommand.Create(() =>
            {
                // Stop all timers
                PhysicsTickTimer.Stop();

                DeltaTimer.Stop();

                // Enable input
                AllowInput = true;
            });

            DrawOnce = ReactiveCommand.Create(() =>
            {
                DrawShapes?.Invoke(this, EventArgs.Empty);
            });

            CreateNewShape = ReactiveCommand.Create(() =>
            {
                // Could make a base class of DrawablePhysicsObject with base settings, but I don't know the syntax nor do I feel like researching it
                switch(SelectedShape) {
                    case 0:
                        Selected = new Rectangle()
                        {
                            Position = new Point(0, 0),
                            Velocity = new Vector2(0, 0),
                            Acceleration = new Vector2(0, 0),
                            Mass = 0,
                            Color = new Color(255, 255, 0, 0),
                            Height = 20,
                            Width = 20
                        };
                        break;
                    case 1:
                        Selected = new Circle()
                        {
                            Position = new Point(0, 0),
                            Velocity = new Vector2(0, 0),
                            Acceleration = new Vector2(0, 0),
                            Mass = 0,
                            Color = new Color(255, 255, 0, 0),
                            Radius = 10
                        };
                        break;
                    case 2:
                        Selected = new Line()
                        {
                            Position = new Point(0, 0),
                            Velocity = new Vector2(0, 0),
                            Acceleration = new Vector2(0, 0),
                            Mass = 0,
                            Color = new Color(255, 255, 0, 0),
                            Width = 2,
                            Offset = new Point(20, 20)
                        };
                        break;
                }
                PhysicsShapes.Add(Selected);
                DrawShapes?.Invoke(this, EventArgs.Empty);
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
