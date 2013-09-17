﻿using System.Drawing;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenRasterFormat;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;

namespace SuperBitBros
{
    public enum SpawnEntityType { NO_SPAWN, UNKNOWN_SPAWN, SPAWN_GOOMBA, SPAWN_PIRANHAPLANT, SPAWN_COIN };

    public enum AddTriggerType { NO_TRIGGER, UNKNOWN_TRIGGER, DEATH_ZONE, PLAYER_SPAWN_POSITION };

    public class ImageMapParser
    {
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

        public SpawnEntityType GetEntity(int x, int y)
        {
            return FindSpawnEntityType(map.GetColor(LAYER_ENTITIES, x, y));
        }

        public AddTriggerType GetTrigger(int x, int y)
        {
            return FindTriggerType(map.GetColor(LAYER_TRIGGER, x, y));
        }

        public PipeZoneTypeWrapper GetPipeZone(int x, int y)
        {
            return FindPipeZoneType(map.GetColor(LAYER_PIPEZONES, x, y));
        }

        private Block FindBlock(Color c)
        {
            if (c == StandardGroundBlock.GetColor())
                return new StandardGroundBlock();
            else if (c == StandardAirBlock.GetColor())
                return new StandardAirBlock();
            else if (c == GroundAirBlock.GetColor())
                return new GroundAirBlock();
            else if (c == CoinBoxBlock.GetColor())
                return new CoinBoxBlock();
            else if (c == EmptyCoinBoxBlock.GetColor())
                return new EmptyCoinBoxBlock();
            else if (c == HillBlock.GetColor())
                return new HillBlock();
            else if (c == PipeBlock.GetColor())
                return new PipeBlock();
            else if (c == CastleBlock.GetColor())
                return new CastleBlock();
            else if (c == FlagBlock.GetColor())
                return new FlagBlock();
            else if (c == CrazyCoinBoxBlock.GetColor())
                return new CrazyCoinBoxBlock();
            else if (c == EndlessCrazyCoinBoxBlock.GetColor())
                return new EndlessCrazyCoinBoxBlock();
            else
                return null;
        }

        private SpawnEntityType FindSpawnEntityType(Color c)
        {
            if (c.A != 255)
                return SpawnEntityType.NO_SPAWN;
            else if (c == COL_SPAWN_GOOMBA)
                return SpawnEntityType.SPAWN_GOOMBA;
            else if (c == COL_SPAWN_PIRANHAPLANT)
                return SpawnEntityType.SPAWN_PIRANHAPLANT;
            else if (c == COL_SPAWN_COIN)
                return SpawnEntityType.SPAWN_COIN;
            else
                return SpawnEntityType.UNKNOWN_SPAWN;
        }

        private AddTriggerType FindTriggerType(Color c)
        {
            if (c.A != 255)
                return AddTriggerType.NO_TRIGGER;
            else if (c == PlayerSpawnZone.GetColor())
                return AddTriggerType.PLAYER_SPAWN_POSITION;
            else if (c == DeathZone.GetColor())
                return AddTriggerType.DEATH_ZONE;
            else
                return AddTriggerType.UNKNOWN_TRIGGER;
        }

        private PipeZoneTypeWrapper FindPipeZoneType(Color c)
        {
            if (c.A != 255)
                return null;
            //MOVE N-E-S-W
            else if (c == MoveNorthPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(MoveNorthPipeZone));
            else if (c == MoveEastPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(MoveEastPipeZone));
            else if (c == MoveSouthPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(MoveSouthPipeZone));
            else if (c == MoveWestPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(MoveWestPipeZone));
            //MOVE Multi
            else if (c == MoveEastWestPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(MoveEastWestPipeZone));
            else if (c == MoveNorthSouthPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(MoveNorthSouthPipeZone));
            else if (c == MoveAnyPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(MoveAnyPipeZone));
            //ENTER N-E-S-W
            else if (c == EnterNorthPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(EnterNorthPipeZone));
            else if (c == EnterEastPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(EnterEastPipeZone));
            else if (c == EnterSouthPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(EnterSouthPipeZone));
            else if (c == EnterWestPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(EnterWestPipeZone));
            // Enter Multi
            else if (c == EnterEastWestPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(EnterEastWestPipeZone));
            else if (c == EnterNorthSouthPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(EnterNorthSouthPipeZone));
            else if (c == EnterAnyPipeZone.GetColor())
                return new PipeZoneTypeWrapper(typeof(EnterAnyPipeZone));
            // Unknown
            else
                return new PipeZoneTypeWrapper(null);
        }
    }
}