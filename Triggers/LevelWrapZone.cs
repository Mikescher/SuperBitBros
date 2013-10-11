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
                deactivate();
                (owner as GameWorld).StartChangeWorld(target_world, target_level, (owner as GameWorld).player.power);
            }
        }

        public void deactivate()
        {
            LevelWrapZone[] z = new LevelWrapZone[9];

            z[0] = owner.getReliableTriggerList(position.X, position.Y).Find(x => x is LevelWrapZone) as LevelWrapZone;

            z[1] = owner.getReliableTriggerList(position.X + 1, position.Y).Find(x => x is LevelWrapZone) as LevelWrapZone;
            z[2] = owner.getReliableTriggerList(position.X, position.Y + 1).Find(x => x is LevelWrapZone) as LevelWrapZone;
            z[3] = owner.getReliableTriggerList(position.X - 1, position.Y).Find(x => x is LevelWrapZone) as LevelWrapZone;
            z[4] = owner.getReliableTriggerList(position.X, position.Y - 1).Find(x => x is LevelWrapZone) as LevelWrapZone;

            z[5] = owner.getReliableTriggerList(position.X + 1, position.Y + 1).Find(x => x is LevelWrapZone) as LevelWrapZone;
            z[6] = owner.getReliableTriggerList(position.X + 1, position.Y - 1).Find(x => x is LevelWrapZone) as LevelWrapZone;
            z[7] = owner.getReliableTriggerList(position.X - 1, position.Y + 1).Find(x => x is LevelWrapZone) as LevelWrapZone;
            z[8] = owner.getReliableTriggerList(position.X - 1, position.Y - 1).Find(x => x is LevelWrapZone) as LevelWrapZone;

            for (int i = 0; i < 9; i++)
            {
                if (z[i] != null && z[i].target_level == target_level && z[i].target_world == target_world)
                    z[i].active = false;
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
