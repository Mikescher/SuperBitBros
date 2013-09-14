using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.Entities.Blocks
{
    class EndlessCrazyCoinBoxBlock : CrazyCoinBoxBlock
    {
        public static new Color color = Color.FromArgb(0, 64, 255);

        public EndlessCrazyCoinBoxBlock()
            : base() {
            isActive = true;
            timeUntilDried = int.MaxValue;
        }

        public static new Color GetColor()
        {
            return color;
        }
    }
}
