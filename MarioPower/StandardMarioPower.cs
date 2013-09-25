
using SuperBitBros.Entities;
namespace SuperBitBros.MarioPower
{
    public class StandardMarioPower : AbstractMarioPower
    {
        public StandardMarioPower()
        {

        }

        public override double GetHeightMultiplier()
        {
            return 1.0;
        }

        public override AbstractMarioPower GetSubPower()
        {
            return null;
        }

        public override AnimatedTexture GetTexture()
        {
            AnimatedTexture atexture = new AnimatedTexture();

            atexture.animation_speed = 5;

            // WALK LEFT
            atexture.Add(0, Textures.mario_small_sheet.GetTextureWrapper(2, 0));
            atexture.Add(0, Textures.mario_small_sheet.GetTextureWrapper(3, 0));
            atexture.Add(0, Textures.mario_small_sheet.GetTextureWrapper(4, 0));

            // WALK RIGHT
            atexture.Add(1, Textures.mario_small_sheet.GetTextureWrapper(2, 1));
            atexture.Add(1, Textures.mario_small_sheet.GetTextureWrapper(3, 1));
            atexture.Add(1, Textures.mario_small_sheet.GetTextureWrapper(4, 1));

            // STAND
            atexture.Add(2, Textures.mario_small_sheet.GetTextureWrapper(5, 0));
            atexture.Add(2, Textures.mario_small_sheet.GetTextureWrapper(5, 1));

            //JUMP
            atexture.Add(2, Textures.mario_small_sheet.GetTextureWrapper(0, 0));
            atexture.Add(2, Textures.mario_small_sheet.GetTextureWrapper(0, 1));

            //PIPE
            atexture.Add(3, Textures.mario_small_sheet.GetTextureWrapper(13, 0));
            atexture.Add(3, Textures.mario_small_sheet.GetTextureWrapper(13, 1));

            return atexture;
        }
    }
}
