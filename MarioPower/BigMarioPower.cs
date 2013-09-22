
using SuperBitBros.Entities;
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

            atexture.Add(0, Textures.mario_big_sheet.GetTextureWrapper(2, 0));
            atexture.Add(0, Textures.mario_big_sheet.GetTextureWrapper(3, 0));
            atexture.Add(0, Textures.mario_big_sheet.GetTextureWrapper(4, 0));

            atexture.Add(1, Textures.mario_big_sheet.GetTextureWrapper(2, 1));
            atexture.Add(1, Textures.mario_big_sheet.GetTextureWrapper(3, 1));
            atexture.Add(1, Textures.mario_big_sheet.GetTextureWrapper(4, 1));

            atexture.Add(2, Textures.mario_big_sheet.GetTextureWrapper(5, 0));
            atexture.Add(2, Textures.mario_big_sheet.GetTextureWrapper(5, 1));
            atexture.Add(2, Textures.mario_big_sheet.GetTextureWrapper(0, 0));
            atexture.Add(2, Textures.mario_big_sheet.GetTextureWrapper(0, 1));

            return atexture;
        }
    }
}
