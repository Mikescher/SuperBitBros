using Entities.SuperBitBros;
using OpenTK;
using SuperBitBros.OpenGL.Entities.Blocks;
using SuperBitBros.OpenGL.Properties;
using System;
using System.Drawing;

namespace SuperBitBros.OpenGL {
    class GameWorld : GameModel {
        private const int MAP_WIDTH_MAX = 400;
        private const int MAP_HEIGHT_MAX = 150;

        private Vector2d offset = new Vector2d(0, 0);

        private Block[,] BlockMap = new Block[MAP_WIDTH_MAX, MAP_HEIGHT_MAX];

        private Player player;

        public GameWorld()
            : base() {

        }

        public void AddBlock(Block b, int x, int y) {
            AddEntity(b, Block.BLOCK_WIDTH * x, Block.BLOCK_HEIGHT * y);

            if (BlockMap[x, y] != null)
                Console.Error.WriteLine("Block overrides other block when added to Blockmap: X:{0}, Y:{1}, Block:{2}", x, y, b.ToString());
            BlockMap[x, y] = b;

            b.OnBlockAdd(this, x, y);
        }

        public void ReplaceBlock(Block oldb, Block newb) {
            int x = (int)(oldb.GetBottomLeft().X / Block.BLOCK_WIDTH);
            int y = (int)(oldb.GetBottomLeft().Y / Block.BLOCK_HEIGHT);

            if (BlockMap[x, y] != null)
                RemoveEntity(BlockMap[x, y]);

            AddEntity(newb, Block.BLOCK_WIDTH * x, Block.BLOCK_HEIGHT * y);

            BlockMap[x, y] = newb;
            newb.OnBlockAdd(this, x, y);
        }

        public Block GetBlock(int x, int y) {
            if (x < 0 || y < 0 || x > MAP_WIDTH_MAX || y > MAP_HEIGHT_MAX)
                return null;
            return BlockMap[x, y];
        }

        public void CallParseTrigger(ParseTriggerType trigger, double x, double y) {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            if (trigger == ParseTriggerType.SPAWN_PLAYER) {
                Player p = new Player();
                AddEntity(p, x, y);
                player = p;
            }
        }

        public void LoadMapFromResources() {
            Bitmap bmp = Resources.map_01_01;

            for (int x = 0; x < bmp.Width; x++) {
                for (int y = 0; y < bmp.Height; y++) {
                    Color pixel = bmp.GetPixel(x, bmp.Height - (1 + y));

                    Block ent = MapParser.findBlock(pixel);
                    ParseTriggerType trig = MapParser.findSpawnTrigger(pixel);

                    if (ent != null) {
                        AddBlock(ent, x, y);
                    } else if (trig != ParseTriggerType.NO_TRIGGER) {
                        AddBlock(new StandardAirBlock(), x, y);
                        CallParseTrigger(trig, x * Block.BLOCK_WIDTH, y * Block.BLOCK_HEIGHT);
                    } else
                        Console.Error.WriteLine("Could not parse Color in Map: {0}", pixel);
                }
            }
        }

        public override Vector2d GetOffset(int window_width, int window_height) {
            Vector2d screenMiddle = new Vector2d(offset.X + (window_width / 2.0), offset.Y + (window_height / 2.0));

            Vector2d playerPos = player.GetPosition().GetMiddle();

            Vector2d diff = Vector2d.Subtract(screenMiddle, playerPos);

            if (Math.Abs(diff.X) > window_width / 8.0) {
                diff.X -= (window_width / 8.0) * Math.Sign(diff.X);

                offset.X -= diff.X;
            }

            if (diff.Y < -2 * window_height / 6.0) {
                diff.Y -= (2 * window_height / 6.0) * Math.Sign(diff.Y);

                offset.Y -= diff.Y;
            } else if (diff.Y > 0) {
                offset.Y -= diff.Y;
            }

            offset.X = Math.Max(offset.X, 0);
            offset.Y = Math.Max(offset.Y, 0);

            return offset;
        }
    }
}
