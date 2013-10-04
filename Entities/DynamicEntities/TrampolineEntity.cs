using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class TrampolineEntity : DynamicEntity
    {
        private const int JUMP_POWER = 16;

        private const int JUMP_COUNTDOWN = 15;

        private bool isRetracted = false;
        private int jumpCountdown = JUMP_COUNTDOWN;

        public TrampolineEntity()
            : base()
        {
            distance = Entity.DISTANCE_STRUCTURES;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT * 2;

            texture = (height == Block.BLOCK_HEIGHT * 2) ? Textures.texture_largetrampoline : Textures.texture_trampoline;

            AddController(new StaticEntityController(this));
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            Player p = (owner as GameWorld).player;

            if (PlayerIsOnTop(p))
            {
                if (isRetracted)
                {
                    if (jumpCountdown-- < 0)
                    {
                        p.DoTrampolineJump(JUMP_POWER);
                    }
                }
                else
                {
                    isRetracted = true;
                    height = Block.BLOCK_HEIGHT;
                    jumpCountdown = JUMP_COUNTDOWN;
                }
            }
            else
            {
                isRetracted = false;
                height = Block.BLOCK_HEIGHT * 2;
                jumpCountdown = JUMP_COUNTDOWN;
            }

            texture = (height == Block.BLOCK_HEIGHT * 2) ? Textures.texture_largetrampoline : Textures.texture_trampoline;
        }

        private bool PlayerIsOnTop(Player p)
        {
            if (((p.position.X < position.X + width) && (p.position.X > position.X)) || ((p.position.X + width < position.X + width) && (p.position.X + width > position.X)))
            {
                double ydelta = p.position.Y - (position.Y + Block.BLOCK_HEIGHT);
                if (ydelta >= 0 && ydelta <= Block.BLOCK_HEIGHT)
                {
                    return true;
                }
            }

            return false;
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return true;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            //
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
