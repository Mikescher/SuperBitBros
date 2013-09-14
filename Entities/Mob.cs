using Entities.SuperBitBros;
using OpenTK;
using SuperBitBros.OpenGL.Entities.Blocks;

namespace SuperBitBros.OpenGL.Entities {
    abstract class Mob : DynamicEntity {
        protected int walkDirection = -1; // +1 ||-1 || 0

        public Mob()
            : base() {
            //constructor
        }

        protected void DoWalk(double speedacc, double speedmax) {
            Vector2d delta = new Vector2d(0, 0);

            if (IsOnGround()) {
                Vector2d blockPos;
                if (walkDirection < 0)
                    blockPos = new Vector2d((int)((position.X + width) / Block.BLOCK_WIDTH), (int)((position.Y) / Block.BLOCK_HEIGHT));
                else
                    blockPos = new Vector2d((int)((position.X) / Block.BLOCK_WIDTH), (int)((position.Y) / Block.BLOCK_HEIGHT));

                int y = (int)blockPos.Y - 1;
                int x = (int)blockPos.X + walkDirection;

                Block next = owner.GetBlock(x, y);
                if (next == null || !Entity.TestBlocking(next, this)) {
                    walkDirection *= -1;
                }
            }

            if ((walkDirection > 0 && IsCollidingRight()) || (walkDirection < 0 && IsCollidingLeft())) {
                walkDirection *= -1;
            }

            if ((walkDirection < 0 && movementDelta.X > -speedmax) || (walkDirection > 0 && movementDelta.X < speedmax)) {
                delta.X = speedacc * walkDirection;
            }

            DoGravitationalMovement(delta);
        }

        protected override bool IsBlockingOther(Entity sender) {
            return true;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching) {
            if (collidingEntity.GetBottomLeft().Y >= GetTopRight().Y && collidingEntity is DynamicEntity && ((DynamicEntity)collidingEntity).movementDelta.Y < 0 && isBlockingMovement)
                OnHeadJump(collidingEntity);
            else if (isDirectCollision || isTouching)
                OnTouch(collidingEntity, isCollider, isBlockingMovement, isDirectCollision, isTouching);
        }

        public abstract void OnHeadJump(Entity e);
        public abstract void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching);

    }
}
