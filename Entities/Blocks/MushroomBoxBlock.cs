using SuperBitBros.Entities.DynamicEntities;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class MushroomBoxBlock : Block
    {
        private const double COIN_SPAWN_FORCE = 3;

        public static Color color = Color.FromArgb(0, 128, 128);

        public MushroomBoxBlock()
            : base()
        {
            texture = Textures.texture_coinblock_full;
        }

        public static Color GetColor()
        {
            return color;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (isBlockingMovement && collidingEntity.GetType() == typeof(Player) && collidingEntity.GetTopLeft().Y <= GetBottomRight().Y && ((Player)collidingEntity).GetMovement().Y > 0)
            {
                owner.AddEntity(new MushroomEntity(), GetTopLeft().X, GetTopLeft().Y);
                ((GameWorld)owner).ReplaceBlock(this, new EmptyBoxBlock());
            }
        }

        public override Color GetBlockColor()
        {
            return color;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}