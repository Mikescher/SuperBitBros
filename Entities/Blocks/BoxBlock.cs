using SuperBitBros.Entities.DynamicEntities;

namespace SuperBitBros.Entities.Blocks
{
    public abstract class BoxBlock : Block
    {
        public BoxBlock()
            : base()
        {
            texture = Textures.texture_coinblock_full;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (isBlockingMovement && collidingEntity.GetType() == typeof(Player) && collidingEntity.GetTopLeft().Y <= GetBottomRight().Y && ((Player)collidingEntity).GetMovement().Y > 0)
            {
                OnActivate(collidingEntity as Player);
            }
        }

        public abstract void OnActivate(Player p);

        protected void Deactivate()
        {
            ((GameWorld)owner).ReplaceBlock(this, new EmptyBoxBlock());
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
