using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.MarioPower;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Triggers
{
    public class GrowPlayerZone : Trigger
    {
        public GrowPlayerZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        public override void OnCollide(DynamicEntity collider)
        {
            Player p = collider as Player;
            if (p != null && p.power is StandardMarioPower)
            {
                p.GrowToBigPlayer();
            }
        }

        public static Color color = Color.FromArgb(255, 255, 0);

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