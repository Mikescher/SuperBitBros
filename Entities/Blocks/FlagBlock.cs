﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.SuperBitBros;

namespace SuperBitBros.OpenGL.Entities.Blocks
{
    class FlagBlock : Block
    {
        public static Color color = Color.FromArgb(0, 255, 255);

        private bool isStatic = false;

        public FlagBlock()
            : base()
        {
            texture = Textures.texture_flag;
        }

        public static Color GetColor()
        {
            return color; 
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision)
        {
            if (collidingEntity.GetType() == typeof(Player) && isDirectCollision && !isStatic)
            {
                for (int ny = blockPosY; ny < GameModel.MAP_HEIGHT_MAX; ny++)
                {
                    Block b = owner.GetBlock(blockPosX, ny);
                    if (b != null && b.GetType() == typeof(FlagBlock))
                        owner.ReplaceBlock(b, new StandardAirBlock());
                }

                for (int ny = blockPosY - 1; ny >= 0; ny--)
                {
                    Block b = owner.GetBlock(blockPosX, ny);
                    if (b != null && b.GetType() == typeof(FlagBlock))
                        ((FlagBlock)b).isStatic = true;
                }
            }
        }
    }
}
