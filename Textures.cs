using SuperBitBros.OpenGL;
using SuperBitBros.Properties;

namespace SuperBitBros
{
    public class Textures
    {
        public static OGLTextureSheet font_sheet;
        public static OGLTextureSheet mario_small_sheet;
        public static OGLTextureSheet piranhaplant_sheet;
        public static OGLTextureSheet number_sheet;

        public static OGLTexture texture_ground;
        public static OGLTexture texture_air;
        public static OGLTexture texture_ground_air;
        public static OGLTexture texture_coinblock_full;
        public static OGLTexture texture_coinblock_empty;
        public static OGLTexture texture_hill;
        public static OGLTexture texture_pipe;
        public static OGLTexture texture_castle;
        public static OGLTexture texture_flag;

        public static OGLTexture texture_coin_0;
        public static OGLTexture texture_coin_1;
        public static OGLTexture texture_coin_2;
        public static OGLTexture texture_coin_3;

        public static OGLTexture texture_goomba;
        public static OGLTexture texture_goomba_dead;

        public static void Load()
        {
            font_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.font_raster, 80, 4);
            mario_small_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.mario_small, 16, 2);
            piranhaplant_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.plant, 16, 1);
            number_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.number_raster, 16, 4);

            texture_ground = font_sheet.GetTextureWrapper(35);
            texture_air = font_sheet.GetTextureWrapper(0);
            texture_ground_air = font_sheet.GetTextureWrapper(7);
            texture_coinblock_full = font_sheet.GetTextureWrapper(88);
            texture_coinblock_empty = font_sheet.GetTextureWrapper(79);
            texture_hill = font_sheet.GetTextureWrapper(77);
            texture_pipe = font_sheet.GetTextureWrapper(72);
            texture_castle = font_sheet.GetTextureWrapper(177);
            texture_flag = font_sheet.GetTextureWrapper(179);

            texture_coin_0 = font_sheet.GetTextureWrapper(40);
            texture_coin_1 = font_sheet.GetTextureWrapper(48);
            texture_coin_2 = font_sheet.GetTextureWrapper(41);
            texture_coin_3 = font_sheet.GetTextureWrapper(124);

            texture_goomba = font_sheet.GetTextureWrapper(110);
            texture_goomba_dead = font_sheet.GetTextureWrapper(95);
        }
    }
}