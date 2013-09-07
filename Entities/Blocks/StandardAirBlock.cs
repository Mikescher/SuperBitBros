using Entities.SuperBitBros;
using System.Drawing;

namespace SuperBitBros.OpenGL.Entities.Blocks {
    class StandardAirBlock : Block {
        static Color color = Color.FromArgb(255, 255, 255);

        public StandardAirBlock()
            : base() {
            texture = Textures.texture_air;
        }

        public static Color GetColor() {
            return color;
        }

        public override bool IsBlocking(Entity sender) {
            return false;
        }

        public override bool RenderBackgroundAir() {
            return false;
        }
    }
}
