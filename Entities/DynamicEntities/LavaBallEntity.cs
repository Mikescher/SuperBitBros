using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class LavaBallEntity : DynamicEntity
    {
        public LavaBallEntity()
            : base()
        {
            distance = Entity.DISTANCE_BEHIND_BLOCKS;
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
