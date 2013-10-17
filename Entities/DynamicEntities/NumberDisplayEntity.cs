using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using System;
using System.Drawing;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class NumberDisplayEntity : DynamicEntity
    {
        private static Random rand = new Random();

        private const int FLOAT_DURATION = 60;
        private const int FLOAT_DISTANCE = Block.BLOCK_HEIGHT / 2;


        public NumberDisplayEntity()
            : base()
        {
            distance = Entity.DISTANCE_STRUCTURES;
            width = Block.BLOCK_WIDTH * 2;
            height = Block.BLOCK_HEIGHT * 2;

            texture = Textures.texture_lava; // default

            AddController(new FreeFloatingEntityController(this, FLOAT_DISTANCE, (int)(FLOAT_DURATION + rand.NextDouble() * 6 - 3)));
        }

        public override void InitAfterMapParse(Color c)
        {
            int nmr = c.R - 100;
            if (nmr < 0 || nmr > 9)
                throw new NotSupportedException(String.Format("Color for NumberDisplay ({0}) not supported: {1}", nmr, c));

            texture = Textures.number_sheet.GetTextureWrapper(nmr, 1);
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override double GetTransparency()
        {
            return 1.0;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_NUMBER;
        }
    }
}