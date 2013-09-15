using Entities.SuperBitBros;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.Trigger {
    class PlayerSpawnZone : Trigger {
        public PlayerSpawnZone(Vec2i pos)
            : base(pos) {
            //--
        }

        public override void OnCollide(DynamicEntity collider) {
            //nothing
        }

        public Player SpawnPlayer() {
            Player p = new Player();
            owner.AddEntity(p, position.X * Block.BLOCK_WIDTH, position.Y * Block.BLOCK_HEIGHT);
            return p;
        }
    }
}
