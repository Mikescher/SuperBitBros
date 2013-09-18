using System;
using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.HUD;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.OpenRasterFormat;
using SuperBitBros.Properties;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;

namespace SuperBitBros
{
    public class GameWorld : GameModel
    {
        public OffsetCalculator offset = new OffsetCalculator();

        public Player player;

        public GameWorld()
            : base()
        {
            //
        }

        public override void Init()
        {
            base.Init();

            HUD = new StandardGameHUD(this);
            LoadMapFromResources();
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            offset.Calculate(player.GetPosition(), viewPortWidth, viewPortHeight, mapRealWidth, mapRealHeight);
        }

        public void SpawnEntityFromMapData(EntityTypeWrapper setype, double x, double y)
        {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            AddEntity(setype.Get(), px, py);
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

                offset.Change(player.position);
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
                    EntityTypeWrapper set = parser.GetEntity(imgX, imgY);
                    AddTriggerType att = parser.GetTrigger(imgX, imgY);
                    PipeZoneTypeWrapper pzt = parser.GetPipeZone(imgX, imgY);

                    if (block == null)
                        Console.Error.WriteLine("Could not parse Block-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_BLOCKS, imgX, imgY), x, y);
                    else
                        AddBlock(block, x, y);

                    if (set == null)
                    { }  // No Entity
                    else if (!set.IsSet())
                        Console.Error.WriteLine("Could not parse SpawnEntity-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_ENTITIES, imgX, imgY), x, y);
                    else
                        SpawnEntityFromMapData(set, x, y);

                    if (att == AddTriggerType.UNKNOWN_TRIGGER)
                        Console.Error.WriteLine("Could not parse Trigger-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_TRIGGER, imgX, imgY), x, y);
                    else if (att != AddTriggerType.NO_TRIGGER)
                        AddTriggerFromMapData(att, x, y);

                    if (pzt == null)
                    { }  // No Zone
                    else if (!pzt.IsSet())
                        Console.Error.WriteLine("Could not parse PipeZone-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_PIPEZONES, imgX, imgY), x, y);
                    else
                        AddPipeZoneFromMapData(pzt, x, y);
                }
            }

            offset.AddVisionBoxes(parser.GetVisionZones());

            foreach (DynamicEntity e in entityList)
            {
                e.OnAfterMapGen();
            }
        }

        public override Vec2d GetOffset()
        {
            return offset.Value;
        }
    }
}