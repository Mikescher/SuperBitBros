using Entities.SuperBitBros;
using OpenTK;
using SuperBitBros.OpenGL.Entities;
using SuperBitBros.OpenGL.Entities.Blocks;
using SuperBitBros.OpenGL.Properties;
using System;

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
            } else if (trigger == ParseTriggerType.SPAWN_COIN) {
                AddEntity(new CoinEntity(), x, y);
            }
        }

        public void LoadMapFromResources() {
            ImageMapParser parser = new ImageMapParser(Resources.map_01_01);
            mapWidth = parser.GetWidth() * Block.BLOCK_WIDTH;
            mapHeight = parser.GetHeight() * Block.BLOCK_HEIGHT;

            for (int x = 0; x < parser.GetWidth(); x++) {
                for (int y = 0; y < parser.GetHeight(); y++) {
                    int px = x;
                    int py = parser.GetHeight() - (1 + y);

                    Block ent = parser.FindBlock(px, py);
                    ParseTriggerType trig = parser.FindSpawnTrigger(px, py);

                    if (ent != null) {
                        AddBlock(ent, x, y);
                    } else if (trig != ParseTriggerType.NO_TRIGGER) {
                        AddBlock(parser.GetMapAirBlock(), x, y);
                        CallParseTrigger(trig, x * Block.BLOCK_WIDTH, y * Block.BLOCK_HEIGHT);
                    } else
                        Console.Error.WriteLine("Could not parse Color in Map: {0}", parser.GetPositionColor(px, py));
                }
            }

            foreach (DynamicEntity e in entityList) {
                e.OnAfterMapGen();
            }
        }

        public override Vector2d GetOffset(int window_width, int window_height) {
            Vector2d screenMiddle = new Vector2d(offset.X + (window_width / 2.0), offset.Y + (window_height / 2.0));

            Vector2d playerPos = player.GetPosition().GetMiddle();

            Vector2d diffToMid = Vector2d.Subtract(screenMiddle, playerPos);
            Vector2d diffToOff = Vector2d.Subtract(player.GetBottomLeft(), offset);

            if (Math.Abs(diffToMid.X) > window_width / 8.0) {
                diffToMid.X -= (window_width / 8.0) * Math.Sign(diffToMid.X);

                offset.X -= diffToMid.X;
            }

            if (diffToMid.Y < -2 * window_height / 6.0) {
                offset.Y -= diffToMid.Y - (2 * window_height / 6.0) * Math.Sign(diffToMid.Y);
            }
            if (diffToOff.Y < 2* Block.BLOCK_HEIGHT) {
                offset.Y += diffToOff.Y - 2 * Block.BLOCK_HEIGHT;
            }

            offset.X = Math.Min(offset.X + window_width, mapWidth) - window_width;
            offset.Y = Math.Min(offset.Y + window_height, mapHeight) - window_height;

            offset.X = Math.Max(offset.X, 0);
            offset.Y = Math.Max(offset.Y, 0);

            return offset;
        }
    }
}
