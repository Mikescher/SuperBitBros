using OpenTK.Graphics.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.OpenGL
{
    public class OGLTextureFragment
    {
        private int texID;

        public Rect2d coords { get; private set; }

        public OGLTextureFragment(int id, double x, double y, double w, double h)
        {
            coords = new Rect2d(x, y, w, h);
            this.texID = id;
        }

        public virtual int GetID()
        {
            return texID;
        }

        public Rect2d GetCoordinates()
        {
            return coords;
        }

        public void bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, GetID());
        }

        public OGLTexture GetTextureWrapper()
        {
            return new OGLTexture(this);
        }
    }
}