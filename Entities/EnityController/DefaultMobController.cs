using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
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

        protected virtual void ChangeDirection()
        {
            walkDirection *= -1;
        }

        protected virtual bool IsOnMaxSpeed(double mspeed)
        {
            return !((walkDirection < 0 && movementDelta.X > -MOB_SPEED) || (walkDirection > 0 && movementDelta.X < MOB_SPEED));
        }

        protected virtual Vec2d GetDirectionVector()
        {
            return new Vec2d(walkDirection, 0);
        }

        protected virtual bool IsCollidingInWalkingDirection()
        {
            return (walkDirection > 0 && ent.IsCollidingRight()) || (walkDirection < 0 && ent.IsCollidingLeft());
        }

        protected virtual bool CanWalkWithoutFalling()
        {
            Vec2i blockPos;
            if (walkDirection < 0)
                blockPos = (Vec2i)(new Vec2d(ent.position.X + ent.width, ent.position.Y) / Block.BLOCK_SIZE);
            else
                blockPos = (Vec2i)(ent.position / Block.BLOCK_SIZE);

            int y = (int)blockPos.Y - 1;
            int x = (int)blockPos.X + walkDirection;

            Block next = ent.owner.GetBlock(x, y);
            return !(next == null || !Entity.TestBlocking(next, ent));
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (IsCollidingInWalkingDirection())
                ChangeDirection();

            if (ent.IsOnGround() && !CanWalkWithoutFalling())
                ChangeDirection();

            Vec2d delta = GetDirectionVector() * MOB_ACC;

            if (IsOnMaxSpeed(MOB_SPEED))
                delta *= 0;

            if (!ent.IsOnGround() || CanWalkWithoutFalling()) // Walk normal
            {
                DoGravitationalMovement(delta);
            }
            else // Emergency break
            {
                delta = new Vec2d(-movementDelta.X, 0);
                delta.DoMaxLength(MOB_ACC);

                DoGravitationalMovement(delta);
            }
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}