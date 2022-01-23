using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Gymnasiearbete.Views
{
    public partial class MainView : UserControl
    { 
        private Stopwatch Timer { get; set; }
        private long CurrentElapsed { get; set; }
        private long LastElapsed { get; set; }
        private int DeltaTime { get; set; }

        public ObservableCollection<Models.Rectangle> PhysicsShapes { get; set; }
        public List<Shape> ControlShapes { get; set; }
        public Canvas MainCanvas { get; set; }

        public MainView()
        {
            InitializeComponent();

            PhysicsShapes = new ObservableCollection<Models.Rectangle>();
            ControlShapes = new List<Shape>();
            Timer = new Stopwatch();
            LastElapsed = 0;

            PhysicsShapes.CollectionChanged += new NotifyCollectionChangedEventHandler(DrawShapes);

            MainCanvas = this.FindControl<Canvas>("MainCanvas");

            PhysicsShapes.Add(new Models.Rectangle(20, 20, 20, 20, new Color(255, 255, 0, 0)));

            Timer.Start(); // Move later on, should be button activated

            // Needs better main loop
            while (true)
            {
                // Set DeltaTime and Time
                CurrentElapsed = Timer.ElapsedMilliseconds;
                DeltaTime = (int)CurrentElapsed - (int)LastElapsed;
                LastElapsed = CurrentElapsed;

                foreach (Models.Rectangle PhysicsShape in PhysicsShapes)
                {
                    PhysicsShape.X = CurrentElapsed;
                }
            }
        }

        public void DrawShapes(object? sender, EventArgs e)
        {
            ControlShapes.Clear();
            foreach (Models.Rectangle PhysicsShape in PhysicsShapes)
            {
                Rectangle RectangleControl = new Rectangle
                {
                    Fill = new SolidColorBrush
                    {
                        Color = PhysicsShape.Color
                    },
                    Height = PhysicsShape.Height,
                    Width = PhysicsShape.Width
                };
                MainCanvas.Children.Add(RectangleControl);
                Canvas.SetLeft(RectangleControl, PhysicsShape.X);
                Canvas.SetTop(RectangleControl, PhysicsShape.Y);

                ControlShapes.Add(RectangleControl);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
