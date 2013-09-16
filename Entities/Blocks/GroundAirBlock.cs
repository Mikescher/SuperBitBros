using System.Drawing;

namespace SuperBitBros.Entities.Blocks {

    public class GroundAirBlock : StandardAirBlock {
        public new static Color color = Color.FromArgb(192, 192, 192);

        public GroundAirBlock()
            : base() {
            texture = Textures.texture_ground_air;
        }

        public new static Color GetColor() {
            return color;
        }
    }
}