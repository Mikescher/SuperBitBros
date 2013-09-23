using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class CastleGroundBlock : StandardGroundBlock
    {
        public new static Color color = Color.FromArgb(64, 64, 128);

        public CastleGroundBlock()
            : base()
        {
            texture = Textures.texture_castleGround;
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
