using OpenTK.Input;
using System;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class HammerBowser : Bowser
    {
        private const int HAMMERCOOLDOWN = 10;
        private const int HAMMERCOUNT = 25;
        private const int SLEEPTIME = 180;

        private static Random rand = new Random();

        private int hammerCooldown = 0;
        private int hammerCount = 0;
        private int sleepTime = 0;

        public HammerBowser()
            : base()
        {

        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (sleepTime-- < 0)
            {
                if (hammerCount <= 0)
                {
                    sleepTime = (int)(SLEEPTIME * (rand.NextDouble() * 0.5 + 0.5));
                    hammerCount = HAMMERCOUNT;
                }
                else
                {
                    if (hammerCooldown-- < 0)
                    {
                        hammerCooldown = (int)(HAMMERCOOLDOWN * (rand.NextDouble() * 0.5 + 0.5));

                        owner.AddEntity(new ShootingHammerEntity(), position.X - 4, position.Y + height);
                        hammerCount--;
                    }
                }
            }
        }
    }
}
