
using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
namespace SuperBitBros.MarioPower
{
    public class ShootingMarioPower : AbstractMarioPower
    {
        private const int FIRE_COOLDOWN = 15;

        private int fireCooldown = 0;

        public ShootingMarioPower()
        {

        }

        public override double GetHeightMultiplier()
        {
            return 2.0;
        }

        public override AbstractMarioPower GetSubPower()
        {
            return new BigMarioPower();
        }

        public override AnimatedTexture GetTexture()
        {
            AnimatedTexture atexture = new AnimatedTexture();

            atexture.animation_speed = 5;

            //WALK LEFT
            atexture.Add(0, Textures.mario_fire_sheet.GetTextureWrapper(2, 0));
            atexture.Add(0, Textures.mario_fire_sheet.GetTextureWrapper(3, 0));
            atexture.Add(0, Textures.mario_fire_sheet.GetTextureWrapper(4, 0));

            //WALK RIGHT
            atexture.Add(1, Textures.mario_fire_sheet.GetTextureWrapper(2, 1));
            atexture.Add(1, Textures.mario_fire_sheet.GetTextureWrapper(3, 1));
            atexture.Add(1, Textures.mario_fire_sheet.GetTextureWrapper(4, 1));

            //STAND
            atexture.Add(2, Textures.mario_fire_sheet.GetTextureWrapper(5, 0));
            atexture.Add(2, Textures.mario_fire_sheet.GetTextureWrapper(5, 1));

            //JUMP
            atexture.Add(2, Textures.mario_fire_sheet.GetTextureWrapper(0, 0));
            atexture.Add(2, Textures.mario_fire_sheet.GetTextureWrapper(0, 1));

            //PIPE
            atexture.Add(3, Textures.mario_fire_sheet.GetTextureWrapper(13, 0));
            atexture.Add(3, Textures.mario_fire_sheet.GetTextureWrapper(13, 1));

            // SWIM LEFT
            atexture.Add(4, Textures.mario_fire_sheet.GetTextureWrapper(8, 0));
            atexture.Add(4, Textures.mario_fire_sheet.GetTextureWrapper(9, 0));
            atexture.Add(4, Textures.mario_fire_sheet.GetTextureWrapper(10, 0));
            atexture.Add(4, Textures.mario_fire_sheet.GetTextureWrapper(11, 0));

            // SWIM RIGHT
            atexture.Add(5, Textures.mario_fire_sheet.GetTextureWrapper(8, 1));
            atexture.Add(5, Textures.mario_fire_sheet.GetTextureWrapper(9, 1));
            atexture.Add(5, Textures.mario_fire_sheet.GetTextureWrapper(10, 1));
            atexture.Add(5, Textures.mario_fire_sheet.GetTextureWrapper(11, 1));

            return atexture;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (fireCooldown > 0)
                fireCooldown--;
        }

        public override void DoAction(Player p)
        {
            if (fireCooldown == 0)
            {
                if (p.direction == Direction.LEFT)
                {
                    p.owner.TestAddEntity(new ShootingFireballEntity(-1), p.position.X - 5, p.position.Y + p.height / 2.0);
                }
                else if (p.direction == Direction.RIGHT)
                {
                    p.owner.TestAddEntity(new ShootingFireballEntity(1), p.position.X + p.width + 5, p.position.Y + p.height / 2.0);
                }

                fireCooldown = FIRE_COOLDOWN;
            }
        }
    }
}
