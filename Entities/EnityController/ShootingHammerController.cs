using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    class ShootingHammerController : AbstractNewtonEntityController
    {
        public const double X_SPEED = -2.25;
        public const double Y_SPEED = 8;

        public ShootingHammerController(ShootingHammer e)
            : base(e)
        {
            movementDelta.X = X_SPEED;
            movementDelta.Y = Y_SPEED;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            DoGravitationalMovement(Vec2d.Zero, false, false, false, false);

            if (ent.position.Y < 0)
                ent.KillLater();

        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
