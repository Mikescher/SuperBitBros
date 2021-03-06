﻿using SuperBitBros.OpenGL;
using SuperBitBros.Properties;

namespace SuperBitBros
{
    public class Textures
    {
        public static OGLTextureSheet[] texturePacks = new OGLTextureSheet[3];

        public static OGLReferenceTextureSheet block_textures;
        public static OGLReferenceTextureSheet doubleblock_sheet;
        public static OGLReferenceTextureSheet doubleheight_sheet;

        public static OGLTextureSheet number_sheet;

        public static OGLTextureSheet mario_small_sheet;
        public static OGLTextureSheet mario_big_sheet;
        public static OGLTextureSheet mario_fire_sheet;

        public static OGLTexture[,] texture_logo;

        public static OGLTexture[] array_coin;

        public static OGLTexture texture_ground;
        public static OGLTexture texture_air;
        public static OGLTexture texture_ground_air;
        public static OGLTexture texture_ceiling;
        public static OGLTexture texture_pillar;
        public static OGLTexture texture_coinblock_full;
        public static OGLTexture texture_coinblock_empty;
        public static OGLTexture texture_hill;
        public static OGLTexture texture_pipe;
        public static OGLTexture texture_castle;
        public static OGLTexture texture_darkGround;
        public static OGLTexture texture_darkCeiling;
        public static OGLTexture texture_darkHill;
        public static OGLTexture texture_mshroomplatform;
        public static OGLTexture texture_castleGround;
        public static OGLTexture texture_lava;
        public static OGLTexture texture_solidcloud;
        public static OGLTexture texture_underwater_water;
        public static OGLTexture texture_underwater_ground;
        public static OGLTexture texture_billblaster;

        public static OGLTexture texture_beanstalk;
        public static OGLTexture texture_bridge;
        public static OGLTexture texture_lever;
        public static OGLTexture texture_flag;
        public static OGLTexture texture_trampoline;
        public static OGLTexture texture_largetrampoline;

        public static OGLTexture texture_coin_0;
        public static OGLTexture texture_coin_1;
        public static OGLTexture texture_coin_2;
        public static OGLTexture texture_coin_3;
        public static OGLTexture texture_mariohead;

        public static OGLTexture texture_mushroom;
        public static OGLTexture texture_flower;

        public static OGLTexture texture_fireball;
        public static OGLTexture texture_lavaball;
        public static OGLTexture texture_hammer;
        public static OGLTexture texture_spikeball;

        public static OGLTexture texture_goomba;
        public static OGLTexture texture_goomba_dead;
        public static OGLTexture texture_koopa;
        public static OGLTexture texture_koopashell;
        public static OGLTexture texture_paratroopa;
        public static OGLTexture texture_bowser;
        public static OGLTexture texture_toad;
        public static OGLTexture texture_cheepcheep;
        public static OGLTexture texture_blooper;
        public static OGLTexture texture_hammerbro;
        public static OGLTexture texture_lakitu;
        public static OGLTexture texture_buzzybeetle;
        public static OGLTexture texture_buzzybeetleshell;
        public static OGLTexture texture_bulletbill;

        public static void Load()
        {
            for (int i = 0; i < texturePacks.Length; i++)
            {
                texturePacks[i] = OGLTextureSheet.LoadTextureFromBitmap(ResourceAccessor.GetTexturePack(i), 80, 8);
            }

            block_textures = OGLReferenceTextureSheet.LoadTextureFromReference(texturePacks[Program.TEXTUREPACK], 80, 8);
            doubleblock_sheet = OGLReferenceTextureSheet.LoadTextureFromReference(block_textures, 40, 4);
            doubleheight_sheet = OGLReferenceTextureSheet.LoadTextureFromReference(block_textures, 80, 4);

            number_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.number_raster, 16, 4);

