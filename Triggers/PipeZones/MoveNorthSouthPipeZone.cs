using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Triggers.PipeZones {

    public class MoveNorthSouthPipeZone : PipeZone {

        public MoveNorthSouthPipeZone(Vec2i pos)
            : base(pos) {
            //--
        }

        protected override PipeDirection GetDirection() {
            return PipeDirection.NORTHSOUTH;
        }

        public static Color color = Color.FromArgb(64, 0, 128);

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