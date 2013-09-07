using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.OpenGL.Entities.Blocks
{
    class HillBlock : Block
    {
        static Color color = Color.FromArgb(255, 0, 255);

        public HillBlock()
            : base()
        {
            texture = Textures.texture_hill;
        }

        public static Color GetColor()
        {
            return color; 
        }
    }
}
