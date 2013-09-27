using OpenTK.Graphics;
using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Particles;
using SuperBitBros.HUD;
using SuperBitBros.MarioPower;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Triggers;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SuperBitBros
{
    public abstract class GameModel
    {
        private List<DynamicEntity>[,] collisionMap;
        public EntityCache entityCache;

        public List<DynamicEntity> dynamicEntityList { get; protected set; }
        public List<Block> blockList { get; protected set; }
        public List<Trigger> triggerList { get; protected set; }

        private Block[,] blockMap;
        private List<Trigger>[,] triggerMap;

        private List<DynamicEntity> killList;

        public List<DelayedAction> delayedActionList;

        public int mapBlockWidth { get; protected set; }
        public int mapBlockHeight { get; protected set; }

        public double mapRealWidth { get; protected set; }
        public double mapRealHeight { get; protected set; }

        public int viewPortWidth = OpenGLView.INIT_RESOLUTION_WIDTH;
        public int viewPortHeight = OpenGLView.INIT_RESOLUTION_HEIGHT;

        public OpenGLView ownerView;

        public HUDModel HUD = null;

        public GameModel()
        {
            mapBlockWidth = 0;
            mapBlockHeight = 0;

            mapRealWidth = 0;
            mapRealHeight = 0;

            Particle.GlobalParticleCount = 0;

            dynamicEntityList = new List<DynamicEntity>();
            blockList = new List<Block>();
            killList = new List<DynamicEntity>();
            triggerList = new List<Trigger>();
            delayedActionList = new List<DelayedAction>();
        }

        public virtual void Init(AbstractMarioPower p)
        {
            entityCache = new EntityCache();
        }

        protected void AddBlock(Block b, int x, int y)
        {
            b.position.X = Block.BLOCK_WIDTH * x;
            b.position.Y = Block.BLOCK_HEIGHT * y;
            blockList.Add(b);

            if (blockMap[x, y] != null)
                Console.Error.WriteLine("Block overrides other block when added to Blockmap: X:{0}, Y:{1}, Block:{2}", x, y, b.ToString());
            blockMap[x, y] = b;

            entityCache.AddEntity(b);

            b.OnAdd(this, x, y);
        }

        public void ReplaceBlock(Block oldb, Block newb)
        {
            int x = oldb.blockPos.X;
            int y = oldb.blockPos.Y;

            RemoveBlock(x, y);

            if (newb != null)
                AddBlock(newb, x, y);
        }

        protected void RemoveBlock(Vec2i pos)
        {
            RemoveBlock(pos.X, pos.Y);
        }

        protected void RemoveBlock(int x, int y)
        {
            if (blockMap[x, y] != null)
            {
                blockList.Remove(blockMap[x, y]);
                entityCache.RemoveEntity(blockMap[x, y]);

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

        public virtual Entity TestAddEntity(DynamicEntity e, double x, double y)
        {
            e.position.X = x;
            e.position.Y = y;
            e.owner = this;

            if (e.DoCollisions(true))
                return null;
            else
                return AddEntity(e, x, y);
        }

        public virtual Entity AddEntity(DynamicEntity e, double x, double y)
        {
            e.position.X = x;
            e.position.Y = y;
            e.OnAdd(this);

            dynamicEntityList.Add(e);
            entityCache.AddEntity(e);
            if (e.IsInCollisionMap())
                AddEntityToCollisionMap(e);

            return e;
        }

        private bool RemoveEntity(DynamicEntity e)
        {
            e.OnRemove();
            entityCache.RemoveEntity(e);
            if (e.IsInCollisionMap())
                RemoveEntityFromCollisionMap(e);

            return dynamicEntityList.Remove(e);
        }

        public virtual List<DynamicEntity> GetCurrentEntityList()
        {
            return new List<DynamicEntity>(dynamicEntityList);
        }

        public virtual List<DynamicEntity> GetSurroundingEntityList(Vec2i cachePos)
        {
            List<DynamicEntity> r = new List<DynamicEntity>();

            for (int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {
                    r.AddRange(GetCollisionMap(cachePos.X + x, cachePos.Y + y));
                }
            }

            return r;
        }

        public virtual List<Block> GetCurrentBlockList()
        {
            return new List<Block>(blockList);
        }

        public virtual void Update(KeyboardDevice keyboard)
        {
            for (int i = delayedActionList.Count - 1; i >= 0; i--)
            {
                if (delayedActionList[i].DecInvoke())
                    delayedActionList.RemoveAt(i);
            }

            BooleanKeySwitch.UpdateAll(keyboard);

            foreach (DynamicEntity e in GetCurrentEntityList())
            {
                e.Update(keyboard);
                e.UpdateCollisionMapPosition();
            }

            foreach (Block e in GetCurrentBlockList())
            {
                e.Update(keyboard);
            }

            foreach (Trigger t in triggerList)
            {
                t.Update(keyboard);
            }

            foreach (DynamicEntity e in killList)
            {
                if (!RemoveEntity(e))
                    Console.Error.WriteLine("Could not KillLater Entity: " + e);
            }
            killList.Clear();

            if (HUD != null)
            {
                HUD.Update(keyboard);
            }
        }

        public void KillLater(DynamicEntity e)
        {
            if (!killList.Contains(e))
                killList.Add(e);
        }

        public void setSize(int w, int h)
        {
            blockMap = new Block[w, h];
            collisionMap = new List<DynamicEntity>[w, h];
            triggerMap = new List<Trigger>[w, h];

            mapBlockWidth = w;
            mapBlockHeight = h;

            mapRealWidth = Block.BLOCK_WIDTH * w;
            mapRealHeight = Block.BLOCK_HEIGHT * h;
        }

        public List<DynamicEntity> GetCollisionList(int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return new List<DynamicEntity>();

            if (collisionMap[x, y] == null)
                collisionMap[x, y] = new List<DynamicEntity>();

            return collisionMap[x, y];
        }

        public List<DynamicEntity> GetCollisionMap(int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return new List<DynamicEntity>();

            if (collisionMap[x, y] == null)
                collisionMap[x, y] = new List<DynamicEntity>();

            return collisionMap[x, y];
        }

        public void AddEntityToCollisionMap(DynamicEntity e)
        {
            AddEntityToCollisionMap(e, e.GetCollisionMapPosition());
        }

        public void AddEntityToCollisionMap(DynamicEntity e, Vec2i pos)
        {
            AddEntityToCollisionMap(e, pos.X, pos.Y);
        }

        public void AddEntityToCollisionMap(DynamicEntity e, int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return;

            if (collisionMap[x, y] == null)
                collisionMap[x, y] = new List<DynamicEntity>();

            collisionMap[x, y].Add(e);
        }

        public void RemoveEntityFromCollisionMap(DynamicEntity e)
        {
            RemoveEntityFromCollisionMap(e, e.GetCollisionMapPosition());
        }

        public void RemoveEntityFromCollisionMap(DynamicEntity e, Vec2i pos)
        {
            RemoveEntityFromCollisionMap(e, pos.X, pos.Y);
        }

        public void RemoveEntityFromCollisionMap(DynamicEntity e, int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return;

            if (collisionMap[x, y] == null)
                collisionMap[x, y] = new List<DynamicEntity>();

            if (!collisionMap[x, y].Remove(e))
                Console.Out.WriteLine("Could not rem Entity {0} from CollisionMap {1}:{2}", e.GetType().Name, x, y);
        }

        public void MoveEntityInCollisionMap(DynamicEntity e, Vec2i p, Vec2i n)
        {
            MoveEntityInCollisionMap(e, p.X, p.Y, n.X, n.Y);
        }

        public void MoveEntityInCollisionMap(DynamicEntity e, int prevx, int prevy, int newx, int newy)
        {
            if (prevx == newx && prevy == newy)
                return;

            RemoveEntityFromCollisionMap(e, prevx, prevy);
            AddEntityToCollisionMap(e, newx, newy);
        }

        public List<Trigger> getTriggerList(int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return null;
            return triggerMap[x, y];
        }

        public List<Trigger> getReliableTriggerList(int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return new List<Trigger>();
            if (triggerMap[x, y] == null)
                return new List<Trigger>();
            return triggerMap[x, y];
        }

        public void AddTrigger(Trigger t, int x, int y)
        {
            if (x < 0 || y < 0 || x >= mapBlockWidth || y >= mapBlockHeight)
                return;

            if (triggerMap[x, y] == null)
                triggerMap[x, y] = new List<Trigger>();

            triggerMap[x, y].Add(t);
            triggerList.Add(t);

            t.OnAdd(this);
        }

        public void AddDelayedAction(int delay, Action a)
        {
            delayedActionList.Add(new DelayedAction(delay, a));
        }

        public abstract Vec2d GetOffset();
    }
}