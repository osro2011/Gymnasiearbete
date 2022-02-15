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
        new public event PropertyChangedEventHandler? PropertyChanged;

        public PhysicsEngine Engine;
        ObservableCollection<DrawablePhysicsObject> _physicsShapes;
        public ObservableCollection<DrawablePhysicsObject> PhysicsShapes 
        {
            get
            {
                return _physicsShapes;
            }
            set
            {
                _physicsShapes = value;
                // This is not a great way of doing this and should probably be revised
                // Unable to do this properly because values are changed in parent of DrawablePhysicsObject
                foreach (DrawablePhysicsObject PhysicsShape in _physicsShapes)
                {
                    PhysicsShape.NotifyPropertyChanged();
                }
            }
        }

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
            PhysicsShapes = new ObservableCollection<DrawablePhysicsObject>();

            // Test shapes
            PhysicsShapes.Add(new DrawableRectangle()
            {
                Height = 20,
                Width = 20,
                Position = new Avalonia.Point(20, 20),
                Color = new Color(255, 255, 0, 0),
                Velocity = new Vector2(20, 0), // px/s
                Acceleration = new Vector2(0, 0)
            });
            PhysicsShapes.Add(new DrawableCircle()
            {
                Radius = 10,
                Position = new Avalonia.Point(20, 50),
                Color = new Color(255, 0, 255, 0),
                Velocity = new Vector2(20, 0)
            });
            PhysicsShapes.Add(new DrawableLine()
            {
                Position = new Avalonia.Point(20, 80),
                Offset = new Avalonia.Point(20, 20),
                Color = new Color(255, 0, 0, 255),
                Velocity = new Vector2(20, 0),
                Width = 5
            });

            Selected = PhysicsShapes[0];

            Engine = new PhysicsEngine();

            Engine.PhysicsObjects = new List<PhysicsObject>(PhysicsShapes);

            Engine.PhysicsTicked += (s, args) =>
            {
                PhysicsShapes = new ObservableCollection<DrawablePhysicsObject>(Engine.PhysicsObjects.Cast<DrawablePhysicsObject>().ToList());
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
                string Json = JsonConvert.SerializeObject(PhysicsShapes, JsonSettings);
                File.WriteAllText("./objects.json", Json);
            });

            Open = ReactiveCommand.Create(() =>
            {
                PhysicsShapes = JsonConvert.DeserializeObject<ObservableCollection<DrawablePhysicsObject>>(File.ReadAllText("./objects.json"), JsonSettings);
                Engine.PhysicsObjects = new List<PhysicsObject>(PhysicsShapes);
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
                    case 2:
                        Selected = new DrawableLine()
                        {
                            Position = new Avalonia.Point(0, 0),
                            Velocity = new Vector2(0, 0),
                            Acceleration = new Vector2(0, 0),
                            Mass = 0,
                            Color = new Color(255, 255, 0, 0),
                            Width = 2,
                            Offset = new Avalonia.Point(20, 20)
                        };
                        break;
                }
                PhysicsShapes.Add(Selected);
                DrawShapes?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
