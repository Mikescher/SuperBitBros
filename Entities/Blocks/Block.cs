using Entities.SuperBitBros;
using OpenTK;

namespace SuperBitBros.OpenGL.Entities.Blocks {
    abstract class Block : Entity {
        public const int BLOCK_WIDTH = 16;
        public const int BLOCK_HEIGHT = 24;

        protected GameModel owner;
        public int blockPosX;
        public int blockPosY;

        public Rectangle2d position2dCache = null;
        public Rectangle3d position3dCache = null;

        public Block()
            : base() {
            distance = Entity.DISTANCE_BLOCKS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;
        }

        public virtual void OnAdd(GameModel mod, int bx, int by) {
            owner = mod;
            blockPosX = bx;
            blockPosY = by;
        }

        public override Rectangle2d GetPosition() {
            if (position2dCache == null) {
                position2dCache = new Rectangle2d(position, width, height);
            }
            return position2dCache;
        }

        public override Rectangle3d GetPositionWithDistance() {
            if (position3dCache == null) {
                position3dCache = new Rectangle3d(new Vector3d(position.X, position.Y, distance), width, height);
            }
            return position3dCache;
        }

        public Block getTopBlock() {
            return owner.GetBlock(blockPosX, blockPosY);
        }

        public Block getLeftBlock() {
            return owner.GetBlock(blockPosX - 1, blockPosY);
        }

        public Block getRightBlock() {
            return owner.GetBlock(blockPosX + 1, blockPosY);
        }

        public Block getBottomBlock() {
            return owner.GetBlock(blockPosX, blockPosY - 1);
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return true;
        }

        public virtual bool RenderBackgroundAir() { return false; }
    }
}
