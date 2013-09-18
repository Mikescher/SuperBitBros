﻿using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public abstract class Block : Entity
    {
        public const int BLOCK_WIDTH = 16;
        public const int BLOCK_HEIGHT = 24;
        public static readonly Vec2d BLOCK_SIZE = new Vec2d(BLOCK_WIDTH, BLOCK_HEIGHT);

        public Vec2i blockPos = Vec2i.Zero;

        public Rect2d position2dCache = null;

        public Block()
            : base()
        {
            distance = Entity.DISTANCE_BLOCKS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;
        }

        public virtual void OnAdd(GameModel mod, int bx, int by)
        {
            owner = mod;
            blockPos.Set(bx, by);
        }

        public override Rect2d GetPosition()
        {
            if (position2dCache == null)
            {
                position2dCache = new Rect2d(position, width, height);
            }
            return position2dCache;
        }

        public Block getTopBlock()
        {
            return owner.GetBlock(blockPos.X, blockPos.Y);
        }

        public Block getLeftBlock()
        {
            return owner.GetBlock(blockPos.X - 1, blockPos.Y);
        }

        public Block getRightBlock()
        {
            return owner.GetBlock(blockPos.X + 1, blockPos.Y);
        }

        public Block getBottomBlock()
        {
            return owner.GetBlock(blockPos.X, blockPos.Y - 1);
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return true;
        }

        public abstract Color GetBlockColor();

        public virtual bool RenderBackgroundAir()
        {
            return false;
        }
    }
}