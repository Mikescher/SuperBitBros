using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Input;

namespace SuperBitBros
{
    abstract class GameModel
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
            e.OnAdd(this);
        }

        public virtual void Update(KeyboardDevice keyboard)
        {
            foreach(Entity entity in entityList) {
                entity.Update(keyboard);
            }
        }

        public abstract Vector2d GetOffset(int window_width, int window_height);
    }
}
