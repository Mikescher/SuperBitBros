using System.Drawing;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers.PipeZones
{
    public class MoveAnyPipeZone : PipeZone
    {
        public MoveAnyPipeZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        protected override PipeDirection GetDirection()
        {
            return PipeDirection.ANY;
        }

        public static Color color = Color.FromArgb(255, 255, 128);

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