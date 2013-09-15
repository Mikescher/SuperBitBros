using System.Drawing;

namespace SuperBitBros.Entities.Blocks {
    class StandardGroundBlock : Block {
        public static Color color = Color.FromArgb(0, 0, 0);

        public StandardGroundBlock()
            : base() {
            texture = Textures.texture_ground;
        }

        public static Color GetColor() {
            return color;
        }

        public override Color GetBlockColor() {
            return color;
        }
    }
}
