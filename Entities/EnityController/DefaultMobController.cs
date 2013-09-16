using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController {

    public class DefaultMobController : AbstractNewtonEntityController {
        protected int walkDirection = -1; // +1 ||-1 || 0

        public const double MOB_ACC = 0.2;
        public const double MOB_SPEED = 1;

        public DefaultMobController(Mob e)
            : base(e) {
            //---
        }

        public override void Update(KeyboardDevice keyboard) {
            Vec2d delta = new Vec2d(0, 0);

            if (ent.IsOnGround()) {
                Vec2d blockPos;
                if (walkDirection < 0)
                    blockPos = new Vec2d((int)((ent.position.X + ent.width) / Block.BLOCK_WIDTH), (int)((ent.position.Y) / Block.BLOCK_HEIGHT));
                else
                    blockPos = new Vec2d((int)((ent.position.X) / Block.BLOCK_WIDTH), (int)((ent.position.Y) / Block.BLOCK_HEIGHT));

                int y = (int)blockPos.Y - 1;
                int x = (int)blockPos.X + walkDirection;

                Block next = ent.owner.GetBlock(x, y);
                if (next == null || !Entity.TestBlocking(next, ent)) {
                    walkDirection *= -1;
                }
            }

            if ((walkDirection > 0 && ent.IsCollidingRight()) || (walkDirection < 0 && ent.IsCollidingLeft())) {
                walkDirection *= -1;
            }

            if ((walkDirection < 0 && movementDelta.X > -MOB_SPEED) || (walkDirection > 0 && movementDelta.X < MOB_SPEED)) {
                delta.X = MOB_ACC * walkDirection;
            }

            DoGravitationalMovement(delta);
        }

        public override bool IsActive() {
            return true;
        }
    }
}