using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.OpenGL
{
    internal enum OGLTextureMode { TM_SINGLE, TM_REFERENCE_XY, TM_REFERENCE_POS, TM_FRAGMENT };

    public class OGLTexture
    {
        private OGLSingleTexture texSingle;

        private OGLTextureSheet texSheet;
        private int sheetX;
        private int sheetY;
        private int sheetPos;

        private OGLTextureFragment texFragment;

        private OGLTextureMode mode;

        public OGLTexture(OGLSingleTexture tex)
        {
            mode = OGLTextureMode.TM_SINGLE;
            texSingle = tex;
        }

        public OGLTexture(OGLTextureSheet tex, int x, int y)
        {
            mode = OGLTextureMode.TM_REFERENCE_XY;
            texSheet = tex;
            sheetX = x;
            sheetY = y;
        }

        public OGLTexture(OGLTextureSheet tex, int pos)
        {
            mode = OGLTextureMode.TM_REFERENCE_POS;
            texSheet = tex;
            sheetPos = pos;
        }

        public OGLTexture(OGLTextureFragment tex)
        {
            mode = OGLTextureMode.TM_FRAGMENT;
            texFragment = tex;
        }

        public static int LoadResourceIntoUID(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(filename);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return id;
        }

        public static int LoadResourceIntoUID(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentException();

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            return id;
        }

        public Rect2d GetCoordinates()
        {
            switch (mode)
            {
                case OGLTextureMode.TM_SINGLE:
                    return texSingle.GetCoordinates();

                case OGLTextureMode.TM_REFERENCE_XY:
                    return texSheet.GetCoordinates(sheetX, sheetY);

                case OGLTextureMode.TM_REFERENCE_POS:
                    return texSheet.GetCoordinates(sheetPos);

                case OGLTextureMode.TM_FRAGMENT:
                    return texFragment.GetCoordinates();

                default:
                    throw new InvalidEnumArgumentException("mode", (int)mode, typeof(OGLTextureMode));
            }
        }

        public void bind()
        {
            switch (mode)
            {
                case OGLTextureMode.TM_SINGLE:
                    texSingle.bind();
                    return;

                case OGLTextureMode.TM_REFERENCE_XY:
                case OGLTextureMode.TM_REFERENCE_POS:
                    texSheet.bind();
                    return;

                case OGLTextureMode.TM_FRAGMENT:
                    texFragment.bind();
                    return;

                default:
                    throw new InvalidEnumArgumentException("mode", (int)mode, typeof(OGLTextureMode));
            }
        }

        public int GetID()
        {
            switch (mode)
            {
                case OGLTextureMode.TM_SINGLE:
                    return texSingle.ID;

                case OGLTextureMode.TM_REFERENCE_XY:
                case OGLTextureMode.TM_REFERENCE_POS:
                    return texSheet.ID;

                case OGLTextureMode.TM_FRAGMENT:
                    return texFragment.ID;

                default:
                    throw new InvalidEnumArgumentException("mode", (int)mode, typeof(OGLTextureMode));
            }
        }
    }
}