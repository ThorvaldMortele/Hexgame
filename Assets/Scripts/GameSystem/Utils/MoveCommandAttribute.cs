using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Utils
{
    public class MoveCommandAttribute : Attribute
    {
        public string Name;

        public MoveCommandAttribute(string name)
        {
            Name = name;
        }
    }
}
