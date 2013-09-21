using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Triggers
{
    public class LevelWrapZone : Trigger
    {
        private const int ANY = 0;

        private const int MASK_WORLD = 0xF0;
        private const int MASK_LEVEL = 0x0F;

        private int target_world;
        private int target_level;

        private bool active = true;

        public LevelWrapZone(Vec2i pos, int ident)
            : base(pos)
        {
            target_world = (ident & MASK_WORLD) >> 4;
            target_level = (ident & MASK_LEVEL);
        }

        public override void OnCollide(DynamicEntity collider)
        {
            Player p = collider as Player;
            if (p != null && active)
            {
                active = false;
                (owner as GameWorld).StartChangeWorld(target_world, target_level);
            }
        }

        public static Color color = Color.FromArgb(0, 200, ANY);

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
