using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.OpenGL;

namespace SuperBitBros.HUD
{
    public class HUDAnimatedImage : HUDElement
    {
        private AnimatedTexture atexture = new AnimatedTexture();

        public HUDAnimatedImage(int duration, OGLTexture[] textures, int w, int h)
            : base()
        {
            atexture.animation_speed = duration;
            foreach (OGLTexture ogt in textures)
                atexture.Add(0, ogt);

            width = w;
            height = h;
        }

        public override OGLTexture GetCurrentTexture()
        {
            return atexture.GetTexture();
        }

        public override void Update(KeyboardDevice keyboard)
        {
            atexture.Update();
        }
    }
}
