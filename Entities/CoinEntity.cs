
using Entities.SuperBitBros;
using OpenTK.Input;
using SuperBitBros.OpenGL.Entities.Blocks;
namespace SuperBitBros.OpenGL.Entities {
    class CoinEntity : AnimatedDynamicEntity {

        public CoinEntity()
            : base() {
            distance = Entity.DISTANCE_POWERUPS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            atexture.animation_speed = 15;

            atexture.Add(0, Textures.texture_coin_0);
            atexture.Add(0, Textures.texture_coin_1);
            atexture.Add(0, Textures.texture_coin_2);
            atexture.Add(0, Textures.texture_coin_3);
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            atexture.Update();
        }

        protected override bool IsBlockingOther(Entity sender) {
            return false;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching) {
            if (collidingEntity.GetType() == typeof(Player) && isDirectCollision) {
                owner.RemoveEntity(this);
            }
        }
    }
}
