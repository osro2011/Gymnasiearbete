using Avalonia.Media;
using Gymnasiearbete.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Reactive;
using Engine;
using System.Collections.Generic;
using System.Linq;
using Engine.Objects;
using System.ComponentModel;
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
            PhysicsShapes.Add(new DrawableRectangle()
            {
                Height = 20,
                Width = 20,
                Position = new Avalonia.Point(50, 50),
                Color = new Color(255, 255, 0, 0),
                Velocity = new Vector2(0, 0), // px/s
                Acceleration = new Vector2(0, 0),
                Mass = 20
            });
            PhysicsShapes.Add(new DrawableRectangle()
            {
                Height = 20,
                Width = 20,
                Position = new Avalonia.Point(90, 50),
                Color = new Color(255, 255, 0, 0),
                Velocity = new Vector2(-20, 0), // px/s
                Acceleration = new Vector2(0, 0),
                Mass = 10
            });

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
                PhysicsShapes.Add((PhysicsObject)Selected);
                DrawShapes?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
