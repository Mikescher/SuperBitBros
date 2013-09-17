using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class DefaultMobController : AbstractNewtonEntityController
    {
        protected int walkDirection = -1; // +1 ||-1 || 0

        public const double MOB_ACC = 0.2;
        public const double MOB_SPEED = 1;

        public DefaultMobController(Mob e)
            : base(e)
        {
            //---
        }

        public override void Update(KeyboardDevice keyboard)
        {
            Vec2d delta = new Vec2d(0, 0);

            Vec2i blockPos;
            if (walkDirection < 0)
                blockPos = (Vec2i)(new Vec2d(ent.position.X + ent.width, ent.position.Y) / Block.BLOCK_SIZE);
            else
                blockPos = (Vec2i)(ent.position / Block.BLOCK_SIZE);

            int y = (int)blockPos.Y - 1;
            int x = (int)blockPos.X + walkDirection;

            if (ent.IsOnGround())
            {
                Block next = ent.owner.GetBlock(x, y);
                if (next == null || !Entity.TestBlocking(next, ent))
                {
                    walkDirection *= -1;
                }
            }

            if ((walkDirection > 0 && ent.IsCollidingRight()) || (walkDirection < 0 && ent.IsCollidingLeft()))
            {
                walkDirection *= -1;
            }

            if ((walkDirection < 0 && movementDelta.X > -MOB_SPEED) || (walkDirection > 0 && movementDelta.X < MOB_SPEED))
            {
                delta.X = MOB_ACC * walkDirection;
            }

            if (walkDirection < 0) blockPos = (Vec2i)(new Vec2d(ent.position.X + ent.width, ent.position.Y) / Block.BLOCK_SIZE);
            else blockPos = (Vec2i)(ent.position / Block.BLOCK_SIZE);
            x = (int)blockPos.X + walkDirection;
            y = (int)blockPos.Y - 1;
            Block realnext = ent.owner.GetBlock(x, y);

            if (realnext != null && Entity.TestBlocking(realnext, ent))
            {
                DoGravitationalMovement(delta);
            }
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}