using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.MarioPower;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

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

        public Player SpawnPlayer(AbstractMarioPower pw = null)
        {
            Player p = new Player(pw);
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