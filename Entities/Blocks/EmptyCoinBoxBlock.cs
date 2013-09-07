using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.OpenGL.Entities.Blocks
{
    class EmptyCoinBoxBlock : Block
    {
        static Color color = Color.FromArgb(255, 255, 0);

        public EmptyCoinBoxBlock()
            : base()
        {
            texture = Textures.texture_coinblock_empty;
        }

        public static Color GetColor()
        {
            return color; 
        }
    }
}
