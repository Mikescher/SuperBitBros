using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class StandardPillarBlock : Block
    {
        public static Color color = Color.FromArgb(192, 64, 0);

        public StandardPillarBlock()
            : base()
        {
            texture = Textures.texture_pillar;
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
