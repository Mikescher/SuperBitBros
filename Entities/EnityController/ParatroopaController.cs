using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class ParatroopaController : AbstractNewtonEntityController
    {
        private const int CHANGETIME = 240;

        public const double TROOPA_ACC = 0.1;
        public const double TROOPA_SPEED = 1.5;

        private int direction = 1;

        private int timeUntilChange = CHANGETIME / 2;

        public ParatroopaController(Paratroopa p)
            : base(p)
        {

        }

        public override void Update(KeyboardDevice keyboard)
        {
            if ((direction > 0 && ent.IsCollidingRight()) || (direction < 0 && ent.IsCollidingLeft()) || timeUntilChange <= 0)
            {
                Console.Out.WriteLine("ch");
                direction *= -1;
                timeUntilChange = CHANGETIME;
            }

            if ((movementDelta.X < TROOPA_SPEED && direction > 0) || (movementDelta.X > -TROOPA_SPEED && direction < 0))
            {
                movementDelta += new Vec2d(direction * TROOPA_ACC, 0);
            }

            timeUntilChange--;
            MoveBy(movementDelta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
