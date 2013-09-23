using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class LavaBlock : Block
    {
        public static Color color = Color.FromArgb(128, 0, 0);

        public LavaBlock()
            : base()
        {
            texture = Textures.texture_lava;
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
