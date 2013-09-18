using System;
using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Particles;
using SuperBitBros.HUD;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class CoinEntity : AnimatedDynamicEntity
    {
        public const int COIN_EXPLOSIONFRAGMENTS_X = 3;
        public const int COIN_EXPLOSIONFRAGMENTS_Y = 3;
        private const double COIN_EXPLOSIONFRAGMENTS_FORCE = 24.0;

        private static Random random = new Random();

        public CoinEntity()
            : base()
        {
            distance = Entity.DISTANCE_POWERUPS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            atexture.animation_speed = 15;

            atexture.Add(0, Textures.texture_coin_0);
            atexture.Add(0, Textures.texture_coin_1);
            atexture.Add(0, Textures.texture_coin_2);
            atexture.Add(0, Textures.texture_coin_3);
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            atexture.Update();
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (collidingEntity.GetType() == typeof(Player) && isDirectCollision)
            {
                CreateCoinParticles();
                KillLater();
            }
        }

        private void CreateCoinParticles()
        {
            StandardGameHUD hud = owner.HUD as StandardGameHUD;

            if (hud == null) return;

            double forceMult = COIN_EXPLOSIONFRAGMENTS_FORCE / (Math.Sqrt(width * width + height * height) / 2.0);

            double w = width / COIN_EXPLOSIONFRAGMENTS_X;
            double h = height / COIN_EXPLOSIONFRAGMENTS_Y;

            for (int y = 0; y < COIN_EXPLOSIONFRAGMENTS_Y; y++)
            {
                for (int x = 0; x < COIN_EXPLOSIONFRAGMENTS_X; x++)
                {
                    owner.AddEntity(
                        new CoinExplosionParticle(
                            this,
                            x,
                            y,
                            COIN_EXPLOSIONFRAGMENTS_X,
                            COIN_EXPLOSIONFRAGMENTS_Y,
                            forceMult + (random.NextDouble()*6-3),
                            hud),
                        position.X + x * w,
                        position.Y + y * h);
                }
            }
        }
    }
}