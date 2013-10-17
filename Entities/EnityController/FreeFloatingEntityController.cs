using OpenTK.Input;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    class FreeFloatingEntityController : AbstractEntityController
    {
        private static Random rand = new Random();

        private int floatpos = 0;
        private double floatDist;
        private int floatDur;
        private double standardY = Double.MinValue;

        public FreeFloatingEntityController(DynamicEntity p, double floatDistance, int floatDuration)
            : base(p)
        {
            floatDist = floatDistance;
            floatDur = floatDuration;

            floatpos = (int)(rand.NextDouble() * floatDuration);
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (standardY == Double.MinValue)
                standardY = ent.position.Y;

            floatpos++;

            if (floatpos >= floatDur)
                floatpos -= floatDur;

            double inc = Math.Sin((floatpos * 2 * Math.PI) / floatDur) * floatDist;

            ent.position.Y = standardY + inc;

            ent.DoCollisions();
        }

        public override bool IsActive()
        {
            return true;
        }

        public override void OnIllegalIntersection(Entity other)
        {
            //ignore
        }

        public override Vec2d GetDelta()
        {
            return Vec2d.Zero;
        }

        public override void OnHide()
        {
            //empty
        }

        public override void OnReshow()
        {
            //empty
        }
    }
}
