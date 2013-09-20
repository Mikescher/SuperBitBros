using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class CrazyCoinBoxBlock : Block
    {
        private Random random = new Random();

        private const double COIN_MAX_SPAWN_FORCE = 6;
        private const double COIN_MIN_SPAWN_FORCE = 2;
        private const int COIN_SPAWN_TIME = 7;
        private const int COIN_SPAWN_LIFETIME = 180;

        protected double timeUntilDried = COIN_SPAWN_LIFETIME;
        protected double timeUntilSpawn = 0;
        protected bool isActive = false;

        public static Color color = Color.FromArgb(0, 128, 255);

        public CrazyCoinBoxBlock()
            : base()
        {
            texture = Textures.texture_coinblock_full;
        }

        public static Color GetColor()
        {
            return color;
        }

        public override void Update(KeyboardDevice keyboard, double ucorrection)
        {
            base.Update(keyboard, ucorrection);

            if (isActive)
            {
                timeUntilDried -= ucorrection;
                timeUntilSpawn -= ucorrection;

                if (timeUntilSpawn <= 0 && timeUntilDried > 0)
                {
                    double angle = random.NextDouble() * (Math.PI / 3) - (Math.PI / 6);
                    angle += Math.Sign(angle) * (Math.PI / 8);
                    double force = random.NextDouble() * (COIN_MAX_SPAWN_FORCE - COIN_MIN_SPAWN_FORCE) + COIN_MIN_SPAWN_FORCE;

                    CoinEntity ce = new GravityCoinEntity(new Vec2d(Math.Sin(angle) * force, Math.Cos(angle) * force), true);
                    owner.AddEntity(ce, GetTopLeft().X, GetTopLeft().Y);

                    timeUntilSpawn += COIN_SPAWN_TIME;
                }

                if (timeUntilDried <= 0)
                {
                    isActive = false;
                    ((GameWorld)owner).ReplaceBlock(this, new EmptyCoinBoxBlock());
                }
            }
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (isBlockingMovement && collidingEntity.GetType() == typeof(Player) && collidingEntity.GetTopLeft().Y <= GetBottomRight().Y && ((Player)collidingEntity).GetMovement().Y > 0)
            {
                isActive = true;
            }
        }

        public override Color GetBlockColor()
        {
            return color;
        }
    }
}