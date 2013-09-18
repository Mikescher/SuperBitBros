using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.DynamicEntities.Particles
{
    public abstract class FragmentParticle : Particle
    {
        public FragmentParticle(Entity e, int texX, int texY, int fullW, int fullH)
            : base()
        {
            OGLTexture baseTex = e.GetCurrentTexture();
            Rect2d coords = baseTex.GetCoordinates();
            double w = coords.Width / (fullW * 1.0);
            double h = coords.Height / (fullH * 1.0);
            double x = coords.bl.X + w*texX;
            double y = coords.tl.Y - h*texY - h;

            width = e.width / (fullW * 1.0);
            height = e.height / (fullH * 1.0);
            texture = new OGLTextureFragment(baseTex.GetID(), x, y, w, h).GetTextureWrapper();
        }
    }
}
