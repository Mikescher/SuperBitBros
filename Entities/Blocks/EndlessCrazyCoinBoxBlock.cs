using System.Drawing;

namespace SuperBitBros.Entities.Blocks {

    public class EndlessCrazyCoinBoxBlock : CrazyCoinBoxBlock {
        public static new Color color = Color.FromArgb(0, 64, 255);

        public EndlessCrazyCoinBoxBlock()
            : base() {
            isActive = true;
            timeUntilDried = int.MaxValue;
        }

        public static new Color GetColor() {
            return color;
        }
    }
}