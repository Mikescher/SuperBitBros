using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class BridgeEntity : DynamicEntity
    {
        private const int EXPLOSION_FRAGMENTS = 4;
        private const double EXPLOSION_FORCE = 20;

        public BridgeEntity()
            : base()
        {
            distance = Entity.DISTANCE_STRUCTURES;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_bridge;

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

        protected override void OnKill()
        {
            DoExplosionEffect(EXPLOSION_FRAGMENTS, EXPLOSION_FRAGMENTS, EXPLOSION_FORCE);
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
