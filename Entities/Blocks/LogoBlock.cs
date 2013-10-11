using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class LogoBlock : Block
    {
        public static Color color = Color.FromArgb(255, 0, 0);

        public LogoBlock(int x, int y)
            : base()
        {
            texture = Textures.texture_logo[x, y];
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
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