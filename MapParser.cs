using SuperBitBros.OpenGL.Entities.Blocks;
using System.Drawing;

namespace SuperBitBros.OpenGL {
    public enum ParseTriggerType { NO_TRIGGER, SPAWN_PLAYER, SPAWN_GOOMBA, SPAWN_PIRANHAPLANT };

    class MapParser {
        private static readonly Color COL_SPAWN_PLAYER = Color.FromArgb(128, 128, 128);
        private static readonly Color COL_SPAWN_GOOMBA = Color.FromArgb(127, 0, 0);
        private static readonly Color COL_SPAWN_PIRANHAPLANT = Color.FromArgb(0, 127, 0);

        public static Block findBlock(Color c) {
            if (c == StandardGroundBlock.GetColor())
                return new StandardGroundBlock();
            else if (c == StandardAirBlock.GetColor())
                return new StandardAirBlock();
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

        public static ParseTriggerType findSpawnTrigger(Color c) {
            if (c == COL_SPAWN_PLAYER)
                return ParseTriggerType.SPAWN_PLAYER;
            else if (c == COL_SPAWN_GOOMBA)
                return ParseTriggerType.SPAWN_GOOMBA;
            else if (c == COL_SPAWN_PIRANHAPLANT)
                return ParseTriggerType.SPAWN_PIRANHAPLANT;
            else
                return ParseTriggerType.NO_TRIGGER;
        }
    }
}
