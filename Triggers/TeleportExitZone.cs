using SuperBitBros.Entities;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Drawing;

namespace SuperBitBros.Triggers
{
    public class TeleportExitZone : Trigger
    {
        private const int ANY = 0;

        private int id;

        public TeleportExitZone(Vec2i pos, int ident)
            : base(pos)
        {
            id = ident;
        }

        public override void OnCollide(DynamicEntity collider)
        {
            //
        }

        public Vec2i GetTeleportPosition()
        {
            Vec2i pos = new Vec2i(position);

            foreach (Trigger t in owner.triggerList)
            {
                TeleportExitZone tel = t as TeleportExitZone;
                if (tel != null && tel.GetTeleportID() == id)
                {
                    pos.X = Math.Min(pos.X, tel.position.X);
                    pos.Y = Math.Min(pos.Y, tel.position.Y);
                }
            }

            return pos;
        }

        public int GetTeleportID()
        {
            return id;
        }

        public static Color color = Color.FromArgb(ANY, 100, 200);

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
