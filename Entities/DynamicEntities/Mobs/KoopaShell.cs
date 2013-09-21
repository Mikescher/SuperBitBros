using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class KoopaShell : Mob
    {
        public const int REINCARNATION_TIME = 180;

        public const double KOOPASHELL_ACC = 0.2;
        public const double KOOPASHELL_SPEED = 1;

        private double timeUntilReinc;

        private bool suppressExplosion = false;

        public KoopaShell(Koopa k)
            : base()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            timeUntilReinc = REINCARNATION_TIME;

            texture = Textures.texture_koopashell;

            AddController(new KoopaShellController(this));
        }

        public override void Update(KeyboardDevice keyboard, double ucorrection)
        {
            base.Update(keyboard, ucorrection);

            if (!(controllerStack.Peek() as KoopaShellController).isStill())
                timeUntilReinc = REINCARNATION_TIME;

            timeUntilReinc -= ucorrection;
            if (timeUntilReinc <= 0)
            {
                suppressExplosion = true;
                KillLater();
                owner.AddEntity(new Koopa(), position.X, position.Y);
            }
        }

        public override void OnHeadJump(Entity e)
        {
            timeUntilReinc = REINCARNATION_TIME;
            (controllerStack.Peek() as KoopaShellController).ToogleSlide();
        }

        public override void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            KoopaShellController c = (controllerStack.Peek() as KoopaShellController);

            Player p = e as Player;
            if (p != null)
            {
                if (c.isStill())
                {
                    timeUntilReinc = REINCARNATION_TIME;
                    c.DoSlide(position.X - e.position.X);
                }
                else
                {
                    timeUntilReinc = REINCARNATION_TIME;
                    c.DoSlide(position.X - e.position.X);
                    p.DoDeath(this);
                }
            }
            else if (e is Mob)
            {
                if (e is KoopaShell)
                {
                    KillLater();
                }
                (e as Mob).KillLater();
            }
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