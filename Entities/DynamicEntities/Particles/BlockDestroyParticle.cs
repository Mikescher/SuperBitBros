using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.DynamicEntities.Particles
{
    public class BlockDestroyParticle : FragmentParticle
    {
        private const int STANDARD_LIFETIME = 120;
        private const int STANDARD_FADETIME = 30;

        EntityRenderType rtype;

        public BlockDestroyParticle(Block p, int texX, int texY, int fullTextW, int fullTextH, double forceMult)
            : base(p, texX, texY, fullTextW, fullTextH)
        {
            SetLifetime(STANDARD_LIFETIME);
            SetFadetime(STANDARD_FADETIME);

            rtype = p.GetRenderType();

            Vec2d force = new Vec2d(texX - fullTextW / 2, texY);
            force *= forceMult;
            force.Y = Math.Abs(force.Y);

            AddController(new ExplosionController(this, force));
        }

        public override EntityRenderType GetRenderType()
        {
            return rtype;
        }
    }
}
