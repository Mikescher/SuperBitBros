
namespace SuperBitBros.Entities.EnityController
{
    public abstract class UnderwaterNewtonEntityController : AbstractNewtonEntityController
    {
        public const double UNDERWATER_GRAVITY_ACCELERATION = 0.15;
        public const double UNDERWATER_GRAVITY_MAX = 3.5;

        public UnderwaterNewtonEntityController(DynamicEntity e)
            : base(e)
        {
            Gravity_Acc = UNDERWATER_GRAVITY_ACCELERATION;
            Gravity_Max = UNDERWATER_GRAVITY_MAX;
        }
    }
}
