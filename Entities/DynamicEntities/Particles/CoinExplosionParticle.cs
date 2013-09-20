using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using SuperBitBros.HUD;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.DynamicEntities.Particles
{
    public class CoinExplosionParticle : FragmentParticle
    {
        private const int STANDARD_LIFETIME = 1200;
        private const int STANDARD_FADETIME = 0;

        public CoinExplosionParticle(CoinEntity c, int texX, int texY, int fullTextW, int fullTextH, double forceMult, StandardGameHUD hud)
            : base(c, texX, texY, fullTextW, fullTextH)
        {
            SetLifetime(STANDARD_LIFETIME);
            SetFadetime(STANDARD_FADETIME);

            Vec2d force = new Vec2d(texX - fullTextW / 2, texY - fullTextH / 2);
            force *= forceMult;

            AddController(new CoinExplosionController(this, force, hud, (c.owner as GameWorld).offset));
        }

        protected override bool IsPureOptical() 
        {
            return false; // Wird bei zuviel Partikeln nicht angezeigt
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
