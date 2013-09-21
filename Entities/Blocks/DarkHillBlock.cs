using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class DarkHillBlock : StandardHillBlock
    {
        public new static Color color = Color.FromArgb(255, 0, 128);

        public DarkHillBlock()
            : base()
        {
            texture = Textures.texture_darkHill;
        }

        public new static Color GetColor()
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
