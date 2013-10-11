using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Triggers
{
    public class SpawnLogoZone : Trigger
    {

        public SpawnLogoZone(Vec2i pos)
            : base(pos)
        {

        }

        public override void OnCollide(DynamicEntity collider)
        {
            //
        }

        public override void OnAfterMapGen()
        {
            for (int lx = 0; lx < 15; lx++)
            {
                for (int ly = 0; ly < 5; ly++)
                {
                    owner.ReplaceBlock(owner.GetBlock(position.X + lx, position.Y + ly), new LogoBlock(lx, ly));
                }
            }
        }

        public static Color color = Color.FromArgb(0, 128, 64);

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
