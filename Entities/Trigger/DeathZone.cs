using Entities.SuperBitBros;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.Trigger {
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
    }
}
