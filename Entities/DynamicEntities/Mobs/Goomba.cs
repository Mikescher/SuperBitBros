using System;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class Goomba : Mob
    {
        public const double GOOMBA_ACC = 0.2;
        public const double GOOMBA_SPEED = 1;

        public Goomba()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_goomba;

            AddController(new DefaultMobController(this));
        }

        public override void OnHeadJump(Entity e)
        {
            owner.RemoveEntity(this);
            owner.AddEntity(new GoombaCorpse(this), position.X, position.Y);
        }

        public override void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            if (e.GetType() == typeof(Player))
                Console.Out.WriteLine("DEAD");
        }
    }
}