using Entities.SuperBitBros;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks {
    class StandardAirBlock : Block {
        public static Color color = Color.FromArgb(255, 255, 255);

        public StandardAirBlock()
            : base() {
            texture = Textures.texture_air;
        }

        public static Color GetColor() {
            return color;
        }

        protected override bool IsBlockingOther(Entity sender) {
            return false;
        }

        public override bool RenderBackgroundAir() {
            return false;
        }

        public override Color GetBlockColor() {
            return color;
        }
    }
}
