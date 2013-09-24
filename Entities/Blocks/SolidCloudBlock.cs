using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class SolidCloudBlock : Block
    {
        public static Color color = Color.FromArgb(128, 128, 255);

        public SolidCloudBlock()
            : base()
        {
            texture = Textures.texture_solidcloud;
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
