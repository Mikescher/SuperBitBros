using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenRasterFormat;
using System.Drawing;

namespace SuperBitBros {
    public enum SpawnEntityType { NO_SPAWN, UNKNOWN_SPAWN, SPAWN_GOOMBA, SPAWN_PIRANHAPLANT, SPAWN_COIN };
    public enum AddTriggerType { NO_TRIGGER, UNKNOWN_TRIGGER, DEATH_ZONE, PLAYER_SPAWN_POSITION };

    class ImageMapParser {
        public const string LAYER_BLOCKS = "Blocks";
        public const string LAYER_ENTITIES = "Entities";
        public const string LAYER_TRIGGER = "Trigger";

        private static readonly Color COL_SPAWN_GOOMBA = Color.FromArgb(127, 0, 0);
        private static readonly Color COL_SPAWN_PIRANHAPLANT = Color.FromArgb(0, 127, 0);
        private static readonly Color COL_SPAWN_COIN = Color.FromArgb(100, 200, 100);

        private static readonly Color COL_SPAWN_PLAYER = Color.FromArgb(128, 128, 128);
        private static readonly Color COL_DEATH_ZONE = Color.FromArgb(127, 0, 64);


        public readonly OpenRasterImage map;

        public ImageMapParser(OpenRasterImage map) {
            this.map = map;
        }

        public int GetWidth() {
            return map.Width;
        }

        public int GetHeight() {
            return map.Height;
        }

        public Block GetBlock(int x, int y) {
            return FindBlock(map.GetColor(LAYER_BLOCKS, x, y));
        }

        public SpawnEntityType GetEntity(int x, int y) {
            return FindSpawnEntityType(map.GetColor(LAYER_ENTITIES, x, y));
        }

        public AddTriggerType GetTrigger(int x, int y) {
            return FindTriggerType(map.GetColor(LAYER_TRIGGER, x, y));
        }

        private Block FindBlock(Color c) {
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

        private SpawnEntityType FindSpawnEntityType(Color c) {
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

        private AddTriggerType FindTriggerType(Color c) {
            if (c.A != 255)
                return AddTriggerType.NO_TRIGGER;
            else if (c == COL_SPAWN_PLAYER)
                return AddTriggerType.PLAYER_SPAWN_POSITION;
            else if (c == COL_DEATH_ZONE)
                return AddTriggerType.DEATH_ZONE;
            else
                return AddTriggerType.UNKNOWN_TRIGGER;
        }
    }
}
