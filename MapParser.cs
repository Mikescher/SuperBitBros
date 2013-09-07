using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBitBros.OpenGL.Entities.Blocks;

namespace SuperBitBros.OpenGL
{
    public enum ParseTriggerType { NO_TRIGGER, SPAWN_PLAYER};

    class MapParser
    {
        private static readonly Color COL_SPAWN_PLAYER = Color.FromArgb(128, 128, 128);

        public static Block findBlock(Color c)
        {
            if (c == StandardGroundBlock.GetColor()) return new StandardGroundBlock();
            //else if (c == StandardAirBlock.GetColor()) return new StandardAirBlock(); //TODO reincomm
            else if (c == CoinBoxBlock.GetColor()) return new CoinBoxBlock();
            else if (c == EmptyCoinBoxBlock.GetColor()) return new EmptyCoinBoxBlock();
            else if (c == HillBlock.GetColor()) return new HillBlock();
            else if (c == PipeBlock.GetColor()) return new PipeBlock();
            else if (c == CastleBlock.GetColor()) return new CastleBlock();
            else if (c == FlagBlock.GetColor()) return new FlagBlock();
            else return null;
        }

        public static ParseTriggerType findSpawnTrigger(Color c)
        {
            if (c == COL_SPAWN_PLAYER) return ParseTriggerType.SPAWN_PLAYER;
            else return ParseTriggerType.NO_TRIGGER;
        }
    }
}
