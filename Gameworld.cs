using Entities.SuperBitBros;
using OpenTK;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenRasterFormat;
using SuperBitBros.Properties;
using System;

namespace SuperBitBros {
    class GameWorld : GameModel {

        private Vector2d offset = new Vector2d(0, 0);

        public Player player;

        public GameWorld()
            : base() {

        }

        public void SpawnEntityFromMapData(SpawnEntityType setype, double x, double y) {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            if (setype == SpawnEntityType.SPAWN_GOOMBA) {
                AddEntity(new Goomba(), px, py);
            } else if (setype == SpawnEntityType.SPAWN_PIRANHAPLANT) {
                AddEntity(new PiranhaPlant(), px, py);
            } else if (setype == SpawnEntityType.SPAWN_COIN) {
                AddEntity(new CoinEntity(), px, py);
            }
        }

        public void AddTriggerFromMapData(AddTriggerType triggertype, double x, double y) {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            if (triggertype == AddTriggerType.PLAYER_SPAWN_POSITION) {
                Player p = new Player();
                AddEntity(p, px, py);
                player = p;
            } else if (triggertype == AddTriggerType.DEATH_ZONE) {
                //TODO DEATH
            }
        }

        public void LoadMapFromResources() {
            ImageMapParser parser = new ImageMapParser(new OpenRasterImage(Resources.map_01_01));
            mapWidth = parser.GetWidth() * Block.BLOCK_WIDTH;
            mapHeight = parser.GetHeight() * Block.BLOCK_HEIGHT;

            for (int x = 0; x < parser.GetWidth(); x++) {
                for (int y = 0; y < parser.GetHeight(); y++) {
                    int imgX = x;
                    int imgY = parser.GetHeight() - (1 + y);

                    Block block = parser.GetBlock(imgX, imgY);
                    SpawnEntityType set = parser.GetEntity(imgX, imgY);
                    AddTriggerType att = parser.GetTrigger(imgX, imgY);

                    if (block == null)
                        Console.Error.WriteLine("Could not parse Block-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_BLOCKS, imgX, imgY), x, y);
                    else
                        AddBlock(block, x, y);

                    if (set == SpawnEntityType.UNKNOWN_SPAWN)
                        Console.Error.WriteLine("Could not parse SpawnEntity-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_ENTITIES, imgX, imgY), x, y);
                    else if (set != SpawnEntityType.NO_SPAWN)
                        SpawnEntityFromMapData(set, x, y);

                    if (att == AddTriggerType.UNKNOWN_TRIGGER)
                        Console.Error.WriteLine("Could not parse Trigger-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_TRIGGER, imgX, imgY), x, y);
                    else if (att != AddTriggerType.NO_TRIGGER)
                        AddTriggerFromMapData(att, x, y);
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
            if (diffToOff.Y < 2 * Block.BLOCK_HEIGHT) {
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
