using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.OpenGL.Entities.Blocks
{
    class CastleBlock : Block
    {
        static Color color = Color.FromArgb(255, 0, 0);

        public CastleBlock()
            : base()
        {
            texture = Textures.texture_castle;
        }

        public static Color GetColor()
        {
            return color; 
        }
    }
}
