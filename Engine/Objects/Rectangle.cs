using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Objects
{
    public class Rectangle : PhysicsObject
    {
        virtual public int Height { get; set; }
        virtual public int Width { get; set; }
    }
}
