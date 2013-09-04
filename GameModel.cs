using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros
{
    class GameModel
    {
        public List<Entity> entityList {get; protected set;}

        public GameModel()
        {
            entityList = new List<Entity>();
        }

        public virtual void AddEntity(Entity e, double x, double y)
        {
            e.position.X = x;
            e.position.Y = y;
            entityList.Add(e);
            e.OnAdd();
        }
    }
}
