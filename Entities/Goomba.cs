using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Input;
using SuperBitBros.OpenGL.Entities.Blocks;

namespace SuperBitBros.OpenGL.Entities {
    class Goomba : DynamicEntity {

        public Goomba() {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_goomba;
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            updateGravitationalMovement(Vector2d.Zero);
        }

        public override bool IsBlocking(Entity sender) {
            return false;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision) {
            if (collidingEntity.GetType() == typeof(Player) && isDirectCollision) {
                owner.RemoveEntity(this);
                owner.AddEntity(new GoombaCorpse(), position.X, position.Y);
            }
        }
    }
}
