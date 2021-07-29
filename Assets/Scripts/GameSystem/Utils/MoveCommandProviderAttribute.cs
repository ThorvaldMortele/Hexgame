using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Utils
{
    public class MoveCommandProviderAttribute : Attribute
    {
        public string Name;

        public MoveCommandProviderAttribute(string name)
        {
            Name = name;
        }
    }
}
