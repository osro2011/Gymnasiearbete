using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Gymnasiearbete.Models;
using Gymnasiearbete.ViewModels;
using System.Collections.Generic;

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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DrawShapes()
        {
            MainCanvas.Children.Clear();
            foreach (Models.Rectangle PhysicsShape in _vm.PhysicsShapes)
            {
                if (PhysicsShape.ControlShape == null)
                {
                    Avalonia.Controls.Shapes.Rectangle RectangleControl = new Avalonia.Controls.Shapes.Rectangle
                    {
                        Fill = new SolidColorBrush
                        {
                            Color = PhysicsShape.Color
                        },
                        Height = PhysicsShape.Height,
                        Width = PhysicsShape.Width
                    };
                    MainCanvas.Children.Add(RectangleControl);
                    Canvas.SetLeft(RectangleControl, PhysicsShape.Position.X);
                    Canvas.SetTop(RectangleControl, PhysicsShape.Position.Y);
                }
            }
        }
    }
}
