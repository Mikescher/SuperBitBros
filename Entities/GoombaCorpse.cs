
using SuperBitBros.Entities;
using OpenTK;
using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Entities.EnityController;
namespace SuperBitBros.Entities {
    class GoombaCorpse : DynamicEntity {

        public const double GOOMBA_CORPSE_FRICION = 0.05;

        public GoombaCorpse(Goomba g) {
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
    }
}
