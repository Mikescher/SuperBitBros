using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.Entities.Blocks
{
    class PipeBlock : Block
    {
        static Color color = Color.FromArgb(0, 255, 0);

        public PipeBlock()
            : base()
        {
            texture = Textures.texture_pipe;
        }

        public static Color GetColor()
        {
            return color; 
        }
    }
}
