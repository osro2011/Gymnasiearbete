using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymnasiearbete.Models
{
    public interface IDrawable : INotifyPropertyChanged
    {
        public Color Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }
        public int XAcceleration { get; set; }
        public int YAcceleration { get; set; }
        public void InvokePropertyChanged();
    }
}
