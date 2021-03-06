﻿using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class PiranhaPlant : Mob
    {
        private const double SHRINK_WIDTH = 0.1;
        private const int UPDATE_SPEED = 5;
        private const int FAST_DOWN_SPEED = 1;
        private const int CYCLUS_SPEED = 60;
        private const int STATE_COUNT = 13;

        private int lastUpdate = 0;
        private bool direction = true;
        public int state { get; private set; }

        private Rect2d pipeUnder = null;

        public PiranhaPlant()
            : base()
        {
            state = 0;

            distance = Entity.DISTANCE_MOBS;
            width = 2 * Block.BLOCK_WIDTH;
            height = 0;

            texture = Textures.doubleblock_sheet.GetTextureWrapper(0, 3);

            AddController(new StaticEntityController(this));
        }

        public override void OnAdd(GameModel owner)
        {
            base.OnAdd(owner);
            position.X += SHRINK_WIDTH;
            width -= SHRINK_WIDTH * 2;
        }

        protected override void OnKill()
        {
            base.OnKill();

            Explode();
        }

        public override Rect2d GetTexturePosition()
        {
            return new Rect2d(position.X - SHRINK_WIDTH, position.Y, 2 * Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT * 2);
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (state == 0 && (owner as GameWorld).player.IsInPipe())
            {
                lastUpdate = 0;
            }

            if (((state == 0 || state == STATE_COUNT - 1) && lastUpdate > CYCLUS_SPEED) || (!(state == 0 || state == STATE_COUNT - 1) && lastUpdate > UPDATE_SPEED) || ((owner as GameWorld).player.IsInPipe() && lastUpdate > FAST_DOWN_SPEED))
            {
                state = state + ((direction) ? (1) : (-1));
                if (state == 0)
                    direction = true;
                if (state == STATE_COUNT - 1)
                    direction = false;

                texture = Textures.doubleblock_sheet.GetTextureWrapper(state, 3);
                height = (Block.BLOCK_HEIGHT * 2.0) * (state * 1.0 / (STATE_COUNT - 1));
                lastUpdate = 0;
            }
            lastUpdate++;

            if (state == 0)
                testPlayerBlocking();
        }

        public override void OnAfterMapGen()
        {
            base.OnAfterMapGen();
            calcUnderlyingPipe();
        }

        private void calcUnderlyingPipe()
        {
            int minX = (int)(position.X / Block.BLOCK_WIDTH);
            int maxX = minX;

            int minY = (int)((position.Y - 0.5) / Block.BLOCK_HEIGHT);
            int maxY = minY;

            Block blockUnder = owner.GetBlock(minX, maxY);
            if (blockUnder == null)
            {
                pipeUnder = GetPosition();
                return;
            }
            Type typeUnder = blockUnder.GetType();

            //for (; ; )
            //{ // minX
            //    Block b = owner.GetBlock(minX - 1, maxY);
            //    if (b != null && b.GetType() == typeUnder)
            //        minX--;
            //    else
            //        break;
            //}

            for (; ; )
            { // maxX
                Block b = owner.GetBlock(maxX + 1, maxY);
                if (b != null && b.GetType() == typeUnder)
                    maxX++;
                else
                    break;
            }

            int width = maxX - minX + 1;
            width = Math.Min(width, 2);
            maxX = width + minX - 1;

            for (; ; )
            { // minY
                Block b = owner.GetBlock(maxX, minY - 1);
                if (b != null && b.GetType() == typeUnder)
                    minY--;
                else
                    break;
            }

            for (; ; )
            { // maxY
                Block b = owner.GetBlock(maxX, maxY + 1);
                if (b != null && b.GetType() == typeUnder)
                    maxY++;
                else
                    break;
            }

            
            int height = maxY - minY + 1;

            

            pipeUnder = new Rect2d(minX * Block.BLOCK_WIDTH, minY * Block.BLOCK_HEIGHT, width * Block.BLOCK_WIDTH, height * Block.BLOCK_HEIGHT);
        }

        private void testPlayerBlocking()
        {
            Player p = ((GameWorld)owner).player;
            Rect2d prect = p.GetPosition();

            if (prect.IsTouching(pipeUnder))
            {
                lastUpdate = 0;
            }
        }

        public override void OnHeadJump(Entity e)
        {
            Player p = e as Player;
            if (state != 0 && p != null)
                p.DoDeath(this);
        }

        public override void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            Player p = e as Player;
            if (state != 0 && p != null)
                p.DoDeath(this);
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_PLANT;
        }
    }
}