using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class ShootingSpikeBallEntity : DynamicEntity
    {
        public int DESINTEGRATE_TIME = 90;

        private int time = 0;

        public ShootingSpikeBallEntity(int direction)
            : base()
        {
            distance = Entity.DISTANCE_BEHIND_BLOCKS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_WIDTH;

            texture = Textures.texture_spikeball;

            AddController(new ShootingSpikeBallController(this, direction));
        }

        public bool IsDesintegrated()
        {
            return time > DESINTEGRATE_TIME;
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        protected override bool IsNeverBlocking()
        {
            return IsDesintegrated();
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            time++;
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
                if (d != null && !d.IsInvincible() && !(d is Lakitu))
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
