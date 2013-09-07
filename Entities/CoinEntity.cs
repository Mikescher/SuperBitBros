
using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Input;
using SuperBitBros.OpenGL.Entities.Blocks;
namespace SuperBitBros.OpenGL.Entities {
    class CoinEntity : AnimatedDynamicEntity {
        private const double COIN_SPAWN_FORCE = 3;

        public CoinEntity() {
            distance = Entity.DISTANCE_POWERUPS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            atexture.animation_speed = 15;

            atexture.Add(0, Textures.texture_coin_0);
            atexture.Add(0, Textures.texture_coin_1);
            atexture.Add(0, Textures.texture_coin_2);
            atexture.Add(0, Textures.texture_coin_3);
        }

        public override void OnAdd(GameModel owner) {
            base.OnAdd(owner);
            movementDelta.Y = COIN_SPAWN_FORCE;
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            updateGravitationalMovement(Vector2d.Zero);

            atexture.Update();
        }

        public override bool IsBlocking(Entity sender) {
            return false;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement) {
            if (collidingEntity.GetType() == typeof(Player) && collidingEntity.GetPosition().isColldingWith(GetPosition())) {
                owner.RemoveEntity(this);
            }
        }
    }
}
