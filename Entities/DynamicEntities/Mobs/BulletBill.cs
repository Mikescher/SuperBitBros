using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class BulletBill : Mob
    {
        public BulletBill(int direction)
            : base()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = 9;

            texture = Textures.texture_bulletbill;

            AddController(new BulletBillController(this, direction));
        }

        public override void OnHeadJump(Entity e)
        {
            KillLater();
        }

        public override void OnTouch(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            Player p = collidingEntity as Player;
            if (p != null)
            {
                p.DoDeath(this);
                KillLater();
            }
            else
            {
                Mob d = collidingEntity as Mob;
                if (d != null && !d.IsInvincible() && !d.IsFireballImmune())
                {
                    d.KillLater();
                    KillLater();
                }
            }
        }

        protected override void OnKill()
        {
            base.OnKill();

            Explode();
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}