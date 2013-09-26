using OpenTK.Input;

namespace SuperBitBros.Entities.DynamicEntities.Particles
{
    public abstract class Particle : DynamicEntity
    {
        public static int MAX_PARTICLE_COUNT
        {
            get
            {
                return Program.debugParticleSwitch.Value ? 0 : 250;
            }
        }

        public static int GlobalParticleCount = 0;

        private int lifetime = -1;
        private int fadetime_max = 0;
        private int fadetime = 0;

        private double transparency = 1.0;

        private bool doInstantKill = false;

        public Particle()
            : base()
        {
            distance = DISTANCE_PARTICLES;

            if (IsPureOptical())
            {
                GlobalParticleCount++;
                if (GlobalParticleCount > MAX_PARTICLE_COUNT)
                    doInstantKill = true;
            }
        }

        protected void SetLifetime(int t)
        {
            lifetime = t;
        }

        protected void SetFadetime(int t)
        {
            fadetime = t;
            fadetime_max = t;
        }

        public override void OnAdd(GameModel model)
        {
            base.OnAdd(model);

            if (doInstantKill)
                KillLater();
        }

        public override void OnRemove()
        {
            base.OnRemove();

            if (IsPureOptical())
            {
                GlobalParticleCount--;
            }
        }

        public static int GetGlobalParticleCount()
        {
            return GlobalParticleCount;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (lifetime > 0)
            {
                lifetime--;
            }
            else if (lifetime == 0)
            {
                if (fadetime > 0)
                {
                    fadetime--;
                    transparency = fadetime * 1.0 / fadetime_max;
                }
                else
                {
                    KillLater();
                }
            }
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override double GetTransparency()
        {
            return transparency;
        }

        protected virtual bool IsPureOptical() // true := Wird bei zuviel Partikeln nicht angezeigt
        {
            return true;
        }
    }
}
