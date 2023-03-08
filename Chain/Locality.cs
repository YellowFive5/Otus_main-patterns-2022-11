#region Usings

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
        private readonly Dictionary<string, List<IMovable>> objectsByLocality = new();

        public Locality(int bias = 0)
        {
            this.bias = bias;
        }

        public void PlaceToLocality(List<IMovable> objects)
        {
            foreach (var obj in objects)
            {
                var keyX = (int)(obj.Position.X / (WorldSide / LocalitySide + bias));
                var keyY = (int)(obj.Position.Y / (WorldSide / LocalitySide + bias));
                var key = $"{keyX}_{keyY}";
                if (!objectsByLocality.ContainsKey(key))
                {
                    objectsByLocality.TryAdd(key, new List<IMovable> { obj });
                }
                else
                {
                    objectsByLocality[key].Add(obj);
                }
            }
        }

        public IEnumerable<IMovable> GetCollisionObjects()
        {
            foreach (var objects in objectsByLocality.Values)
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