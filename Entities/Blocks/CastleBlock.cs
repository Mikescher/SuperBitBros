using Entities.SuperBitBros;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks {
    class CastleBlock : Block {
        public static Color color = Color.FromArgb(255, 0, 0);

        public CastleBlock()
            : base() {
            texture = Textures.texture_castle;
        }

        protected override bool IsBlockingOther(Entity sender) {
            return false;
        }

        public static Color GetColor() {
            return color;
        }
    }
}
