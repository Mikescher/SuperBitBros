using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class ToadEntity : DynamicEntity
    {
        public ToadEntity()
            : base()
        {
            distance = Entity.DISTANCE_STRUCTURES;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_toad;

            AddController(new StaticEntityController(this));
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            //
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return true;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            //
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
