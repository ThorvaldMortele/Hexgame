using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Utils
{
    public class AStarPathFinding<TPosition>
    {
        public delegate List<TPosition> NeighbourStrategy(TPosition from);

        public delegate float DistanceStrategy(TPosition from, TPosition toNeighbour);
        //maakt de exacte berekening om van 1 node naar 1 van zen neighbours te gaan

        public delegate float HeuristicStrategy(TPosition from, TPosition to);
        //gokt de afstand tussen 2 nodes op een bord

        private NeighbourStrategy _neighbours;
        private DistanceStrategy _distance;
        private HeuristicStrategy _heuristic;

        public AStarPathFinding(NeighbourStrategy neighbours, DistanceStrategy distance, HeuristicStrategy heuristic)
        {
            _neighbours = neighbours;
            _distance = distance;
            _heuristic = heuristic;
        }

        public List<TPosition> Path(TPosition from, TPosition to)
        {
            var openSet = new List<TPosition>() { from };

            var cameFrom = new Dictionary<TPosition, TPosition>();

            var gScores = new Dictionary<TPosition, float>() { { from, 0f } };

            var fScores = new Dictionary<TPosition, float> { { from, _heuristic(from, to) } };

            while (openSet.Count > 0)
            {
                TPosition current = FindLowestfScore(fScores, openSet);

                if (current.Equals(to)) return ReconstructPath(cameFrom, current);

                openSet.Remove(current);
                var neighbours = _neighbours(current);

                foreach (var neighbour in neighbours)
                {
                    var tentativegScore = gScores[current] + _distance(current, neighbour);
                    if (tentativegScore < gScores.GetValueOrDefault(neighbour, float.PositiveInfinity))
                    {
                        cameFrom[neighbour] = current;
                        gScores[neighbour] = tentativegScore;
                        fScores[neighbour] = gScores[neighbour] + _heuristic(neighbour, to);

                        if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                    }
                }
            }

            return new List<TPosition>(0);
        }

        private List<TPosition> ReconstructPath(Dictionary<TPosition, TPosition> cameFrom, TPosition current)
        {
            var path = new List<TPosition>() { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current);
            }

            return path;
        }

        private TPosition FindLowestfScore(Dictionary<TPosition, float> fScore, List<TPosition> openSet)
        {
            TPosition currentNode = openSet[0];

            foreach (var node in openSet)
            {
                var currentfScore = fScore.GetValueOrDefault(currentNode, float.PositiveInfinity);
                var newfScore = fScore.GetValueOrDefault(node, float.PositiveInfinity);

                if (newfScore < currentfScore)
                {
                    currentNode = node;
                }
            }

            return currentNode;
        }
    }
}
