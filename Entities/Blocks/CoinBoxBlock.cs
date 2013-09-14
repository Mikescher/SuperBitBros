using Entities.SuperBitBros;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks {
    class CoinBoxBlock : Block {
        private const double COIN_SPAWN_FORCE = 3;

        public static Color color = Color.FromArgb(0, 0, 255);

        public CoinBoxBlock()
            : base() {
            texture = Textures.texture_coinblock_full;
        }

        public static Color GetColor() {
            return color;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching) {
            if (isBlockingMovement && collidingEntity.GetType() == typeof(Player) && collidingEntity.GetTopLeft().Y <= GetBottomRight().Y && ((Player)collidingEntity).movementDelta.Y > 0) {
                owner.AddEntity(new GravityCoinEntity(COIN_SPAWN_FORCE), GetTopLeft().X, GetTopLeft().Y);
                ((GameWorld)owner).ReplaceBlock(this, new EmptyCoinBoxBlock());
            }
        }
    }
}
