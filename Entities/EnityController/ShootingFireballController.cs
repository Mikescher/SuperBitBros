using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    class ShootingFireballController : AbstractNewtonEntityController
    {
        private int direction;
        public const double X_SPEED = 5;
        public const double Y_SPEED = 6;

        public ShootingFireballController(ShootingFireballEntity e, int d)
            : base(e)
        {
            direction = d;
            movementDelta.X = direction * X_SPEED;
            movementDelta.Y = 0;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            DoGravitationalMovement(Vec2d.Zero);

            if (ent.IsOnCeiling())
                movementDelta.Y = Math.Min(movementDelta.Y, 0);

            if (ent.IsOnGround())
                movementDelta.Y = Y_SPEED;

            if ((direction == 1 && ent.IsCollidingRight()) || (direction == -1 && ent.IsCollidingLeft()))
            {
                ent.KillLater();
            }

        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
