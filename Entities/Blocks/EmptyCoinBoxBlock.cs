using System.Drawing;

namespace SuperBitBros.Entities.Blocks {
    class EmptyCoinBoxBlock : Block {
        public static Color color = Color.FromArgb(255, 255, 0);

        public EmptyCoinBoxBlock()
            : base() {
            texture = Textures.texture_coinblock_empty;
        }

        public static Color GetColor() {
            return color;
        }

        public override Color GetBlockColor() {
            return color;
        }
    }
}
