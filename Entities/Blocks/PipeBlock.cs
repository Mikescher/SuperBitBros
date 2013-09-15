using System.Drawing;

namespace SuperBitBros.Entities.Blocks {
    class PipeBlock : Block {
        public static Color color = Color.FromArgb(0, 255, 0);

        public PipeBlock()
            : base() {
            texture = Textures.texture_pipe;
        }

        public static Color GetColor() {
            return color;
        }

        public override Color GetBlockColor() {
            return color;
        }
    }
}
