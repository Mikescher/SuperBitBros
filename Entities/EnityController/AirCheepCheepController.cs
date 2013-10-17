using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class AirCheepCheepController : AbstractNewtonEntityController
    {
        private const int MAX_COOLDOWN = 600;
        public static readonly Vec2d JUMP_SPEED = new Vec2d(3.5, 11);
        public const double CHEEP_SPEED = 1.5;

        private static Random rand = new Random();
        private int cooldown = 0;
        private Vec2d startPos = null;

        public AirCheepCheepController(AirCheepCheep p)
            : base(p)
        {
            cooldown = (int)(rand.NextDouble() * MAX_COOLDOWN);
            Gravity_Acc = Gravity_Acc / 2.0;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (startPos == null)
            {
                startPos = new Vec2d(ent.position);
                startPos.Y -= Block.BLOCK_HEIGHT;
            }

            if (cooldown == -1) // Jumping
            {
                DoGravitationalMovement(Vec2d.Zero, false, false, false, false);

                if (movementDelta.Y < 0 && ent.position.Y < 0)
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


            movementDelta = new Vec2d(JUMP_SPEED * (0.65 + rand.NextDouble() * 0.5)) * new Vec2d((rand.Next() % 2) * 2 - 1, 1);
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
