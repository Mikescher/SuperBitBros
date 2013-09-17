using System.Drawing;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers
{
    public class PlayerSpawnZone : Trigger
    {
        public PlayerSpawnZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        public override void OnCollide(DynamicEntity collider)
        {
            //nothing
        }

        public Player SpawnPlayer()
        {
            Player p = new Player();
            owner.AddEntity(p, position.X * Block.BLOCK_WIDTH, position.Y * Block.BLOCK_HEIGHT);
            return p;
        }

        public static Color color = Color.FromArgb(128, 128, 128);

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