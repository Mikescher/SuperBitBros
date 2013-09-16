namespace SuperBitBros.Entities.DynamicEntities {

    public abstract class Mob : DynamicEntity {

        public Mob()
            : base() {
            //--
        }

        protected override bool IsBlockingOther(Entity sender) {
            return true;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching) {
            if (collidingEntity.GetBottomLeft().Y >= GetTopRight().Y && collidingEntity is DynamicEntity && ((DynamicEntity)collidingEntity).GetMovement().Y < 0 && isBlockingMovement)
                OnHeadJump(collidingEntity);
            else if (isDirectCollision || isTouching)
                OnTouch(collidingEntity, isCollider, isBlockingMovement, isDirectCollision, isTouching);
        }

        public abstract void OnHeadJump(Entity e);

        public abstract void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching);
    }
}