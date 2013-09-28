using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class ShootingFireballEntity : DynamicEntity
    {
        private const int LIFETIME = 240;

        private int lifetime = LIFETIME;

        public ShootingFireballEntity(int direction)
            : base()
        {
            distance = Entity.DISTANCE_POWERUPS;
            width = 8;
            height = 8;

            texture = Textures.texture_fireball;

            AddController(new ShootingFireballController(this, direction));
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (lifetime-- < 0)
                KillLater();
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (collidingEntity is Mob && isDirectCollision)
            {
                if (!(collidingEntity as Mob).IsInvincible() && !(collidingEntity as Mob).IsFireballImmune())
                {
                    (collidingEntity as DynamicEntity).KillLater();
                }
                KillLater();
            }
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
