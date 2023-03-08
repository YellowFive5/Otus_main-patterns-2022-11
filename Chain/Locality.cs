#region Usings

using System;
using System.Collections.Generic;
using Move;

#endregion

namespace Chain
{
    public class Locality
    {
        private const float WorldSide = 50.0f;
        private const int LocalitySide = 5;
        private readonly int bias;
        private readonly Dictionary<string, List<IMovable>> objects = new();

        public Locality(int bias = 0)
        {
            this.bias = bias;
        }

        public void PlaceToLocality(IMovable obj)
        {
            var keyX = Math.Abs(obj.Position.X / (WorldSide / LocalitySide + bias));
            var keyY = Math.Abs(obj.Position.Y / (WorldSide / LocalitySide + bias));
            objects[$"{keyX}_{keyY}"].Add(obj);
        }

        public IEnumerable<IMovable> GetCollisionObjects()
        {
            foreach (var objects in objects.Values)
            {
                if (objects.Count > 1) // object collision logic here - now all from locality in collision
                {
                    foreach (var obj in objects)
                    {
                        yield return obj;
                    }
                }
            }
        }
    }
}