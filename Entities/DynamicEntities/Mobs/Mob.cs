namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public abstract class Mob : DynamicEntity
    {
        private const int MOB_EXPLOSIONFRAGMENTS_X = 4;
        private const int MOB_EXPLOSIONFRAGMENTS_Y = 4;
        private const double MOB_EXPLOSIONFRAGMENTS_FORCE = 16;

        public Mob()
            : base()
        {
            //--
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return true;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (collidingEntity.GetBottomLeft().Y >= GetTopRight().Y && collidingEntity is DynamicEntity && ((DynamicEntity)collidingEntity).GetMovement().Y < 0 && isBlockingMovement)
                OnHeadJump(collidingEntity);
            else if (isDirectCollision || isTouching)
                OnTouch(collidingEntity, isCollider, isBlockingMovement, isDirectCollision, isTouching);
        }

        public void Explode()
        {
            DoExplosionEffect(MOB_EXPLOSIONFRAGMENTS_X, MOB_EXPLOSIONFRAGMENTS_Y, MOB_EXPLOSIONFRAGMENTS_FORCE);
        }

        public abstract void OnHeadJump(Entity e);

        public abstract void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching);
    }
}