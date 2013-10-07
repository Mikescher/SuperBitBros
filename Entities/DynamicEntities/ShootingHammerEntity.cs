using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class ShootingHammerEntity : DynamicEntity
    {
        public ShootingHammerEntity()
            : base()
        {
            distance = Entity.DISTANCE_STRUCTURES;
            width = 16;
            height = 16;

            texture = Textures.texture_hammer;

            AddController(new ShootingHammerController(this));
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        protected override bool IsNeverBlocking()
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
            else
            {
                Mob d = collidingEntity as Mob;
                if (d != null && !d.IsInvincible() && !(d is Hammerbro) && !(d is HammerBowser))
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
