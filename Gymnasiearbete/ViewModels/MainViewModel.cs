using Avalonia.Media;
using Gymnasiearbete.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Reactive;
using Engine;
using System.Collections.Generic;
using Engine.Objects;
using Newtonsoft.Json;
using System.IO;

namespace Gymnasiearbete.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public EventHandler? DrawShapes;
        public PhysicsEngine Engine;
        ObservableCollection<PhysicsObject>? _physicsShapes;
        public ObservableCollection<PhysicsObject>? PhysicsShapes 
        {
            get
            {
                
                return _physicsShapes;
            }
            set
            {
                _physicsShapes = value;
            }
        }

        // UI Properties
        IDrawable? _selected;
        public IDrawable? Selected { 
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
        private JsonSerializerSettings JsonSettings { get; } = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        // Button commands
        public ReactiveCommand<Unit, Unit> Start { get; }
        public ReactiveCommand<Unit, Unit> Stop { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public ReactiveCommand<Unit, Unit> Open { get; }
        public ReactiveCommand<Unit, Unit> DrawOnce { get; }
        public ReactiveCommand<Unit, Unit> CreateNewShape { get; }

        public MainViewModel()
        {
            PhysicsShapes = new ObservableCollection<PhysicsObject>();

            Random rd = new Random();

            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    PhysicsShapes.Add(new DrawableRectangle()
                    {
                        Width = 20,
                        Height = 20,
                        Position = new Avalonia.Point(i * 40, j * 40),
                        Color = new Color(255, (byte)rd.Next(0, 255), (byte)rd.Next(0, 255), (byte)rd.Next(0, 255)),
                        Velocity = new Vector2(rd.Next(-50, 50), rd.Next(-50, 50)),
                        Acceleration = new Vector2(rd.Next(-5, 5), rd.Next(-5, 5)),
                        Mass = rd.Next(1, 100)
                    });
                }
            }

            //PhysicsShapes.Add(new DrawableRectangle()
            //{
            //    Width = 20,
            //    Height = 20,
            //    Position = new Avalonia.Point(50, 50),
            //    Color = new Color(255, 255, 0, 0),
            //    Velocity = new Vector2(20, 20), // px/s
            //    Acceleration = new Vector2(0, 0),
            //    Mass = 20
            //});
            //PhysicsShapes.Add(new DrawableCircle()
            //{
            //    Radius = 10,
            //    Position = new Avalonia.Point(90, 90),
            //    Color = new Color(255, 255, 0, 0),
            //    Velocity = new Vector2(0, 0), // px/s
            //    Acceleration = new Vector2(0, 0),
            //    Mass = 30
            //});

            Engine = new PhysicsEngine();

            Engine.PhysicsObjects = new List<PhysicsObject>(PhysicsShapes);

            Engine.PhysicsTicked += (s, args) =>
            {
                PhysicsShapes = new ObservableCollection<PhysicsObject>(Engine.PhysicsObjects);
            };

            // Button commands implementations
            Start = ReactiveCommand.Create(() => 
            {
                Engine.Start();
                // Disable input while physics are running
                AllowInput = false;
            });

            Stop = ReactiveCommand.Create(() =>
            {
                Engine.Stop();
                // Enable input
                AllowInput = true;

                DrawShapes?.Invoke(this, EventArgs.Empty);
            });

            Save = ReactiveCommand.Create(() => 
            {
                //foreach (PhysicsObject PhysicsShape in PhysicsShapes)
                //{
                //    PhysicsShape.ControlShape = null;
                //}
                string Json = JsonConvert.SerializeObject(PhysicsShapes, JsonSettings);
                File.WriteAllText("./objects.json", Json);
            });

            Open = ReactiveCommand.Create(() =>
            {
                PhysicsShapes = JsonConvert.DeserializeObject<ObservableCollection<PhysicsObject>>(File.ReadAllText("./objects.json"), JsonSettings);
                Engine.PhysicsObjects = new List<PhysicsObject>(PhysicsShapes);
                DrawShapes?.Invoke(this, EventArgs.Empty);
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
                        Selected = new DrawableRectangle()
                        {
                            Position = new Avalonia.Point(0, 0),
                            Velocity = new Vector2(0, 0),
                            Acceleration = new Vector2(0, 0),
                            Mass = 0,
                            Color = new Color(255, 255, 0, 0),
                            Height = 20,
                            Width = 20
                        };
                        break;
                    case 1:
                        Selected = new DrawableCircle()
                        {
                            Position = new Avalonia.Point(0, 0),
                            Velocity = new Vector2(0, 0),
                            Acceleration = new Vector2(0, 0),
                            Mass = 0,
                            Color = new Color(255, 255, 0, 0),
                            Radius = 10
                        };
                        break;
                    default:
                        throw new Exception();
                }
                Engine.PhysicsObjects.Add((PhysicsObject)Selected);
                DrawShapes?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
