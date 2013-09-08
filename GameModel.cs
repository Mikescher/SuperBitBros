using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Input;
using SuperBitBros.OpenGL.Entities.Blocks;
using System;
using System.Collections.Generic;

namespace SuperBitBros.OpenGL {
    abstract class GameModel {
        private const int MAP_WIDTH_MAX = 400;
        private const int MAP_HEIGHT_MAX = 150;

        public List<DynamicEntity> entityList { get; protected set; }
        public List<Block> blockList { get; protected set; }
        private Block[,] BlockMap = new Block[MAP_WIDTH_MAX, MAP_HEIGHT_MAX];

        public GameModel() {
            entityList = new List<DynamicEntity>();
            blockList = new List<Block>();
        }

        public void AddBlock(Block b, int x, int y) {
            b.position.X = Block.BLOCK_WIDTH * x;
            b.position.Y = Block.BLOCK_HEIGHT * y;
            blockList.Add(b);

            if (BlockMap[x, y] != null)
                Console.Error.WriteLine("Block overrides other block when added to Blockmap: X:{0}, Y:{1}, Block:{2}", x, y, b.ToString());
            BlockMap[x, y] = b;

            b.OnAdd(this, x, y);
        }

        public void ReplaceBlock(Block oldb, Block newb) {
            int x = oldb.blockPosX;
            int y = oldb.blockPosY;

            RemoveBlock(x, y);

            AddBlock(newb, x, y);
        }

        public void RemoveBlock(int x, int y) {
            if (BlockMap[x, y] != null) {
                blockList.Remove(BlockMap[x, y]);
                BlockMap[x, y].OnRemove();
                BlockMap[x, y] = null;
            }
        }

        public Block GetBlock(int x, int y) {
            if (x < 0 || y < 0 || x >= MAP_WIDTH_MAX || y >= MAP_HEIGHT_MAX)
                return null;
            return BlockMap[x, y];
        }

        public virtual void AddEntity(DynamicEntity e, double x, double y) {
            e.position.X = x;
            e.position.Y = y;
            entityList.Add(e);
            e.OnAdd(this);
        }

        public virtual bool RemoveEntity(DynamicEntity e) {
            e.OnRemove();
            return entityList.Remove(e);
        }

        public virtual List<Entity> GetCurrentEntityList() {
            return new List<Entity>(entityList);
        }

        public virtual void Update(KeyboardDevice keyboard) {
            foreach (Entity e in GetCurrentEntityList()) {
                e.Update(keyboard);
            }
        }

        public abstract Vector2d GetOffset(int window_width, int window_height);
    }
}