            mario_small_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.mario_small, 16, 2);
            mario_big_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.mario_big, 16, 2);
            mario_fire_sheet = OGLTextureSheet.LoadTextureFromBitmap(Resources.mario_fire, 16, 2);

            texture_logo = new OGLTexture[15, 5];
            for (int x = 0; x < 15; x++)
                for (int y = 0; y < 5; y++)
                    texture_logo[x, y] = block_textures.GetTextureWrapper(65 + x, 7 - y);

            texture_ground = block_textures.GetTextureWrapper(35);
            texture_air = block_textures.GetTextureWrapper(0);
            texture_ground_air = block_textures.GetTextureWrapper(7);
            texture_coinblock_full = block_textures.GetTextureWrapper(88);
            texture_coinblock_empty = block_textures.GetTextureWrapper(79);
            texture_hill = block_textures.GetTextureWrapper(77);
            texture_pipe = block_textures.GetTextureWrapper(72);
            texture_castle = block_textures.GetTextureWrapper(177);
            texture_ceiling = block_textures.GetTextureWrapper(256);
            texture_pillar = block_textures.GetTextureWrapper(176);
            texture_mshroomplatform = block_textures.GetTextureWrapper(84);
            texture_castleGround = block_textures.GetTextureWrapper(206);
            texture_lava = block_textures.GetTextureWrapper(94);
            texture_solidcloud = block_textures.GetTextureWrapper(1);
            texture_underwater_water = block_textures.GetTextureWrapper(263);
            texture_underwater_ground = block_textures.GetTextureWrapper(262);
            texture_darkGround = block_textures.GetTextureWrapper(178);
            texture_darkCeiling = block_textures.GetTextureWrapper(4);
            texture_darkHill = block_textures.GetTextureWrapper(255);
            texture_billblaster = block_textures.GetTextureWrapper(220);

            texture_flag = block_textures.GetTextureWrapper(179);
            texture_bridge = block_textures.GetTextureWrapper(205);
            texture_lever = block_textures.GetTextureWrapper(11);
            texture_beanstalk = block_textures.GetTextureWrapper(21);
            texture_trampoline = block_textures.GetTextureWrapper(23);
            texture_largetrampoline = doubleheight_sheet.GetTextureWrapper(24);

            texture_coin_0 = block_textures.GetTextureWrapper(40);
            texture_coin_1 = block_textures.GetTextureWrapper(48);
            texture_coin_2 = block_textures.GetTextureWrapper(41);
            texture_coin_3 = block_textures.GetTextureWrapper(124);
            texture_mariohead = block_textures.GetTextureWrapper(261);

            texture_mushroom = block_textures.GetTextureWrapper(6);
            texture_flower = block_textures.GetTextureWrapper(12);

            texture_fireball = block_textures.GetTextureWrapper(248);
            texture_lavaball = block_textures.GetTextureWrapper(264);
            texture_hammer = block_textures.GetTextureWrapper(15);
            texture_spikeball = block_textures.GetTextureWrapper(265);

            texture_goomba = block_textures.GetTextureWrapper(110);
            texture_goomba_dead = block_textures.GetTextureWrapper(95);
            texture_koopa = block_textures.GetTextureWrapper(109);
            texture_koopashell = block_textures.GetTextureWrapper(127);
            texture_paratroopa = block_textures.GetTextureWrapper(257);
            texture_toad = block_textures.GetTextureWrapper(258);
            texture_bowser = block_textures.GetCombinedTextureWrapper(19, 3, 2, 1);
            texture_cheepcheep = block_textures.GetTextureWrapper(29);
            texture_blooper = block_textures.GetTextureWrapper(30);
            texture_hammerbro = block_textures.GetTextureWrapper(190);
            texture_lakitu = block_textures.GetTextureWrapper(167);
            texture_buzzybeetle = block_textures.GetTextureWrapper(266);
            texture_buzzybeetleshell = block_textures.GetTextureWrapper(267);
            texture_bulletbill = block_textures.GetTextureWrapper(61);

            array_coin = new OGLTexture[] { texture_coin_0, texture_coin_1, texture_coin_2, texture_coin_3 };
        }
    }
}