using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class FlowerEntity : DynamicEntity
    {
        public FlowerEntity()
            : base()
        {
            distance = Entity.DISTANCE_POWERUPS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_flower;

            AddController(new DefaultPowerupController(this));
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (collidingEntity.GetType() == typeof(Player) && isDirectCollision)
            {
                (owner as GameWorld).player.GrowToShootingPlayer();
                KillLater();
            }
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
