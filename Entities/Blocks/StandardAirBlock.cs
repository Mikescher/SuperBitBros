using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.SuperBitBros;

namespace SuperBitBros.Entities.Blocks
{
    class StandardAirBlock : Block
    {
        static Color color = Color.FromArgb(255, 255, 255);

        public StandardAirBlock()
            : base()
        {
            texture = Textures.texture_air;
        }

        public static Color GetColor()
        {
            return color; 
        }

        public override bool IsBlocking(Entity sender)
        {
            return false;
        }
    }
}
