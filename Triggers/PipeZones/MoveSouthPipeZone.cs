﻿using System.Drawing;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers.PipeZones
{
    public class MoveSouthPipeZone : PipeZone
    {
        public MoveSouthPipeZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        protected override PipeDirection GetDirection()
        {
            return PipeDirection.SOUTH;
        }

        public static Color color = Color.FromArgb(128, 255, 0);

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