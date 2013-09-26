using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class LavaBallEntity : DynamicEntity
    {
        public LavaBallEntity()
            : base()
        {
            distance = Entity.DISTANCE_HUD;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_lavaball;

            AddController(new LavaballController(this));
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }


        public override bool IsKillZoneImmune()
        {
            return true;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            Player p = collidingEntity as Player;
            if (p != null)
            {
                p.DoDeath(this);
            }
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
