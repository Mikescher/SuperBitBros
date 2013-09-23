using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class FireballEntityController : AbstractEntityController
    {
        private const double ANGLESPEED = (2 * Math.PI) / 150.0;

        private Vec2d standardPos;
        private Vec2d midPos;

        private Vec2d deltaCache;
        private double angle = 0;

        public FireballEntityController(FireballEntity e, FireBoxBlock b, double fbdistance)
            : base(e)
        {
            midPos = new Vec2d(b.GetMiddle());
            standardPos = new Vec2d(midPos);
            standardPos.Y += fbdistance;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            angle += ANGLESPEED;
            if (angle >= 2 * Math.PI)
            {
                angle -= 2 * Math.PI;
            }

            Vec2d newpos = new Vec2d(standardPos);
            newpos.rotateAround(midPos, -angle);

            newpos -= 4;

            deltaCache = newpos - ent.position;
            ent.position = newpos;

            ent.DoCollisions();
        }

        public override bool IsActive()
        {
            return true;
        }

        public override void OnIllegalIntersection(Entity other)
        {

        }

        public override void OnHide()
        {

        }

        public override void OnReshow()
        {

        }

        public override Vec2d GetDelta()
        {
            return deltaCache;
        }
    }
}
