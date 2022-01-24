using Avalonia;
using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymnasiearbete.Models
{
    public class SnakePart
    {
        public Shape UiElement { get; set; }
        public Point Position { get; set; }
        public bool IsHead { get; set; }

        
    }
}
