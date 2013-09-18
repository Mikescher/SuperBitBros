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
        public int Lifetime { get; protected set; }

        public Particle()
            : base()
        {
            distance = DISTANCE_PARTICLES;

            Lifetime = -1;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (Lifetime > 0)
            {
                Lifetime--;
            }
            else if (Lifetime == 0)
            {
                KillLater();
            }
        }
    }
}
