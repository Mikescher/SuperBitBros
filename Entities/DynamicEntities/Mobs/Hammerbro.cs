using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using System;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class Hammerbro : Mob
    {
        private const int HAMMERCOOLDOWN = 10;
        private const int HAMMERCOUNT = 10;
        private const int SLEEPTIME = 180;

        private static Random rand = new Random();

        private int hammerCooldown = 0;
        private int hammerCount = 0;
        private int sleepTime = 0;

        public Hammerbro()
            : base()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_hammerbro;

            AddController(new HammerbroController(this));
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (sleepTime-- < 0)
            {
                if (hammerCount <= 0)
                {
                    sleepTime = (int)(SLEEPTIME * (rand.NextDouble() * 0.5 + 0.5));
                    hammerCount = HAMMERCOUNT;
                }
                else
                {
                    if (hammerCooldown-- < 0)
                    {
                        hammerCooldown = (int)(HAMMERCOOLDOWN * (rand.NextDouble() * 0.5 + 0.5));

                        owner.AddEntity(new ShootingHammerEntity(), position.X - 4, position.Y + height);
                        hammerCount--;
                    }
                }
            }
        }

        public override void OnHeadJump(Entity e)
        {
            KillLater();
        }

        public override void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            Player p = e as Player;
            if (p != null)
                p.DoDeath(this);
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