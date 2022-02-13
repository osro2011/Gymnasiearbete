using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Objects
{
    class Circle : PhysicsObject, ICircle
    {
        public int Radius { get; set; }
    }
}
