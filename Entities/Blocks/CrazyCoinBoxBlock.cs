using Entities.SuperBitBros;
using System;
using System.Drawing;
using OpenTK.Input;

namespace SuperBitBros.OpenGL.Entities.Blocks {
    class CrazyCoinBoxBlock : Block {
        private Random random = new Random();

        private const double COIN_MAX_SPAWN_FORCE = 6;
        private const double COIN_MIN_SPAWN_FORCE = 2;
        private const int COIN_SPAWN_TIME = 7;
        private const int COIN_SPAWN_LIFETIME = 180;

        protected int timeUntilDried = COIN_SPAWN_LIFETIME;
        protected int timeUntilSpawn = 0;
        protected bool isActive = false;

        public static Color color = Color.FromArgb(0, 128, 255);

        public CrazyCoinBoxBlock()
            : base() {
            texture = Textures.texture_coinblock_full;
        }

        public static Color GetColor() {
            return color;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (isActive)
            {
                timeUntilDried--;
                timeUntilSpawn--;

                if (timeUntilSpawn <= 0 && timeUntilDried > 0)
                {
                    CoinEntity ce = new CoinEntity(0, true);
                    owner.AddEntity(ce, GetTopLeft().X, GetTopLeft().Y);
                    double angle = random.NextDouble() * (Math.PI / 3) - (Math.PI / 6);
                    angle += Math.Sign(angle) * (Math.PI / 8);
                    double force = random.NextDouble() * (COIN_MAX_SPAWN_FORCE - COIN_MIN_SPAWN_FORCE) + COIN_MIN_SPAWN_FORCE;
                    ce.movementDelta.X = Math.Sin(angle) * force;
                    ce.movementDelta.Y = Math.Cos(angle) * force;

                    timeUntilSpawn = COIN_SPAWN_TIME;
                    timeUntilDried--;
                }

                if (timeUntilDried <= 0)
                {
                    isActive = false;
                    ((GameWorld)owner).ReplaceBlock(this, new EmptyCoinBoxBlock());
                }
            }
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision) {
            if (isBlockingMovement && collidingEntity.GetType() == typeof(Player) && collidingEntity.GetTopLeft().Y <= GetBottomRight().Y && ((Player)collidingEntity).movementDelta.Y > 0) {
                isActive = true;
            }
        }
    }
}
