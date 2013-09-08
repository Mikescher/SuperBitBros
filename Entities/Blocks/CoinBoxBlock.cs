using Entities.SuperBitBros;
using System;
using System.Drawing;

namespace SuperBitBros.OpenGL.Entities.Blocks {
    class CoinBoxBlock : Block {
        static Color color = Color.FromArgb(0, 0, 255);

        public CoinBoxBlock()
            : base() {
            texture = Textures.texture_coinblock_full;
        }

        public static Color GetColor() {
            return color;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision) {
            if (isBlockingMovement && collidingEntity.GetType() == typeof(Player) && collidingEntity.GetTopLeft().Y <= GetBottomRight().Y && ((Player)collidingEntity).movementDelta.Y > 0) {
                owner.AddEntity(new CoinEntity(), GetTopLeft().X, GetTopLeft().Y);
                ((GameWorld)owner).ReplaceBlock(this, new EmptyCoinBoxBlock());
            }
        }
    }
}
