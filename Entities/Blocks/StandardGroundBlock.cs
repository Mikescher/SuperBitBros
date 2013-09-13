using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.OpenGL.Entities.Blocks
{
    class StandardGroundBlock : Block
    {
        public static Color color = Color.FromArgb(0, 0, 0);

        public StandardGroundBlock()
            : base()
        {
            texture = Textures.texture_ground;
        }

        public static Color GetColor()
        {
            return color; 
        }
    }
}
