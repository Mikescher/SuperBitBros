using SuperBitBros.Entities.DynamicEntities;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class StandardCeilingBlock : Block
    {
        public static Color color = Color.FromArgb(255, 128, 0);

        public StandardCeilingBlock()
            : base()
        {
            texture = Textures.texture_ceiling;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (isBlockingMovement && collidingEntity.GetType() == typeof(Player) && (collidingEntity as Player).IsBig() && collidingEntity.GetTopLeft().Y <= GetBottomRight().Y && ((Player)collidingEntity).GetMovement().Y > 0)
            {
                (collidingEntity as Player).StopYMovement();
                DestroyExplode();
                ((GameWorld)owner).ReplaceBlock(this, GetReplacementAir());
            }
        }

        public virtual StandardAirBlock GetReplacementAir()
        {
            return new StandardAirBlock();
        }

        public static Color GetColor()
        {
            return color;
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
