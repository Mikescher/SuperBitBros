using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using System;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class Hammerbro : Mob
    {
        private const int HAMMERCOOLDOWN = 25;

        private static Random rand = new Random();

        private int hammerCooldown = 0;

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

            if (hammerCooldown-- < 0)
            {
                hammerCooldown = HAMMERCOOLDOWN + (int)(rand.NextDouble() * 10);

                owner.AddEntity(new ShootingHammer(), position.X - 4, position.Y + height);
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