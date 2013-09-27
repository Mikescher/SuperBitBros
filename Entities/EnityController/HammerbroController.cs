using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class HammerbroController : DefaultMobController
    {
        private const double MAX_DISTANCE = 8;

        private static Random rand = new Random();

        private Vec2d startPos = null;
        private double mdistance = MAX_DISTANCE + rand.NextDouble() * 16 - 4;

        public HammerbroController(Hammerbro p)
            : base(p)
        {
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (startPos == null)
                startPos = new Vec2d(ent.position);

            base.Update(keyboard);
        }

        protected override bool IsCollidingInWalkingDirection()
        {
            bool result_base = base.IsCollidingInWalkingDirection();

            bool result_now = (walkDirection < 0 && (startPos.X - ent.position.X) > mdistance) || (walkDirection > 0 && (ent.position.X - startPos.X) > mdistance);

            return result_base || result_now;
        }
    }
}
