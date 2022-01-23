using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace Gymnasiearbete.Models
{
    public class Rectangle
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Color Color { get; set; }

        public Rectangle(double x, double y, int height, int width, Color color)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Color = color;
        }
    }
}
