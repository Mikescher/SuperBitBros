using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class BuzzyBeetle : Mob
    {
        private bool suppressExplosion = false;

        public BuzzyBeetle()
            : base()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_buzzybeetle;

            AddController(new DefaultMobController(this));
        }

        public override void OnHeadJump(Entity e)
        {
            suppressExplosion = true;
            KillLater();
            owner.AddEntity(new BuzzyBeetleShell(this), position.X, position.Y);
        }

        public override void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            Player p = e as Player;
            if (p != null)
                p.DoDeath(this);
        }

        public override bool IsFireballImmune()
        {
            return true;
        }

        protected override void OnKill()
        {
            base.OnKill();

            if (!suppressExplosion)
                Explode();
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}