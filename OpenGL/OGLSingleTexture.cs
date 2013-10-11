using OpenTK.Graphics.OpenGL;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.OpenGL
{
    public class OGLSingleTexture
    {
        private int texID;

        private OGLSingleTexture(int id)
        {
            this.texID = id;
        }

        public virtual int GetID()
        {
            return texID;
        }

        public static OGLSingleTexture LoadTextureFromFile(string filename)
        {
            return new OGLSingleTexture(OGLTexture.LoadResourceIntoUID(filename));
        }

        public static OGLSingleTexture LoadTextureFromBitmap(Bitmap bmp)
        {
            return new OGLSingleTexture(OGLTexture.LoadResourceIntoUID(bmp));
        }

        public void bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, GetID());
        }

        public Rect2d GetCoordinates()
        {
            return new Rect2d(new Vec2d(0, 0), 1);
        }

        public OGLTexture GetTextureWrapper()
        {
            return new OGLTexture(this);
        }
    }
}