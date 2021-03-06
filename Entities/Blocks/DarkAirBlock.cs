﻿using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class DarkAirBlock : StandardAirBlock
    {
        public new static Color color = Color.FromArgb(192, 192, 192);

        public DarkAirBlock()
            : base()
        {
            texture = Textures.texture_ground_air;
        }

        public new static Color GetColor()
        {
            return color;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}