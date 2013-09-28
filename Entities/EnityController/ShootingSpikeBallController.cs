using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    class ShootingSpikeBallController : AbstractNewtonEntityController
    {
        public const double X_SPEED = 2.25;
        public const double Y_SPEED = 2;

        public ShootingSpikeBallController(ShootingSpikeBallEntity e, int direction)
            : base(e)
        {
            movementDelta.X = X_SPEED * direction;
            movementDelta.Y = Y_SPEED;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if ((ent as ShootingSpikeBallEntity).IsDesintegrated())
            {
                DoGravitationalMovement(Vec2d.Zero, false, false, false, false);
            }
            else
            {
                if (ent.IsOnGround())
                    movementDelta.Y = movementDelta.Y * -0.5;
                DoGravitationalMovement(Vec2d.Zero);
            }

            if (ent.position.Y < 0)
                ent.KillLater();

        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
