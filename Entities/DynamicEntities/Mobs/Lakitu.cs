using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using System;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class Lakitu : Mob
    {
        private const int SPIKECOOLDOWN = 70;

        private static Random rand = new Random();

        private int spikeCooldown = 0;

        private LakituController controller;

        public Lakitu()
            : base()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_lakitu;

            AddController(controller = new LakituController(this));
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (spikeCooldown-- < 0)
            {
                spikeCooldown = SPIKECOOLDOWN + (int)(rand.NextDouble() * 30);

                if (controller.direction == -1)
                {
                    owner.AddEntity(new ShootingSpikeBallEntity(controller.direction), position.X - 8, position.Y + height / 2.0);
                }
                else
                {
                    owner.AddEntity(new ShootingSpikeBallEntity(controller.direction), position.X + Block.BLOCK_WIDTH + 8, position.Y + height / 2.0);
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