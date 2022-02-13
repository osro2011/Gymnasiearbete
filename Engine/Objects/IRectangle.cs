using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Objects
{
    public interface IRectangle
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public float Angle { get; set; }
    }
}
