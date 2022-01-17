using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace Gymnasiearbete.Views
{
    public partial class MainView : UserControl
    {
        public List<Shape> Shapes { get; set; }
        public MainView()
        {
            

            DrawShapes();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DrawShapes ()
        {

        }
    }
}
