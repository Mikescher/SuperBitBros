using OpenTK.Graphics.OpenGL;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.OpenGL {
    class OGLSingleTexture {
        private int id;

        private OGLSingleTexture(int id) {
            this.id = id;
        }

        public static OGLSingleTexture LoadTextureFromFile(string filename) {
            return new OGLSingleTexture(OGLTexture.LoadResourceIntoUID(filename));
        }

        public static OGLSingleTexture LoadTextureFromBitmap(Bitmap bmp) {
            return new OGLSingleTexture(OGLTexture.LoadResourceIntoUID(bmp));
        }

        public void bind() {
            GL.BindTexture(TextureTarget.Texture2D, id);
        }

        public Rect2d GetCoordinates() {
            return new Rect2d(new Vec2d(0, 0), 1);
        }

        public OGLTexture GetTextureWrapper() {
            return new OGLTexture(this);
        }
    }
}
