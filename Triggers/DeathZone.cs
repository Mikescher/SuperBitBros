using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

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
            Player p = collider as Player;
            if (p != null)
            {
                p.DoDeath(this);
            }
            else if (collider is DynamicEntity && !(collider as DynamicEntity).IsKillZoneImmune())
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