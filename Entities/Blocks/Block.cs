using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.SuperBitBros;

namespace SuperBitBros.OpenGL.Entities.Blocks
{
    abstract class Block : StaticEntity
    {
        public const int BLOCK_WIDTH = 16;
        public const int BLOCK_HEIGHT = 24;

        private GameWorld world_owner;
        private int blockPosX;
        private int blockPosY;

        public Block()
            : base()
        {
            distance = Entity.DISTANCE_BLOCKS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;
        }

        public void OnBlockAdd(GameWorld world, int x, int y)
        {
            world_owner = world;
            blockPosX = x;
            blockPosY = y;
        }

        public Block getTopBlock()
        {
            return world_owner.GetBlock(blockPosX, blockPosY);
        }

        public Block getLeftBlock()
        {
            return world_owner.GetBlock(blockPosX - 1, blockPosY);
        }

        public Block getRightBlock()
        {
            return world_owner.GetBlock(blockPosX + 1, blockPosY);
        }

        public Block getBottomBlock()
        {
            return world_owner.GetBlock(blockPosX, blockPosY - 1);
        }

        public override bool IsBlocking(Entity sender)
        {
            return true;
        }
    }
}
