using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class DarkGroundBlock : StandardGroundBlock
    {
        public new static Color color = Color.FromArgb(64, 64, 64);

        public DarkGroundBlock()
            : base()
        {
            texture = Textures.texture_darkGround;
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
