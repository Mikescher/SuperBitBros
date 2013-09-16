using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace SuperBitBros.Entities.EnityController
{
    abstract class AbstractEntityController
    {
        protected DynamicEntity ent;

        protected GameModel owner { get { return ent.owner; } }

        public AbstractEntityController(DynamicEntity e)
        {
            this.ent = e;
        }

        public abstract void Update(KeyboardDevice keyboard);

        public abstract bool IsActive();

        public abstract void OnIllegalIntersection(Entity other);
    }
}
