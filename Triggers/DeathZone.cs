using System;
using System.Drawing;
using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers
{
    public class DeathZone : Trigger
    {
        public DeathZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        public override void OnCollide(DynamicEntity collider)
        {
            if (collider is Player)
            {
                Console.Out.WriteLine("DeathZone Triggered");
            }
            else if (collider is DynamicEntity)
            {
                collider.KillLater();
            }
        }

        public static Color color = Color.FromArgb(128, 0, 64);

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