using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class CheepCheepController : AbstractNewtonEntityController
    {
        public const double CHEEP_ACC = 0.1;
        public const double CHEEP_SPEED = 1.5;

        private int direction = -1;

        public CheepCheepController(CheepCheep p)
            : base(p)
        {

        }

        public override void Update(KeyboardDevice keyboard)
        {
            if ((direction > 0 && ent.IsCollidingRight()) || (direction < 0 && ent.IsCollidingLeft()))
            {
                direction *= -1;
            }

            if ((movementDelta.X < CHEEP_SPEED && direction > 0) || (movementDelta.X > -CHEEP_SPEED && direction < 0))
            {
                movementDelta += new Vec2d(direction * CHEEP_ACC, 0);
            }

            MoveBy(movementDelta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
