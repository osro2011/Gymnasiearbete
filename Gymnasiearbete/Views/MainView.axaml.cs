using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;
using Gymnasiearbete.Models;
using Gymnasiearbete.ViewModels;
using System;
using System.ComponentModel;

namespace Gymnasiearbete.Views
{
    public partial class MainView : UserControl
    {
        public Canvas MainCanvas { get; set; }

        MainViewModel _vm;

        public MainView()
        {
            InitializeComponent();

            // Get main canvas
            MainCanvas = this.FindControl<Canvas>("MainCanvas");

            // Update mouse coordinates
            MainCanvas.PointerMoved += (s, args) =>
            {
                this.FindControl<TextBlock>("Coords").Text = args.GetCurrentPoint(MainCanvas).Position.ToString();
            };

            // Set datacontext and viewmodel
            _vm = new MainViewModel();
            DataContext = _vm;

            // Subscribe to events
            _vm.Engine.PhysicsTicked += (s, args) =>
            {
                DrawShapesOnUIThread();
            };

            _vm.DrawShapes += (s, args) =>
            {
                DrawShapesOnUIThread();
            };
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DrawShapesOnUIThread()
        {
            if (!Dispatcher.UIThread.CheckAccess())
            {
                Dispatcher.UIThread.InvokeAsync(DrawShapes, DispatcherPriority.MinValue);
            }
            else
            {
                DrawShapes();
            }
        }

        // Draw the shapes in PhysicsShapes
        private void DrawShapes()
        {
            // Maybe switch to DrawingContext? If I can figure out how it works anyway.
            MainCanvas.Children.Clear();
            
            foreach (DrawablePhysicsObject PhysicsShape in _vm.PhysicsShapes)
            {
                switch (PhysicsShape)
                {
                    case DrawableRectangle Rectangle:
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
                        Canvas.SetLeft(RectangleControl, Rectangle.Position.X);
                        Canvas.SetTop(RectangleControl, Rectangle.Position.Y);
                        Rectangle.ControlShape = RectangleControl;
                        break;

                    case DrawableCircle Circle:
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
                        Canvas.SetLeft(CircleControl, Circle.Position.X);
                        Canvas.SetTop(CircleControl, Circle.Position.Y);
                        Circle.ControlShape = CircleControl;
                        break;

                    case DrawableLine Line:
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
                        Line.ControlShape = LineControl;
                        break;

                    case null:
                        throw new NullReferenceException();

                    default:
                        throw new Exception("Unknown shape");
                }
                MainCanvas.Children.Add(PhysicsShape.ControlShape);

                // Add click handling
                PhysicsShape.ControlShape.Cursor = new Cursor(StandardCursorType.Hand);
                PhysicsShape.ControlShape.PointerPressed += (s, args) =>
                {
                    _vm.Selected = PhysicsShape;
                };
                
    }
        }
    }
}