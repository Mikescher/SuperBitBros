using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class LeverEntity : DynamicEntity
    {
        public LeverEntity()
            : base()
        {
            distance = Entity.DISTANCE_STRUCTURES;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_lever;

            AddController(new StaticEntityController(this));
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            Player p = collidingEntity as Player;
            if (p != null && isDirectCollision)
            {
                KillLater();
            }
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}