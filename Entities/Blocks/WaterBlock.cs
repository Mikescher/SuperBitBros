using SuperBitBros.Entities.DynamicEntities;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class WaterBlock : StandardAirBlock
    {
        public new static Color color = Color.FromArgb(128, 128, 192);

        public WaterBlock()
            : base()
        {
            texture = Textures.texture_underwater_water;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            Player p = collidingEntity as Player;
            if (p != null)
                p.AddWaterController();
        }

        public new static Color GetColor()
        {
            return color;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}