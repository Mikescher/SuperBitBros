using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.OpenRasterFormat;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SuperBitBros
{
    public enum SpawnEntityType { NO_SPAWN, UNKNOWN_SPAWN, SPAWN_GOOMBA, SPAWN_PIRANHAPLANT, SPAWN_COIN, SPAWN };

    public enum AddTriggerType { NO_TRIGGER, UNKNOWN_TRIGGER, DEATH_ZONE, PLAYER_SPAWN_POSITION, LEVEL_WRAP, BRIDGE_DESTROY, BEANSTALK_SPAWN, TELEPORT_ENTRY, TELEPORT_EXIT };

    public class ImageMapParser
    {
        public const string LAYER_PLAYERVISION = "PlayerVision";
        public const string LAYER_PIPEZONES = "PipeNetwork";
        public const string LAYER_TRIGGER = "Trigger";
        public const string LAYER_ENTITIES = "Entities";
        public const string LAYER_BLOCKS = "Blocks";

        //##################
        // ENTITIES
        //##################

        private static readonly Color COL_SPAWN_GOOMBA = Color.FromArgb(127, 0, 0);
        private static readonly Color COL_SPAWN_PIRANHAPLANT = Color.FromArgb(0, 127, 0);
        private static readonly Color COL_SPAWN_COIN = Color.FromArgb(100, 200, 100);
        private static readonly Color COL_SPAWN_FLAG = Color.FromArgb(0, 255, 255);
        private static readonly Color COL_SPAWN_KOOPA = Color.FromArgb(0, 192, 128);
        private static readonly Color COL_SPAWN_PARATROOPA = Color.FromArgb(0, 0, 255);
        private static readonly Color COL_SPAWN_BRIDGE = Color.FromArgb(255, 255, 0);
        private static readonly Color COL_SPAWN_LEVER = Color.FromArgb(0, 255, 128);
        private static readonly Color COL_SPAWN_TOAD = Color.FromArgb(255, 0, 255);
        private static readonly Color COL_SPAWN_BOWSER = Color.FromArgb(255, 128, 0);
        private static readonly Color COL_SPAWN_BEANSTALK = Color.FromArgb(255, 128, 255);
        private static readonly Color COL_SPAWN_CHEEPCHEEP = Color.FromArgb(255, 0, 0);
        private static readonly Color COL_SPAWN_BLOOPER = Color.FromArgb(128, 0, 128);
        private static readonly Color COL_SPAWN_AIRCHEEPCHEEP = Color.FromArgb(192, 0, 0);
        private static readonly Color COL_SPAWN_LAVABALL = Color.FromArgb(64, 0, 128);
        private static readonly Color COL_SPAWN_TRAMPOLINE = Color.FromArgb(128, 128, 0);
        private static readonly Color COL_SPAWN_JUMPING_PARATROOPA = Color.FromArgb(0, 64, 255);
        private static readonly Color COL_SPAWN_HAMMERBRO = Color.FromArgb(64, 0, 0);
        private static readonly Color COL_SPAWN_LAKITU = Color.FromArgb(0, 128, 192);

        public readonly OpenRasterImage map;

        public ImageMapParser(OpenRasterImage map)
        {
            this.map = map;
        }

        public int GetWidth()
        {
            return map.Width;
        }

        public int GetHeight()
        {
            return map.Height;
        }

        public Block GetBlock(int x, int y)
        {
            return FindBlock(map.GetColor(LAYER_BLOCKS, x, y));
        }

        public EntityTypeWrapper GetEntity(int x, int y)
        {
            return FindSpawnEntityType(map.GetColor(LAYER_ENTITIES, x, y));
        }

        public AddTriggerType GetTrigger(int x, int y)
        {
            return FindTriggerType(map.GetColor(LAYER_TRIGGER, x, y));
        }

        public Color GetColor(string layer, int x, int y)
        {
            return map.GetColor(layer, x, y);
        }

        public PipeZoneTypeWrapper GetPipeZone(int x, int y)
        {
            return FindPipeZoneType(map.GetColor(LAYER_PIPEZONES, x, y));
        }

        private Block FindBlock(Color c)
        {
            if (c == StandardGroundBlock.GetColor()) { return new StandardGroundBlock(); }
            else if (c == DarkGroundBlock.GetColor()) { return new DarkGroundBlock(); }
            else if (c == StandardAirBlock.GetColor()) { return new StandardAirBlock(); }
            else if (c == DarkAirBlock.GetColor()) { return new DarkAirBlock(); }
            else if (c == CoinBoxBlock.GetColor()) { return new CoinBoxBlock(); }
            else if (c == PowerUpgradeBoxBlock.GetColor()) { return new PowerUpgradeBoxBlock(); }
            else if (c == EmptyBoxBlock.GetColor()) { return new EmptyBoxBlock(); }
            else if (c == StandardHillBlock.GetColor()) { return new StandardHillBlock(); }
            else if (c == DarkHillBlock.GetColor()) { return new DarkHillBlock(); }
            else if (c == PipeBlock.GetColor()) { return new PipeBlock(); }
            else if (c == CastleBlock.GetColor()) { return new CastleBlock(); }
            else if (c == CrazyCoinBoxBlock.GetColor()) { return new CrazyCoinBoxBlock(); }
            else if (c == EndlessCrazyCoinBoxBlock.GetColor()) { return new EndlessCrazyCoinBoxBlock(); }
            else if (c == StandardCeilingBlock.GetColor()) { return new StandardCeilingBlock(); }
            else if (c == DarkCeilingBlock.GetColor()) { return new DarkCeilingBlock(); }
            else if (c == StandardPillarBlock.GetColor()) { return new StandardPillarBlock(); }
            else if (c == MushroomPlatformBlock.GetColor()) { return new MushroomPlatformBlock(); }
            else if (c == CastleGroundBlock.GetColor()) { return new CastleGroundBlock(); }
            else if (c == LavaBlock.GetColor()) { return new LavaBlock(); }
            else if (c == FireBoxBlock.GetColor()) { return new FireBoxBlock(); }
            else if (c == SolidCloudBlock.GetColor()) { return new SolidCloudBlock(); }
            else if (c == BeanStalkBoxBlock.GetColor()) { return new BeanStalkBoxBlock(); }
            else if (c == UnderwaterGroundBlock.GetColor()) { return new UnderwaterGroundBlock(); }
            else if (c == WaterBlock.GetColor()) { return new WaterBlock(); }
            else { return null; }
        }

        private EntityTypeWrapper FindSpawnEntityType(Color c)
        {
            if (c.A != 255) { return null; }
            else if (c == COL_SPAWN_GOOMBA) { return new EntityTypeWrapper(typeof(Goomba)); }
            else if (c == COL_SPAWN_PIRANHAPLANT) { return new EntityTypeWrapper(typeof(PiranhaPlant)); }
            else if (c == COL_SPAWN_COIN) { return new EntityTypeWrapper(typeof(PersistentCoinEntity)); }
            else if (c == COL_SPAWN_FLAG) { return new EntityTypeWrapper(typeof(FlagEntity)); }
            else if (c == COL_SPAWN_KOOPA) { return new EntityTypeWrapper(typeof(Koopa)); }
            else if (c == COL_SPAWN_PARATROOPA) { return new EntityTypeWrapper(typeof(Paratroopa)); }
            else if (c == COL_SPAWN_BRIDGE) { return new EntityTypeWrapper(typeof(BridgeEntity)); }
            else if (c == COL_SPAWN_LEVER) { return new EntityTypeWrapper(typeof(LeverEntity)); }
            else if (c == COL_SPAWN_TOAD) { return new EntityTypeWrapper(typeof(ToadEntity)); }
            else if (c == COL_SPAWN_BOWSER) { return new EntityTypeWrapper(typeof(Bowser)); }
            else if (c == COL_SPAWN_BEANSTALK) { return new EntityTypeWrapper(typeof(BeanStalkEntity)); }
            else if (c == COL_SPAWN_CHEEPCHEEP) { return new EntityTypeWrapper(typeof(CheepCheep)); }
            else if (c == COL_SPAWN_BLOOPER) { return new EntityTypeWrapper(typeof(Blooper)); }
            else if (c == COL_SPAWN_AIRCHEEPCHEEP) { return new EntityTypeWrapper(typeof(AirCheepCheep)); }
            else if (c == COL_SPAWN_LAVABALL) { return new EntityTypeWrapper(typeof(LavaBallEntity)); }
            else if (c == COL_SPAWN_TRAMPOLINE) { return new EntityTypeWrapper(typeof(TrampolineEntity)); }
            else if (c == COL_SPAWN_JUMPING_PARATROOPA) { return new EntityTypeWrapper(typeof(JumpingParatroopa)); }
            else if (c == COL_SPAWN_HAMMERBRO) { return new EntityTypeWrapper(typeof(Hammerbro)); }
            else if (c == COL_SPAWN_LAKITU) { return new EntityTypeWrapper(typeof(Lakitu)); }
            else { return new EntityTypeWrapper(null); }
        }

        private AddTriggerType FindTriggerType(Color c)
        {
            if (c.A != 255) { return AddTriggerType.NO_TRIGGER; }
            else if (c == PlayerSpawnZone.GetColor()) { return AddTriggerType.PLAYER_SPAWN_POSITION; }
            else if (c == DeathZone.GetColor()) { return AddTriggerType.DEATH_ZONE; }
            else if (c.R == LevelWrapZone.GetColor().R && c.G == LevelWrapZone.GetColor().G) { return AddTriggerType.LEVEL_WRAP; }
            else if (c == BridgeDestroyZone.GetColor()) { return AddTriggerType.BRIDGE_DESTROY; }
            else if (c == BeanStalkSpawnZone.GetColor()) { return AddTriggerType.BEANSTALK_SPAWN; }
            else if (c.G == TeleportEntryZone.GetColor().G && c.B == TeleportEntryZone.GetColor().B) { return AddTriggerType.TELEPORT_ENTRY; }
            else if (c.G == TeleportExitZone.GetColor().G && c.B == TeleportExitZone.GetColor().B) { return AddTriggerType.TELEPORT_EXIT; }
            else { return AddTriggerType.UNKNOWN_TRIGGER; }
        }

        private PipeZoneTypeWrapper FindPipeZoneType(Color c)
        {
            if (c.A != 255)
                return null;
            //MOVE N-E-S-W
            else if (c == MoveNorthPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(MoveNorthPipeZone)); }
            else if (c == MoveEastPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(MoveEastPipeZone)); }
            else if (c == MoveSouthPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(MoveSouthPipeZone)); }
            else if (c == MoveWestPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(MoveWestPipeZone)); }
            //MOVE Multi
            else if (c == MoveEastWestPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(MoveEastWestPipeZone)); }
            else if (c == MoveNorthSouthPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(MoveNorthSouthPipeZone)); }
            else if (c == MoveAnyPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(MoveAnyPipeZone)); }
            //ENTER N-E-S-W
            else if (c == EnterNorthPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(EnterNorthPipeZone)); }
            else if (c == EnterEastPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(EnterEastPipeZone)); }
            else if (c == EnterSouthPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(EnterSouthPipeZone)); }
            else if (c == EnterWestPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(EnterWestPipeZone)); }
            // Enter Multi
            else if (c == EnterEastWestPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(EnterEastWestPipeZone)); }
            else if (c == EnterNorthSouthPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(EnterNorthSouthPipeZone)); }
            else if (c == EnterAnyPipeZone.GetColor()) { return new PipeZoneTypeWrapper(typeof(EnterAnyPipeZone)); }
            // Unknown
            else { return new PipeZoneTypeWrapper(null); }
        }

        public List<Rect2d> GetVisionZones()
        {
            List<Color> finished = new List<Color>();

            List<Rect2d> ls = new List<Rect2d>();

            for (int y = 0; y < GetHeight(); y++)
            {
                for (int x = 0; x < GetWidth(); x++)
                {
                    Color c = map.GetColor(LAYER_PLAYERVISION, x, y);
                    if (c.A == 255 && !finished.Contains(c))
                    {
                        finished.Add(c);
                        Rect2d r = (Rect2d)FindVisionRect(c);
                        r *= Block.BLOCK_SIZE;
                        ls.Add(r);
                    }
                }
            }

            return ls;
        }

        private Rect2i FindVisionRect(Color c)
        {
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            for (int y = 0; y < GetHeight(); y++)
            {
                for (int x = 0; x < GetWidth(); x++)
                {
                    if (map.GetColor(LAYER_PLAYERVISION, x, y) == c)
                    {
                        minX = Math.Min(minX, x);
                        maxX = Math.Max(maxX, x + 1);
                        minY = Math.Min(minY, y);
                        maxY = Math.Max(maxY, y + 1);
                    }
                }
            }

            if (minX < maxX && minY < maxY)
                return new Rect2i(minX, GetHeight() - maxY, maxX - minX, maxY - minY);
            else
                throw new Exception("Could not find VisionRect: " + c);
        }
    }
}