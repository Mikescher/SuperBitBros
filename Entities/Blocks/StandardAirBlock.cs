using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class StandardAirBlock : Block
    {
        public static Color color = Color.FromArgb(255, 255, 255);

        public StandardAirBlock()
            : base()
        {
            distance = Entity.DISTANCE_AIR;
            texture = Textures.texture_air;
        }

        public static Color GetColor()
        {
            return color;
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
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