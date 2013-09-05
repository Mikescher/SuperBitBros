using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using SuperBitBros.OpenGL;
using SuperBitBros.Properties;

namespace SuperBitBros
{
    class Textures
    {
        public static OGLTextureSheet font_sheet;
        public static OGLTextureSheet mario_small_sheet;

        public static OGLTexture texture_ground;
        public static OGLTexture texture_air;
        public static OGLTexture texture_coinblock_full;
        public static OGLTexture texture_coinblock_empty;

        public static OGLTexture texture_hill;
        public static OGLTexture texture_pipe;
        public static OGLTexture texture_castle;
        public static OGLTexture texture_flag;

        public static void Load() {
            font_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.font_raster, 80, 4);
            mario_small_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.mario_small, 13, 2);

            texture_ground =            font_sheet.GetTextureWrapper(35);
            texture_air =               font_sheet.GetTextureWrapper(0);
            texture_coinblock_full =    font_sheet.GetTextureWrapper(88);
            texture_coinblock_empty =   font_sheet.GetTextureWrapper(79);

            texture_hill =              font_sheet.GetTextureWrapper(77);
            texture_pipe =              font_sheet.GetTextureWrapper(72);
            texture_castle =            font_sheet.GetTextureWrapper(177);
            texture_flag =              font_sheet.GetTextureWrapper(179);
        }
    }
}
