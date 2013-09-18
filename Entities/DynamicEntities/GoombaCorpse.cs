using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class GoombaCorpse : DynamicEntity
    {
        public const double GOOMBA_CORPSE_FRICION = 0.05;

        public GoombaCorpse(Goomba g)
        {
            distance = Entity.DISTANCE_CORPSE;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_goomba_dead;

            DefaultNewtonController controller = new DefaultNewtonController(this, GOOMBA_CORPSE_FRICION);
            controller.movementDelta = g.GetMovement();
            AddController(controller);
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_MISC;
        }
    }
}