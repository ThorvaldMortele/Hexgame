using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Utils
{
    public static class PositionExtension
    {
        public static Vector3 AsVector3(this Position position)
        {
            return new Vector3(position.X, 0, position.Y);
        }
    }
}
