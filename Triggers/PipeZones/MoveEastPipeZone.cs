using System.Drawing;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers.PipeZones
{
    public class MoveEastPipeZone : PipeZone
    {
        public MoveEastPipeZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        protected override PipeDirection GetDirection()
        {
            return PipeDirection.EAST;
        }

        public static Color color = Color.FromArgb(255, 0, 0);

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