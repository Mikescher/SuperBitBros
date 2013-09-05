using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.Entities.Blocks
{
    class CoinBoxBlock : Block
    {
        static Color color = Color.FromArgb(0, 0, 255);

        public CoinBoxBlock()
            : base()
        {
            texture = Textures.texture_coinblock_full;
        }

        public static Color GetColor()
        {
            return color; 
        }
    }
}
