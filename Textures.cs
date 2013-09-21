using SuperBitBros.OpenGL;
using SuperBitBros.Properties;

namespace SuperBitBros
{
    public class Textures
    {
        public static OGLTextureSheet block_textures;
        public static OGLTextureSheet mario_small_sheet;
        public static OGLTextureSheet piranhaplant_sheet;
        public static OGLTextureSheet number_sheet;

        public static OGLTexture[] array_coin;

        public static OGLTexture texture_ground;
        public static OGLTexture texture_air;
        public static OGLTexture texture_ground_air;
        public static OGLTexture texture_coinblock_full;
        public static OGLTexture texture_coinblock_empty;
        public static OGLTexture texture_hill;
        public static OGLTexture texture_pipe;
        public static OGLTexture texture_castle;
        public static OGLTexture texture_flag;
        public static OGLTexture texture_darkGround;
        public static OGLTexture texture_darkCeiling;
        public static OGLTexture texture_darkHill;

        public static OGLTexture texture_coin_0;
        public static OGLTexture texture_coin_1;
        public static OGLTexture texture_coin_2;
        public static OGLTexture texture_coin_3;

        public static OGLTexture texture_goomba;
        public static OGLTexture texture_goomba_dead;
        public static OGLTexture texture_koopa;
        public static OGLTexture texture_koopashell;

        public static void Load()
        {
            block_textures = OGLTextureSheet.LoadTextureFromBitmap(Resources.block_textures, 80, 4);
            mario_small_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.mario_small, 16, 2);
            piranhaplant_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.plant, 16, 1);
            number_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.number_raster, 16, 4);

            texture_ground = block_textures.GetTextureWrapper(35);
            texture_air = block_textures.GetTextureWrapper(0);
            texture_ground_air = block_textures.GetTextureWrapper(7);
            texture_coinblock_full = block_textures.GetTextureWrapper(88);
            texture_coinblock_empty = block_textures.GetTextureWrapper(79);
            texture_hill = block_textures.GetTextureWrapper(77);
            texture_pipe = block_textures.GetTextureWrapper(72);
            texture_castle = block_textures.GetTextureWrapper(177);
            texture_flag = block_textures.GetTextureWrapper(179);

            texture_darkGround = block_textures.GetTextureWrapper(178);
            texture_darkCeiling = block_textures.GetTextureWrapper(4);
            texture_darkHill = block_textures.GetTextureWrapper(255);

            texture_coin_0 = block_textures.GetTextureWrapper(40);
            texture_coin_1 = block_textures.GetTextureWrapper(48);
            texture_coin_2 = block_textures.GetTextureWrapper(41);
            texture_coin_3 = block_textures.GetTextureWrapper(124);

            texture_goomba = block_textures.GetTextureWrapper(110);
            texture_goomba_dead = block_textures.GetTextureWrapper(95);
            texture_koopa = block_textures.GetTextureWrapper(109);
            texture_koopashell = block_textures.GetTextureWrapper(127);

            array_coin = new OGLTexture[] { texture_coin_0, texture_coin_1, texture_coin_2, texture_coin_3 };
        }
    }
}