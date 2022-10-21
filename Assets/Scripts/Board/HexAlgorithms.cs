using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Board
{
    public static class HexAlgorithms
    {
        static Vector2Int[] _axialDirectionVectors = {
            new (+1, 0), new (+1, -1), new (0, -1),
            new (-1, 0), new (-1, +1), new (0, +1),
        };

        public static Vector2Int AxialDirection(int direction)
        {
            return _axialDirectionVectors[direction];
        }

        public static Vector2Int AxialNeighbor(Vector2Int hex, int direction)
        {
            return hex + AxialDirection(direction);
        }

        public static Vector2Int[] GetReachableHexes(Vector2Int start, int range, Func<Vector2Int, bool> isBlockChecker)
        {
            var visited = new HashSet<Vector2Int> { start };
            var fringes = new List<List<Vector2Int>> { new() {start} };

            for (var k = 1; k <= range; k++)
            {
                fringes.Add(new List<Vector2Int>());
                foreach (var hex in fringes[k-1])
                {
                    foreach (var direction in 0.To(5))
                    {
                        var neighbor = AxialNeighbor(hex, direction);

                        if (!visited.Contains(neighbor) && !isBlockChecker.Invoke(neighbor))
                        {
                            visited.Add(neighbor);
                            fringes[k].Add(neighbor);
                        }
                    }
                }
            }
            
            return visited.ToArray();
        }
    }
}