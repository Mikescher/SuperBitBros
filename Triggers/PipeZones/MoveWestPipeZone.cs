using System.Drawing;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers.PipeZones
{
    public class MoveWestPipeZone : PipeZone
    {
        public MoveWestPipeZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        protected override PipeDirection GetDirection()
        {
            return PipeDirection.WEST;
        }

        public static Color color = Color.FromArgb(0, 255, 255);

        public static Color GetColor()
        {
            return color;
        }

        public override Color GetTriggerColor()
        {
            return GetColor();
        }

        public override bool CanEnter()
        {
            return false;
        }
    }
}