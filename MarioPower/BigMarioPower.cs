
using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
namespace SuperBitBros.MarioPower
{
    public class BigMarioPower : AbstractMarioPower
    {
        public BigMarioPower()
        {

        }

        public override double GetHeightMultiplier()
        {
            return 2.0;
        }

        public override AbstractMarioPower GetSubPower()
        {
            return new StandardMarioPower();
        }

        public override AnimatedTexture GetTexture()
        {
            AnimatedTexture atexture = new AnimatedTexture();

            atexture.animation_speed = 5;

            //WALK LEFT
            atexture.Add(0, Textures.mario_big_sheet.GetTextureWrapper(2, 0));
            atexture.Add(0, Textures.mario_big_sheet.GetTextureWrapper(3, 0));
            atexture.Add(0, Textures.mario_big_sheet.GetTextureWrapper(4, 0));

            //WALK RIGHT
            atexture.Add(1, Textures.mario_big_sheet.GetTextureWrapper(2, 1));
            atexture.Add(1, Textures.mario_big_sheet.GetTextureWrapper(3, 1));
            atexture.Add(1, Textures.mario_big_sheet.GetTextureWrapper(4, 1));

            //STAND
            atexture.Add(2, Textures.mario_big_sheet.GetTextureWrapper(5, 0));
            atexture.Add(2, Textures.mario_big_sheet.GetTextureWrapper(5, 1));

            //JUMP
            atexture.Add(2, Textures.mario_big_sheet.GetTextureWrapper(0, 0));
            atexture.Add(2, Textures.mario_big_sheet.GetTextureWrapper(0, 1));

            //PIPE
            atexture.Add(3, Textures.mario_big_sheet.GetTextureWrapper(13, 0));
            atexture.Add(3, Textures.mario_big_sheet.GetTextureWrapper(13, 1));

            // SWIM LEFT
            atexture.Add(4, Textures.mario_big_sheet.GetTextureWrapper(8, 0));
            atexture.Add(4, Textures.mario_big_sheet.GetTextureWrapper(9, 0));
            atexture.Add(4, Textures.mario_big_sheet.GetTextureWrapper(10, 0));
            atexture.Add(4, Textures.mario_big_sheet.GetTextureWrapper(11, 0));

            // SWIM RIGHT
            atexture.Add(5, Textures.mario_big_sheet.GetTextureWrapper(8, 1));
            atexture.Add(5, Textures.mario_big_sheet.GetTextureWrapper(9, 1));
            atexture.Add(5, Textures.mario_big_sheet.GetTextureWrapper(10, 1));
            atexture.Add(5, Textures.mario_big_sheet.GetTextureWrapper(11, 1));

            return atexture;
        }

        public override void DoAction(Player p)
        {
            //
        }
    }
}
