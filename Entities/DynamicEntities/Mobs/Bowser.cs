using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class Bowser : Mob
    {
        public const double BOWSER_SCALE = 1.5;

        public Bowser()
            : base()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH * 2 * BOWSER_SCALE;
            height = Block.BLOCK_HEIGHT * BOWSER_SCALE;

            texture = Textures.texture_bowser;

            AddController(new BowserController(this));
        }

        public override void OnAfterMapGen()
        {
            base.OnAfterMapGen();
            (controllerStack.Peek() as BowserController).OnAfterMapGen();
        }

        public override void OnHeadJump(Entity e)
        {
            Player p = e as Player;
            if (p != null)
                p.DoDeath(this);
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
