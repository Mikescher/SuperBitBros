using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using System;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class BillBlasterBlock : Block
    {
        public static Color color = Color.FromArgb(32, 32, 32);

        private static Random rand = new Random();

        private const int DELAY = 120;

        private int delay = DELAY;

        public BillBlasterBlock()
            : base()
        {
            texture = Textures.texture_billblaster;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (delay-- < 0)
            {
                delay = DELAY + (int)(rand.NextDouble() * 40 - 20);

                int direction = Math.Sign((owner as GameWorld).player.position.X - position.X);

                owner.TestAddEntity(new BulletBill(direction), position.X + ((direction == -1) ? (-(width + 4)) : (width + 4)), position.Y + height / 2.0 - 1.5);
            }
        }

        public static Color GetColor()
        {
            return color;
        }

        public override Color GetBlockColor()
        {
            return color;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}