using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SuperBitBros.OpenGL;

namespace SuperBitBros.HUD
{
    public class HUDImage : HUDElement
    {
        public HUDImage(OGLTexture tex, int w, int h)
            : base()
        {
            texture = tex;
            width = w;
            height = h;
        }

        public override void Update(KeyboardDevice keyboard, double ucorrection)
        {

        }
    }
}
