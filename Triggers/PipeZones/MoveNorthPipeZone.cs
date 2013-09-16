using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers.PipeZones
{
    class MoveNorthPipeZone : PipeZone
    {
        public MoveNorthPipeZone(Vec2i pos)
            : base(pos) {
            //--
        }

        public override PipeDirection GetDirection()
        {
            return PipeDirection.NORTH;
        }

        public static Color color = Color.FromArgb(128, 0, 255);
        public static Color GetColor()
        {
            return color;
        }

        public override Color GetTriggerColor()
        {
            return GetColor();
        }
    }
}
