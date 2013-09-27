using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.Entities.EnityController;

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

            AddController(new FireballController(this, block, fbdistance));
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
            else
            {
                Mob d = collidingEntity as Mob;
                if (d != null && !d.IsInvincible())
                {
                    d.KillLater();
                }
            }
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
