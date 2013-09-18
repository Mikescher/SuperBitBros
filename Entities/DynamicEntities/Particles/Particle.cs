using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace SuperBitBros.Entities.DynamicEntities.Particles
{
    public abstract class Particle : DynamicEntity
    {
        private int lifetime = -1;
        private int fadetime_max = 0;
        private int fadetime = 0;

        private double transparency = 1.0;

        public Particle()
            : base()
        {
            distance = DISTANCE_PARTICLES;
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
    }
}
