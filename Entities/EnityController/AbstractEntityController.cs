﻿using OpenTK.Input;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public abstract class AbstractEntityController
    {
        protected DynamicEntity ent;

        protected GameModel owner { get { return ent.owner; } }

        public AbstractEntityController(DynamicEntity e)
        {
            this.ent = e;
        }

        public virtual bool IsSingleTon()
        {
            return true;
        }

        public abstract void Update(KeyboardDevice keyboard);

        public abstract bool IsActive();

        public abstract void OnIllegalIntersection(Entity other);

        public abstract void OnHide();

        public abstract void OnReshow();

        public abstract Vec2d GetDelta();
    }
}