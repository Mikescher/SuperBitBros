using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Triggers.PipeZones {

    public class MoveEastWestPipeZone : PipeZone {

        public MoveEastWestPipeZone(Vec2i pos)
            : base(pos) {
            //--
        }

        protected override PipeDirection GetDirection() {
            return PipeDirection.EASTWEST;
        }

        public static Color color = Color.FromArgb(255, 64, 0);

        public static Color GetColor() {
            return color;
        }

        public override Color GetTriggerColor() {
            return GetColor();
        }

        public override bool CanEnter() {
            return false;
        }
    }
}