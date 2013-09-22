using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class MushroomPlatformBlock : Block
    {
        public static Color color = Color.FromArgb(128, 192, 0);

        public MushroomPlatformBlock()
            : base()
        {
            texture = Textures.texture_mshroomplatform;
        }

        public static Color GetColor()
        {
            return color;
        }

        public override Color GetBlockColor()
        {
            return color;
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            if (sender is DynamicEntity)
                return sender.position.Y > (position.Y + height) || (sender.position.Y == (position.Y + height) && (sender as DynamicEntity).GetMovement().Y <= 0);
            else
                return sender.position.Y >= position.Y + height;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}