using Entities.SuperBitBros;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.Trigger;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.OpenRasterFormat;
using SuperBitBros.Properties;
using System;

namespace SuperBitBros {
    class GameWorld : GameModel {
        private Vec2d offset = Vec2d.Zero;

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

        public void AddTriggerFromMapData(AddTriggerType triggertype, int x, int y) {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            if (triggertype == AddTriggerType.PLAYER_SPAWN_POSITION) {
                PlayerSpawnZone zone = new PlayerSpawnZone(new Vec2i(x, y));
                AddTrigger(zone, x, y);

                player = zone.SpawnPlayer();

                offset.Set(player.position.X, player.position.Y);
            } else if (triggertype == AddTriggerType.DEATH_ZONE) {
                AddTrigger(new DeathZone(new Vec2i(x, y)), x, y);
            }
        }

        public void AddPipeZoneFromMapData(PipeZoneType pipeZoneType, int x, int y) {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            if (pipeZoneType == PipeZoneType.MOVEMENT_NORTH_ZONE) {

            } else if (pipeZoneType == PipeZoneType.MOVEMENT_EAST_ZONE) {

            } else if (pipeZoneType == PipeZoneType.MOVEMENT_SOUTH_ZONE) {

            } else if (pipeZoneType == PipeZoneType.MOVEMENT_WEST_ZONE) {

            }
        }

        public void LoadMapFromResources() {
            ImageMapParser parser = new ImageMapParser(new OpenRasterImage(Resources.map_01_01));
            setSize(parser.GetWidth(), parser.GetHeight());

            for (int x = 0; x < parser.GetWidth(); x++) {
                for (int y = 0; y < parser.GetHeight(); y++) {
                    int imgX = x;
                    int imgY = parser.GetHeight() - (1 + y);

                    Block block = parser.GetBlock(imgX, imgY);
                    SpawnEntityType set = parser.GetEntity(imgX, imgY);
                    AddTriggerType att = parser.GetTrigger(imgX, imgY);
                    PipeZoneType pzt = parser.GetPipeZone(imgX, imgY);

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

                    if (pzt == PipeZoneType.UNKNOWN_ZONE)
                        Console.Error.WriteLine("Could not parse PipeZone-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_PIPEZONES, imgX, imgY), x, y);
                    else if (pzt != PipeZoneType.NO_ZONE)
                        AddPipeZoneFromMapData(pzt, x, y);
                }
            }

            foreach (DynamicEntity e in entityList) {
                e.OnAfterMapGen();
            }
        }

        public override Vec2d GetOffset(int window_width, int window_height) {
            Rect2d cameraBox = new Rect2d(offset, window_width, window_height);

            cameraBox.TrimNorth(window_height / 4.0);
            cameraBox.TrimEast(window_width / 3.0);
            cameraBox.TrimSouth(Block.BLOCK_HEIGHT * 2);
            cameraBox.TrimWest(window_width / 3.0);

            Vec2d playerPos = player.GetPosition().bl;

            offset += cameraBox.GetDistanceTo(playerPos);

            offset.X = Math.Max(offset.X, 0);
            offset.Y = Math.Max(offset.Y, 0);

            offset.X = Math.Min(offset.X + window_width, mapRealWidth) - window_width;
            offset.Y = Math.Min(offset.Y + window_height, mapRealHeight) - window_height;

            return offset;
        }
    }
}
