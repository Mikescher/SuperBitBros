using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Input;
using System.Collections.Generic;

namespace SuperBitBros.OpenGL {
    abstract class GameModel {
        public List<Entity> entityList { get; protected set; }

        public GameModel() {
            entityList = new List<Entity>();
        }

        public virtual void AddEntity(Entity e, double x, double y) {
            e.position.X = x;
            e.position.Y = y;
            entityList.Add(e);
            e.OnAdd(this);
        }

        public virtual bool RemoveEntity(Entity e) {
            e.OnRemove();
            return entityList.Remove(e);
        }

        public virtual List<Entity> GetCurrentEntityList() {
            return new List<Entity>(entityList);
        }

        public virtual void Update(KeyboardDevice keyboard) {
            foreach (Entity e in GetCurrentEntityList()) {
                e.Update(keyboard);
            }
        }

        public abstract Vector2d GetOffset(int window_width, int window_height);
    }
}
