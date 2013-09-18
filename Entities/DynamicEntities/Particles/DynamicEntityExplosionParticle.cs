using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBitBros.Entities.EnityController;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.DynamicEntities.Particles
{
    public class DynamicEntityExplosionParticle  : FragmentParticle
    {
        private const int STANDARD_LIFETIME = 120;
        private const int STANDARD_FADETIME = 30;

        public DynamicEntityExplosionParticle(DynamicEntity p, int texX, int texY, int fullTextW, int fullTextH, double forceMult)
            : base(p, texX, texY, fullTextW, fullTextH)
        {
            SetLifetime(STANDARD_LIFETIME);
            SetFadetime(STANDARD_FADETIME);

            Vec2d force = new Vec2d(texX - fullTextW / 2, texY - fullTextH / 2);
            force *= forceMult;

            AddController(new ExplosionController(this, force));
        }
    }
}
