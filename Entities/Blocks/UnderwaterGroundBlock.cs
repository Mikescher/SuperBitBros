using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class UnderwaterGroundBlock : StandardGroundBlock
    {
        public new static Color color = Color.FromArgb(0, 192, 0);

        public UnderwaterGroundBlock()
            : base()
        {
            texture = Textures.texture_underwater_ground;
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
