using SuperBitBros.Entities.DynamicEntities;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class DarkCeilingBlock : StandardCeilingBlock
    {
        public new static Color color = Color.FromArgb(64, 128, 64);

        public DarkCeilingBlock()
            : base()
        {
            texture = Textures.texture_darkCeiling;
        }

        public override StandardAirBlock GetReplacementAir()
        {
            return new DarkAirBlock();
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
