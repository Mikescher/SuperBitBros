using System;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.OpenRasterFormat;
using SuperBitBros.Properties;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;

namespace SuperBitBros
{
    public class GameWorld : GameModel
    {
        private Vec2d offset = Vec2d.Zero;

        public Player player;

        public GameWorld()
            : base()
        {
        }

        public void SpawnEntityFromMapData(SpawnEntityType setype, double x, double y)
        {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            if (setype == SpawnEntityType.SPAWN_GOOMBA)
            {
                AddEntity(new Goomba(), px, py);
            }
            else if (setype == SpawnEntityType.SPAWN_PIRANHAPLANT)
            {
                AddEntity(new PiranhaPlant(), px, py);
            }
            else if (setype == SpawnEntityType.SPAWN_COIN)
            {
                AddEntity(new CoinEntity(), px, py);
            }
        }

        public void AddTriggerFromMapData(AddTriggerType triggertype, int x, int y)
        {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            if (triggertype == AddTriggerType.PLAYER_SPAWN_POSITION)
            {
                PlayerSpawnZone zone = new PlayerSpawnZone(new Vec2i(x, y));
                AddTrigger(zone, x, y);

                player = zone.SpawnPlayer();

                offset.Set(player.position.X, player.position.Y);
            }
            else if (triggertype == AddTriggerType.DEATH_ZONE)
            {
                AddTrigger(new DeathZone(new Vec2i(x, y)), x, y);
            }
        }

        public void AddPipeZoneFromMapData(PipeZoneTypeWrapper pipeZoneType, int x, int y)
        {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            AddTrigger(pipeZoneType.Get(new Vec2i(x, y)), x, y);
        }

        public void LoadMapFromResources()
        {
            ImageMapParser parser = new ImageMapParser(new OpenRasterImage(Resources.map_01_01));
            setSize(parser.GetWidth(), parser.GetHeight());

            for (int x = 0; x < parser.GetWidth(); x++)
            {
                for (int y = 0; y < parser.GetHeight(); y++)
                {
                    int imgX = x;
                    int imgY = parser.GetHeight() - (1 + y);

                    Block block = parser.GetBlock(imgX, imgY);
                    SpawnEntityType set = parser.GetEntity(imgX, imgY);
                    AddTriggerType att = parser.GetTrigger(imgX, imgY);
                    PipeZoneTypeWrapper pzt = parser.GetPipeZone(imgX, imgY);

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

                    if (pzt == null)
                    {
                        // No Zone
                    }
                    else if (!pzt.IsSet())
                        Console.Error.WriteLine("Could not parse PipeZone-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_PIPEZONES, imgX, imgY), x, y);
                    else
                        AddPipeZoneFromMapData(pzt, x, y);
                }
            }

            foreach (DynamicEntity e in entityList)
            {
                e.OnAfterMapGen();
            }
        }

        public Rect2d GetOffsetBox(int window_width, int window_height)
        {
            Rect2d result = new Rect2d(offset, window_width, window_height);

            result.TrimNorth(window_height / 4.0);
            result.TrimEast(window_width / 3.0);
            result.TrimSouth(Block.BLOCK_HEIGHT * 2);
            result.TrimWest(window_width / 3.0);

            return result;
        }

        public override Vec2d GetOffset(int window_width, int window_height)
        {
            Rect2d cameraBox = GetOffsetBox(window_width, window_height);

            Vec2d playerPos = player.GetPosition().bl;
            playerPos.X += player.width / 2.0;

            offset += cameraBox.GetDistanceTo(playerPos);

            offset.X = Math.Max(offset.X, 0);
            offset.Y = Math.Max(offset.Y, 0);

            offset.X = Math.Min(offset.X + window_width, mapRealWidth) - window_width;
            offset.Y = Math.Min(offset.Y + window_height, mapRealHeight) - window_height;

            return offset;
        }
    }
}