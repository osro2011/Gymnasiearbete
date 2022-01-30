using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Gymnasiearbete.Models;
using Gymnasiearbete.ViewModels;
using System;

namespace Gymnasiearbete.Views
{
    public partial class MainView : UserControl
    {
        public Canvas MainCanvas { get; set; }
        MainViewModel _vm;

        public MainView()
        {
            InitializeComponent();

            MainCanvas = this.FindControl<Canvas>("MainCanvas");

            _vm = new MainViewModel();
            DataContext = _vm;

            _vm.PhysicsTicked += (s, args) =>
            {
                DrawShapes();
            };

            _vm.DrawShapes += (s, args) =>
            {
                DrawShapes();
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DrawShapes()
        {
            // Maybe switch to DrawingContext? If I can figure out how it works anyway.
            MainCanvas.Children.Clear();
            
            foreach (DrawablePhysicsObject PhysicsShape in _vm.PhysicsShapes)
            {
                switch (PhysicsShape)
                {
                    case Rectangle Rectangle:
                        Avalonia.Controls.Shapes.Rectangle RectangleControl = new Avalonia.Controls.Shapes.Rectangle
                        {
                            Fill = new SolidColorBrush
                            {
                                Color = Rectangle.Color
                            },
                            Height = Rectangle.Height,
                            Width = Rectangle.Width,
                            ZIndex = 0
                        };
                        MainCanvas.Children.Add(RectangleControl);
                        Canvas.SetLeft(RectangleControl, Rectangle.Position.X);
                        Canvas.SetTop(RectangleControl, Rectangle.Position.Y);
                        Rectangle.ControlShape = RectangleControl;
                        break;

                    case Circle Circle:
                        Avalonia.Controls.Shapes.Ellipse CircleControl = new Avalonia.Controls.Shapes.Ellipse
                        {
                            Fill = new SolidColorBrush
                            {
                                Color = Circle.Color
                            },
                            Height = Circle.Radius * 2,
                            Width = Circle.Radius * 2,
                            ZIndex = 0
                        };
                        MainCanvas.Children.Add(CircleControl);
                        Canvas.SetLeft(CircleControl, Circle.Position.X);
                        Canvas.SetTop(CircleControl, Circle.Position.Y);
                        Circle.ControlShape = CircleControl;
                        break;

                    case Line Line:
                        Avalonia.Controls.Shapes.Line LineControl = new Avalonia.Controls.Shapes.Line
                        {
                            StrokeThickness = Line.Width,
                            StartPoint = Line.Position,
                            EndPoint = Line.Position + Line.Offset,
                            Stroke = new SolidColorBrush()
                            {
                                Color = Line.Color
                            },
                            ZIndex = 0
                        };
                        MainCanvas.Children.Add(LineControl);
                        Line.ControlShape = LineControl;
                        break;

                    case null:
                        throw new NullReferenceException();

                    default:
                        throw new Exception("Unknown shape");
                }
            }
        }
    }
}
