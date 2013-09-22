using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class MushroomEntity : DynamicEntity
    {
        public MushroomEntity()
            : base()
        {
            distance = Entity.DISTANCE_POWERUPS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_mushroom;

            AddController(new MushroomController(this));
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (collidingEntity.GetType() == typeof(Player) && isDirectCollision)
            {
                (owner as GameWorld).player.growToBigPlayer();
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
