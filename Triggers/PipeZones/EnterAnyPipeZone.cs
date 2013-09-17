using System.Drawing;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers.PipeZones
{
    public class EnterAnyPipeZone : MoveAnyPipeZone
    {
        public EnterAnyPipeZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        public new static Color color = Color.FromArgb(128, 128, 64);

        public new static Color GetColor()
        {
            return color;
        }

        public override Color GetTriggerColor()
        {
            return GetColor();
        }

        public override bool CanEnter()
        {
            return true;
        }
    }
}