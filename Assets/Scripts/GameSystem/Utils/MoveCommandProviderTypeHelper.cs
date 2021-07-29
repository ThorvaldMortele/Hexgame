using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Utils
{
    public class MoveCommandProviderTypeHelper
    {
        public static string[] _movementNames = new string[0];

        public static string[] FindMoveCommandProviderTypes() //memoisation = ervoor zorgen dat de code niet elke frame uitgevoerd word maar 1 keer wanneer nodig
        {
            if (_movementNames.Length == 0)
            {
                _movementNames = InternalFindMoveCommandProviderTypes();
            }

            return _movementNames;
        }

        private static string[] InternalFindMoveCommandProviderTypes()
        {
            var types = new List<string>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var attribute = type.GetCustomAttribute<MoveCommandProviderAttribute>();
                    if (attribute != null)
                    {
                        types.Add(attribute.Name);
                    }
                }
            }

            return types.ToArray();
        }
    }
}
