using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Triggers
{
    public class BeanStalkSpawnZone : Trigger
    {
        private bool active = true;

        public BeanStalkSpawnZone(Vec2i pos)
            : base(pos)
        {
        }

        public void DoSpawn()
        {
            if (active)
            {
                active = false;

                owner.AddEntity(new BeanStalkEntity(), position.X * Block.BLOCK_WIDTH, position.Y * Block.BLOCK_HEIGHT);
            }
        }

        public override void OnCollide(DynamicEntity collider)
        {
            //
        }

        public static Color color = Color.FromArgb(128, 128, 255);

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