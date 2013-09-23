using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.HUD;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.OpenRasterFormat;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;
using System;
using System.Drawing;

namespace SuperBitBros
{
    public class GameWorld : GameModel
    {
        private const int LEVEL_END_ANIMATION_DURATION = 5 * 60;

        private BooleanKeySwitch debugMapExplosionSwitch = new BooleanKeySwitch(false, Key.F6, KeyTriggerMode.COOLDOWN_DOWN);

        public OffsetCalculator offset = new OffsetCalculator();

        public Player player;

        private int mapWorld;
        private int mapLevel;

        public GameWorld(int world, int level)
            : base()
        {
            mapWorld = world;
            mapLevel = level;
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

            if (Program.debugViewSwitch.Value && debugMapExplosionSwitch.Value)
                StartChangeWorld(0, 0);
        }

        public void SpawnEntityFromMapData(EntityTypeWrapper setype, double x, double y)
        {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            AddEntity(setype.Get(), px, py);
        }

        public void AddTriggerFromMapData(AddTriggerType triggertype, Color c, int x, int y)
        {
            double px = x * Block.BLOCK_WIDTH;
            double py = y * Block.BLOCK_HEIGHT;

            if (triggertype == AddTriggerType.PLAYER_SPAWN_POSITION)
            {
                PlayerSpawnZone zone = new PlayerSpawnZone(new Vec2i(x, y));
                AddTrigger(zone, x, y);

                player = zone.SpawnPlayer();
            }
            else if (triggertype == AddTriggerType.DEATH_ZONE)
            {
                AddTrigger(new DeathZone(new Vec2i(x, y)), x, y);
            }
            else if (triggertype == AddTriggerType.LEVEL_WRAP)
            {
                AddTrigger(new LevelWrapZone(new Vec2i(x, y), c.B), x, y);
            }
            else if (triggertype == AddTriggerType.BRIDGE_DESTROY)
            {
                AddTrigger(new BridgeDestroyZone(new Vec2i(x, y)), x, y);
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
            ImageMapParser parser = new ImageMapParser(new OpenRasterImage(ResourceAccessor.GetMap(mapWorld, mapLevel)));
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
                        AddTriggerFromMapData(att, parser.GetColor(ImageMapParser.LAYER_TRIGGER, imgX, imgY), x, y);

                    if (pzt == null)
                    { }  // No Zone
                    else if (!pzt.IsSet())
                        Console.Error.WriteLine("Could not parse PipeZone-Color in Map: {0} ({1}|{2})", parser.map.GetColor(ImageMapParser.LAYER_PIPEZONES, imgX, imgY), x, y);
                    else
                        AddPipeZoneFromMapData(pzt, x, y);
                }
            }

            offset.AddVisionBoxes(parser.GetVisionZones());

            //#############
            //AFTER MAP GEN
            //#############

            offset.Calculate(player.GetPosition(), viewPortWidth, viewPortHeight, mapRealWidth, mapRealHeight, false);
            foreach (DynamicEntity e in dynamicEntityList)
            {
                e.OnAfterMapGen();
            }
        }

        public override Vec2d GetOffset()
        {
            return offset.Value;
        }

        public void Explode()
        {
            Random r = new Random();

            Rect2i viewBox = (Rect2i)(new Rect2d(offset.Value, viewPortWidth, viewPortHeight) / Block.BLOCK_SIZE);

            viewBox.Trim(-2);

            for (int y = 0; y < viewBox.Height; y++)
            {
                for (int x = 0; x < viewBox.Width; x++)
                {
                    Block b = GetBlock(viewBox.bl.X + x, viewBox.bl.Y + y);

                    if (b != null)
                    {
                        AddDelayedAction(
                            (int)(r.NextDouble() * LEVEL_END_ANIMATION_DURATION),
                            (() => b.Explode(3, 3, 2.5 + r.NextDouble() * 2)));
                    }
                }
            }
        }

        public void StartChangeWorld(int target_world, int target_level)
        {
            player.MakeStatic();

            for (int i = 0; i < dynamicEntityList.Count; i++)
            {
                if (dynamicEntityList[i] != player)
                    dynamicEntityList[i].KillLater();
            }

            Explode();

            AddDelayedAction(
                (int)(LEVEL_END_ANIMATION_DURATION + 30),
                (() =>
                    {
                        player.Explode();
                        player.KillLater();
                    }
                ));

            AddDelayedAction(
                            (int)(LEVEL_END_ANIMATION_DURATION + 120),
                            (() => ChangeWorld(target_world, target_level)));
        }

        public void ChangeWorld(int world, int level)
        {
            Console.Out.WriteLine("Change World to {0}:{1}", world, level);

            ownerView.ChangeWorld(world, level);
        }

        private PlayerSpawnZone FindSpawnZone()
        {
            foreach (Trigger t in triggerList)
            {
                if (t is PlayerSpawnZone)
                    return t as PlayerSpawnZone;
            }
            return null;
        }

        public void OnPlayerDeath()
        {
            if ((HUD as StandardGameHUD).headCounter.Value > 0)
            {
                Console.Out.WriteLine("Resuscitate Player");

                (HUD as StandardGameHUD).headCounter.Value--;

                player.isAlive = false;
                player.Explode();
                player.KillLater();

                player = FindSpawnZone().SpawnPlayer();
            }
            else
            {
                Console.Out.WriteLine("Restart Game");

                (HUD as StandardGameHUD).Reset();
                StartChangeWorld(1, 1);
            }
        }
    }
}