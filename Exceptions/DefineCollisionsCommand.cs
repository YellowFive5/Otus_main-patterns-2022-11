#region Usings

using System.Collections.Generic;
using System.Linq;
using Chain;
using Command;
using Move;

#endregion

namespace Exceptions
{
    public class DefineCollisionsCommand : ICommand
    {
        private readonly Server server;
        private readonly List<IMovable> gameObjects;
        private readonly int localityNumber;

        public DefineCollisionsCommand(Server server, List<IMovable> gameObjects, int localityNumber = 1)
        {
            this.server = server;
            this.gameObjects = gameObjects;
            this.localityNumber = localityNumber;
        }

        public void Execute()
        {
            for (int i = 0; i < localityNumber; i++)
            {
                var locality = new Locality(i);
                locality.PlaceToLocality(gameObjects);
                foreach (var collisionObject in locality.GetCollisionObjects())
                {
                    if (server.CollisionObjects.All(o => o != collisionObject))
                    {
                        server.CollisionObjects.Add(collisionObject);
                    }
                }
            }
        }
    }
}