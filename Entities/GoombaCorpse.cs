
using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Input;
using SuperBitBros.OpenGL.Entities.Blocks;
namespace SuperBitBros.OpenGL.Entities {
    class GoombaCorpse : DynamicEntity {
        public GoombaCorpse() {
            distance = Entity.DISTANCE_CORPSE;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_goomba_dead;
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            updateGravitationalMovement(Vector2d.Zero);
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }
    }
}
