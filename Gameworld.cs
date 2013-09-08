using Entities.SuperBitBros;
using OpenTK;
using SuperBitBros.OpenGL.Entities;
using SuperBitBros.OpenGL.Entities.Blocks;
using SuperBitBros.OpenGL.Properties;
using System;
using System.Drawing;

namespace SuperBitBros.OpenGL {
    class GameWorld : GameModel {

        private Vector2d offset = new Vector2d(0, 0);

        public Player player;

        public GameWorld()
            : base() {

        }

        public void CallParseTrigger(ParseTriggerType trigger, double x, double y) {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            if (trigger == ParseTriggerType.SPAWN_PLAYER) {
                Player p = new Player();
                AddEntity(p, x, y);
                player = p;
            } else if (trigger == ParseTriggerType.SPAWN_GOOMBA) {
                AddEntity(new Goomba(), x, y);
            } else if (trigger == ParseTriggerType.SPAWN_PIRANHAPLANT) {
                AddEntity(new PiranhaPlant(), x, y);
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

            foreach (DynamicEntity e in entityList) {
                e.OnAfterMapGen();
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
