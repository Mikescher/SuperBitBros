using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.OpenGL
{
    public class OGLTextureSheet
    {
        public int ID { get; private set; }

        private int width; // texturecount X-Axis
        private int height; // texturecount Y-Axis

        private OGLTextureSheet(int id, int w, int h)
        {
            this.width = w;
            this.height = h;
            this.ID = id;
        }

        public static OGLTextureSheet LoadTextureFromFile(string filename, int width, int height)
        {
            return new OGLTextureSheet(OGLTexture.LoadResourceIntoUID(filename), width, height);
        }

        public static OGLTextureSheet LoadTextureFromBitmap(Bitmap bmp, int width, int height)
        {
            return new OGLTextureSheet(OGLTexture.LoadResourceIntoUID(bmp), width, height);
        }

        public Rect2d GetCoordinates(int x, int y)
        {
            if (x >= width || y >= height || x < 0 || y < 0)
            {
                throw new ArgumentException(String.Format("X:{0}, Y:{1}, W:{2}, H:{3}", x, y, width, height));
            }
            double texWidth = 1.0 / width;
            double texHeight = 1.0 / height;

            Vec2d p = new Vec2d(texWidth * x, texHeight * y);

            return new Rect2d(p, texWidth, texHeight);
        }

        public Rect2d GetCoordinates(int pos)
        {
            return GetCoordinates(pos % width, pos / width);
        }

        public void bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, ID);
        }

        public OGLTexture GetTextureWrapper(int pos)
        {
            return new OGLTexture(this, pos);
        }

        public OGLTexture GetCombinedTextureWrapper(int x, int y, int w, int h)
        {
            double tw = 1.0 / width;
            double th = 1.0 / height;

            return new OGLTextureFragment(ID, x * tw, x * th, w * tw, h * th).GetTextureWrapper();
        }

        public OGLTexture GetTextureWrapper(int x, int y)
        {
            return new OGLTexture(this, x, y);
        }
    }
}