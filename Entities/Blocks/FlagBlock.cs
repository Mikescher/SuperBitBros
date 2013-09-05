using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.Entities.Blocks
{
    class FlagBlock : Block
    {
        static Color color = Color.FromArgb(0, 255, 255);

        public FlagBlock()
            : base()
        {
            texture = Textures.texture_flag;
        }

        public static Color GetColor()
        {
            return color; 
        }
    }
}
