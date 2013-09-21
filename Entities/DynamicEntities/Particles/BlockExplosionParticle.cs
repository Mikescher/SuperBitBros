using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.DynamicEntities.Particles
{
    public class BlockExposionParticle : FragmentParticle
    {
        private const int STANDARD_LIFETIME = 0;
        private const int STANDARD_FADETIME = 60;

        EntityRenderType rtype;

        public BlockExposionParticle(Block b, int texX, int texY, int fullTextW, int fullTextH, double forceMult)
            : base(b, texX, texY, fullTextW, fullTextH)
        {
            SetLifetime(STANDARD_LIFETIME);
            SetFadetime(STANDARD_FADETIME);

            rtype = b.GetRenderType();

            Vec2d force = new Vec2d(texX - fullTextW / 2, texY - fullTextH / 2);
            force *= forceMult;

            AddController(new SimpleExplosionController(this, force));
        }

        protected override bool IsPureOptical()
        {
            return false;
        }

        public override EntityRenderType GetRenderType()
        {
            return rtype;
        }
    }
}
