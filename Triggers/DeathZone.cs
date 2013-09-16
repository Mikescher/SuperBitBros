using SuperBitBros.Entities;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Drawing;

namespace SuperBitBros.Triggers {
    class DeathZone : Trigger {

        public DeathZone(Vec2i pos)
            : base(pos) {
            //--
        }

        public override void OnCollide(DynamicEntity collider) {
            if (collider is Player) {
                Console.Out.WriteLine("DeathZone Triggered");
            } else if (collider is Mob) {
                collider.Kill();
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
