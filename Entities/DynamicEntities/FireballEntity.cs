using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class FireballEntity : DynamicEntity
    {
        public FireballEntity(FireBoxBlock block, double fbdistance)
            : base()
        {
            distance = Entity.DISTANCE_STRUCTURES;
            width = 8;
            height = 8;

            texture = Textures.texture_fireball;

            AddController(new FireballEntityController(this, block, fbdistance));
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
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
