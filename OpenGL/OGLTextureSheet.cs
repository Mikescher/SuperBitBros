using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace SuperBitBros.OpenGL {
    class OGLTextureSheet {
        private int id;

        private int width; // texturecount X-Axis
        private int height; // texturecount Y-Axis

        private OGLTextureSheet(int id, int w, int h) {
            this.width = w;
            this.height = h;
            this.id = id;
        }

        public static OGLTextureSheet LoadTextureFromFile(string filename, int width, int height) {
            return new OGLTextureSheet(OGLTexture.LoadResourceIntoUID(filename), width, height);
        }

        public static OGLTextureSheet LoadTextureFromBitmap(Bitmap bmp, int width, int height) {
            return new OGLTextureSheet(OGLTexture.LoadResourceIntoUID(bmp), width, height);
        }

        public Rectangle2d GetCoordinates(int x, int y) {
            if (x >= width || y >= height || x < 0 || y < 0) {
                throw new ArgumentException(String.Format("X:{0}, Y:{1}, W:{2}, H:{3}", x, y, width, height));
            }
            double texWidth = 1.0 / width;
            double texHeight = 1.0 / height;

            Vector2d p = new Vector2d(texWidth * x, texHeight * y);

            return new Rectangle2d(p, texWidth, texHeight);
        }

        public Rectangle2d GetCoordinates(int pos) {
            return GetCoordinates(pos % width, pos / width);
        }

        public void bind() {
            GL.BindTexture(TextureTarget.Texture2D, id);
        }

        public OGLTexture GetTextureWrapper(int pos) {
            return new OGLTexture(this, pos);
        }

        public OGLTexture GetTextureWrapper(int x, int y) {
            return new OGLTexture(this, x, y);
        }
    }
}
