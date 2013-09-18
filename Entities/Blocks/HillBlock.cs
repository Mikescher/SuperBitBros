using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class HillBlock : Block
    {
        public static Color color = Color.FromArgb(255, 0, 255);

        public HillBlock()
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
            return EntityRenderType.BRT_HILL;
        }
    }
}