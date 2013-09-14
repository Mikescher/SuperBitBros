using SuperBitBros.OpenGL.Entities.Blocks;
using System;
using System.Drawing;

namespace SuperBitBros.OpenGL {
    public enum ParseTriggerType { NO_TRIGGER, SPAWN_PLAYER, SPAWN_GOOMBA, SPAWN_PIRANHAPLANT, SPAWN_COIN };

    class ImageMapParser {
        private static readonly Color COL_SPAWN_PLAYER = Color.FromArgb(128, 128, 128);
        private static readonly Color COL_SPAWN_GOOMBA = Color.FromArgb(127, 0, 0);
        private static readonly Color COL_SPAWN_PIRANHAPLANT = Color.FromArgb(0, 127, 0);
        private static readonly Color COL_SPAWN_COIN = Color.FromArgb(100, 200, 100);

        private Type mapAir = typeof(StandardAirBlock);
        private Bitmap map;

        public ImageMapParser(Bitmap map) {
            this.map = map;
        }

        public int GetWidth() {
            return map.Width;
        }

        public int GetHeight() {
            return map.Height;
        }

        public Color GetPositionColor(int x, int y) {
            return map.GetPixel(x, y);
        }

        public Block FindBlock(int x, int y) {
            return FindBlock(GetPositionColor(x, y));
        }

        public Block FindBlock(Color c) {
            if (c == StandardGroundBlock.GetColor())
                return new StandardGroundBlock();
            else if (c == StandardAirBlock.GetColor()) {
                mapAir = typeof(StandardAirBlock);
                return new StandardAirBlock();
            } else if (c == GroundAirBlock.GetColor()) {
                mapAir = typeof(GroundAirBlock);
                return new GroundAirBlock();
            } else if (c == CoinBoxBlock.GetColor())
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

        public ParseTriggerType FindSpawnTrigger(int x, int y) {
            return FindSpawnTrigger(GetPositionColor(x, y));
        }

        public ParseTriggerType FindSpawnTrigger(Color c) {
            if (c == COL_SPAWN_PLAYER)
                return ParseTriggerType.SPAWN_PLAYER;
            else if (c == COL_SPAWN_GOOMBA)
                return ParseTriggerType.SPAWN_GOOMBA;
            else if (c == COL_SPAWN_PIRANHAPLANT)
                return ParseTriggerType.SPAWN_PIRANHAPLANT;
            else if (c == COL_SPAWN_COIN)
                return ParseTriggerType.SPAWN_COIN;
            else
                return ParseTriggerType.NO_TRIGGER;
        }

        public StandardAirBlock GetMapAirBlock() {
            return (StandardAirBlock)Activator.CreateInstance(mapAir);
        }
    }
}
