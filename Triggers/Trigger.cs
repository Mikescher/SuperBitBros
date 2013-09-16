using System.Drawing;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers {
    abstract class Trigger {
        protected readonly Vec2i position; // block position

        protected GameModel owner;

        public Trigger(Vec2i pos) {
            position = pos;
        }

        public Rect2d GetPosition() {
            return new Rect2d(position * Block.BLOCK_SIZE, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT);
        }

        public virtual void OnAdd(GameModel model) {
            owner = model;
        }

        public abstract void OnCollide(DynamicEntity collider);
        public abstract Color GetTriggerColor();
    }
}
