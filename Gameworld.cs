using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBitBros.Properties;

namespace SuperBitBros
{
    class Gameworld : GameModel
    {
        private const double BLOCK_SIZE = 10;

        public Gameworld() : base()
        {
            loadMapFromResources("map_01-01.png");
        }

        public void loadMapFromResources(string resname)
        {
            Bitmap bmp = Resources.map_01_01;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixel = bmp.GetPixel(x, bmp.Height - (1 + y));

                    if (pixel != Color.FromArgb(255, 255, 255))
                    {
                        AddEntity(new GroundEntity(), BLOCK_SIZE * x, BLOCK_SIZE * y);
                    }
                }
            }
        }
    }
}
