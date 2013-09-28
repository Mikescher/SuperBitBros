using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class Goomba : Mob
    {
        public Goomba()
            : base()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_goomba;

            AddController(new DefaultMobController(this));
        }

        public override void OnHeadJump(Entity e)
        {
            KillLater();
            owner.AddEntity(new GoombaCorpse(this), position.X, position.Y);
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