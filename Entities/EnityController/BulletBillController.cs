using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Mobs;

namespace SuperBitBros.Entities.EnityController
{
    public class BulletBillController : AbstractNewtonEntityController
    {
        public const double SPEED = 4;

        private int direction = 1;

        public BulletBillController(BulletBill p, int dir)
            : base(p)
        {
            direction = dir;
            movementDelta.X = SPEED * direction;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if ((direction > 0 && ent.IsCollidingRight()) || (direction < 0 && ent.IsCollidingLeft()))
            {
                ent.KillLater();
            }

            MoveBy(movementDelta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
