using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Collections.Generic;

namespace Gymnasiearbete.Views
{
    public partial class MainView : UserControl
    {
        public List<Shape>? Shapes { get; set; }
        public Rectangle TestRect { get; set; }
        public Canvas MainCanvas { get; set; }
        public MainView()
        {
            InitializeComponent();

            MainCanvas = this.FindControl<Canvas>("MainCanvas");

            TestRect = new Rectangle
            {
                Fill = new SolidColorBrush
                {
                    Color = new Color(255, 255, 0, 0)
                },
                Height = 200,
                Width = 200
            };
            MainCanvas.Children.Add(TestRect);
            Canvas.SetTop(TestRect, 20);
            Canvas.SetLeft(TestRect, 20);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
