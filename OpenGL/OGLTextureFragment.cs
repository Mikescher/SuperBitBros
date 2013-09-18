using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.OpenGL
{
    public class OGLTextureFragment
    {
        public int ID { get; private set; }

        public Rect2d coords { get; private set; }

        public OGLTextureFragment(int id, double x, double y, double w, double h)
        {
            coords = new Rect2d(x, y, w, h);
            this.ID = id;
        }

        public Rect2d GetCoordinates()
        {
            return coords;
        }

        public void bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, ID);
        }

        public OGLTexture GetTextureWrapper()
        {
            return new OGLTexture(this);
        }
    }
}