using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class MushroomController : AbstractNewtonEntityController
    {
        protected int walkDirection = -1; // +1 ||-1 || 0

        public const double SHROOM_ACC = 0.01;
        public const double SHROOM_SPEED = 1;
        public const double SHROOM_SPAWNFORCE = 3;

        public MushroomController(MushroomEntity m)
            : base(m)
        {
            movementDelta = new Vec2d(0, SHROOM_SPAWNFORCE);
            walkDirection = (new Random().Next() % 2) * 2 - 1;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            Vec2d delta = new Vec2d(0, 0);

            if (Math.Abs(movementDelta.X) < SHROOM_SPEED)
                delta.X = walkDirection * SHROOM_ACC;

            if ((walkDirection > 0 && ent.IsCollidingRight()) || (walkDirection < 0 && ent.IsCollidingLeft()))
            {
                walkDirection *= -1;
            }

            DoGravitationalMovement(delta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
