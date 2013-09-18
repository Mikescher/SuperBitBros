using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class StandardGroundBlock : Block
    {
        public static Color color = Color.FromArgb(0, 0, 0);

        public StandardGroundBlock()
            : base()
        {
            texture = Textures.texture_ground;
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
            return EntityRenderType.BRT_STANDARDGROUND;
        }
    }
}