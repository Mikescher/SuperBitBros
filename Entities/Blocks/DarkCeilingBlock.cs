﻿using SuperBitBros.Entities.DynamicEntities;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class DarkCeilingBlock : StandardCeilingBlock
    {
        public new static Color color = Color.FromArgb(64, 128, 64);

        public DarkCeilingBlock()
            : base()
        {
            texture = Textures.texture_darkCeiling;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (isBlockingMovement && collidingEntity.GetType() == typeof(Player) && (collidingEntity as Player).IsBig() && collidingEntity.GetTopLeft().Y <= GetBottomRight().Y && ((Player)collidingEntity).GetMovement().Y > 0)
            {
                DestroyExplode();
                ((GameWorld)owner).ReplaceBlock(this, new DarkAirBlock());
            }
        }

        public new static Color GetColor()
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
