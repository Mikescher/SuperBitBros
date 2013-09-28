using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class BuzzyBeetleShell : Mob
    {
        public const int REINCARNATION_TIME = 180;

        public const double KOOPASHELL_ACC = 0.2;
        public const double KOOPASHELL_SPEED = 1;

        private int timeUntilReinc;

        private bool suppressExplosion = false;

        public BuzzyBeetleShell(BuzzyBeetle k)
            : base()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            timeUntilReinc = REINCARNATION_TIME;

            texture = Textures.texture_buzzybeetleshell;

            AddController(new BuzzyBeetleShellController(this));
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (!(controllerStack.Peek() as BuzzyBeetleShellController).isStill())
                timeUntilReinc = REINCARNATION_TIME;

            if (timeUntilReinc-- == 0)
            {
                suppressExplosion = true;
                KillLater();
                owner.AddEntity(new BuzzyBeetle(), position.X, position.Y);
            }
        }

        public override void OnHeadJump(Entity e)
        {
            timeUntilReinc = REINCARNATION_TIME;
            (controllerStack.Peek() as BuzzyBeetleShellController).ToogleSlide();
        }

        public override void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            BuzzyBeetleShellController c = (controllerStack.Peek() as BuzzyBeetleShellController);

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
                if (!c.isStill())
                {
                    if (e is BuzzyBeetleShell)
                    {
                        KillLater();
                    }
                    (e as Mob).KillLater();
                }
            }
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