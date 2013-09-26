using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class LavaballController : AbstractNewtonEntityController
    {
        private const int MAX_COOLDOWN = 120;
        private double JUMP_POWER = 10;

        private static Random rand = new Random();
        private int cooldown = 0;
        private Vec2d startPos = null;

        public LavaballController(LavaBallEntity p)
            : base(p)
        {
            Gravity_Acc = Gravity_Acc / 2.0;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (startPos == null)
            {
                startPos = new Vec2d(ent.position);
            }

            if (cooldown == -1) // Jumping
            {
                DoGravitationalMovement(Vec2d.Zero, false, false, false, false);

                if (movementDelta.Y < 0 && ent.position.Y <= startPos.Y)
                    cooldown = (int)(rand.NextDouble() * MAX_COOLDOWN);
            }
            else if (cooldown == 0) // Start Jump
            {
                DoJump();
                cooldown = -1;
            }
            else // Waitig
            {
                cooldown--;
            }
        }

        private void DoJump()
        {
            ent.position = new Vec2d(startPos);

            movementDelta = new Vec2d(0, JUMP_POWER + rand.NextDouble() * 4 - 2);
        }

        public override void OnIllegalIntersection(Entity other)
        {
            // nothing
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
