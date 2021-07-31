using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Utils
{
    public class BreadthFirstAreaSearch<TPosition>
    {
        public delegate List<TPosition> NeighbourStrategy(TPosition from);

        public delegate float DistanceStrategy(TPosition from, TPosition to);

        private readonly NeighbourStrategy _neighbours;
        private readonly DistanceStrategy _distance;

        public BreadthFirstAreaSearch(NeighbourStrategy neighbours, DistanceStrategy distance)
        {
            _neighbours = neighbours;
            _distance = distance;
        }

        public List<TPosition> Area(TPosition centerPosition, float maxDistance)
        {
            var nearbyPositions = new List<TPosition>(); //lijst van alle posities die we verzamelen
            nearbyPositions.Add(centerPosition);

            var nodesToVisit = new Queue<TPosition>(); // lijst van alle nodes die we moeten bekijken
            nodesToVisit.Enqueue(centerPosition); //begint vanaf de centerpositie

            while (nodesToVisit.Count > 0)
            {
                var currentNode = nodesToVisit.Dequeue();
                var neighbours = _neighbours(currentNode); // we vragen de neighbours op van de centerpositie

                foreach (var neighbour in neighbours)
                {
                    if (nearbyPositions.Contains(neighbour))
                    {
                        continue;
                    }
                    if (_distance(centerPosition, neighbour) < maxDistance) // we kijken voor iedere buur if ie dicht genoeg ligt
                    {
                        nearbyPositions.Add(neighbour); // als ie dicht genoeg is slaan we hem op
                        nodesToVisit.Enqueue(neighbour); // en dan zorgen we ervoor dat zijn kinderen ook bezocht worden
                    }
                }
            }

            return nearbyPositions;
        }
    }
}
