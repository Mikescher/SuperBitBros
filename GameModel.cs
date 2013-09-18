﻿using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.HUD;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Triggers;

namespace SuperBitBros
{
    public abstract class GameModel
    {
        public List<DynamicEntity> entityList { get; protected set; }
        public List<Block> blockList { get; protected set; }

        private Block[,] blockMap;
        private List<Trigger>[,] triggerMap;

        private List<DynamicEntity> killList;

        public int mapBlockWidth { get; protected set; }
        public int mapBlockHeight { get; protected set; }

        public double mapRealWidth { get; protected set; }
        public double mapRealHeight { get; protected set; }

        public int viewPortWidth = 1;
        public int viewPortHeight = 1;

        public HUDModel HUD = null;

        public GameModel()
        {
            mapBlockWidth = 0;
            mapBlockHeight = 0;

            mapRealWidth = 0;
            mapRealHeight = 0;

            entityList = new List<DynamicEntity>();
            blockList = new List<Block>();
            killList = new List<DynamicEntity>();
        }

        public virtual void Init()
        {
            // Nothing to see here, officer
        }

        protected void AddBlock(Block b, int x, int y)
        {
            b.position.X = Block.BLOCK_WIDTH * x;
            b.position.Y = Block.BLOCK_HEIGHT * y;
            blockList.Add(b);

            if (blockMap[x, y] != null)
                Console.Error.WriteLine("Block overrides other block when added to Blockmap: X:{0}, Y:{1}, Block:{2}", x, y, b.ToString());
            blockMap[x, y] = b;

            b.OnAdd(this, x, y);
        }

        public void ReplaceBlock(Block oldb, Block newb)
        {
            int x = oldb.blockPos.X;
            int y = oldb.blockPos.Y;

            RemoveBlock(x, y);

            AddBlock(newb, x, y);
        }

        protected void RemoveBlock(int x, int y)
        {
            if (blockMap[x, y] != null)
            {
                blockList.Remove(blockMap[x, y]);
                blockMap[x, y].OnRemove();
                blockMap[x, y] = null;
            }
        }

        public Block GetBlock(Vec2i v)
        {
            return GetBlock(v.X, v.Y);
        }

        public Block GetBlock(int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return null;
            return blockMap[x, y];
        }

        public Color4 GetBlockColor(int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return Color.White;
            return blockMap[x, y].GetBlockColor();
        }

        public virtual Entity AddEntity(DynamicEntity e, double x, double y)
        {
            e.position.X = x;
            e.position.Y = y;
            entityList.Add(e);
            e.OnAdd(this);
            return e;
        }

        private bool RemoveEntity(DynamicEntity e)
        {
            e.OnRemove();
            return entityList.Remove(e);
        }

        public virtual List<DynamicEntity> GetCurrentEntityList()
        {
            return new List<DynamicEntity>(entityList);
        }

        public virtual List<Block> GetCurrentBlockList()
        {
            return new List<Block>(blockList);
        }

        public virtual void Update(KeyboardDevice keyboard)
        {
            foreach (DynamicEntity e in GetCurrentEntityList())
            {
                e.Update(keyboard);
            }
            foreach (Block e in GetCurrentBlockList())
            {
                e.Update(keyboard);
            }

            foreach (DynamicEntity e in killList)
            {
                if (!RemoveEntity(e)) 
                    Console.Error.WriteLine("Could not KillLater Entity: " + e);
            }
            if (HUD != null)
            {
                HUD.Update(keyboard);
            }
            killList.Clear();
        }

        public void KillLater(DynamicEntity e)
        {
            if (!killList.Contains(e)) killList.Add(e);
        }

        public void setSize(int w, int h)
        {
            blockMap = new Block[w, h];
            triggerMap = new List<Trigger>[w, h];

            mapBlockWidth = w;
            mapBlockHeight = h;

            mapRealWidth = Block.BLOCK_WIDTH * w;
            mapRealHeight = Block.BLOCK_HEIGHT * h;
        }

        public List<Trigger> getTriggerList(int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return null;
            return triggerMap[x, y];
        }

        public void AddTrigger(Trigger t, int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return;

            if (triggerMap[x, y] == null)
                triggerMap[x, y] = new List<Trigger>();

            triggerMap[x, y].Add(t);

            t.OnAdd(this);
        }

        public abstract Vec2d GetOffset();
    }
}