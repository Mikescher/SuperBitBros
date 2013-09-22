using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class EmptyBoxBlock : Block
    {
        public static Color color = Color.FromArgb(255, 255, 0);

        public EmptyBoxBlock()
            : base()
        {
            texture = Textures.texture_coinblock_empty;
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