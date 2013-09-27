using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class JumpingParatroopaController : DefaultMobController
    {
        private const int JUMP_PROBABILITY = 60;
        private const int JUMP_POWER = 5;

        private static Random rand = new Random();

        public JumpingParatroopaController(JumpingParatroopa p)
            : base(p)
        {
            Gravity_Acc *= 0.5;
            Gravity_Max *= 0.5;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (rand.Next() % JUMP_PROBABILITY == 0 && ent.IsOnGround())
            {
                movementDelta.Y = JUMP_POWER + (rand.NextDouble() * 6 - 3);
            }

        }
    }
}
