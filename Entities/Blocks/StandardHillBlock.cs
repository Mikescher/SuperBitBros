using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class StandardHillBlock : Block
    {
        public static Color color = Color.FromArgb(255, 0, 255);

        public StandardHillBlock()
            : base()
        {
            texture = Textures.texture_hill;
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