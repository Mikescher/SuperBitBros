using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class LevelEndWalkPlayerController : AbstractNewtonEntityController
    {
        private const double SPEED_WALK = 0.75;

        public LevelEndWalkPlayerController(Player p)
            : base(p)
        {
        }

        public override void Update(KeyboardDevice keyboard)
        {
            DoGravitationalMovement(new Vec2d(SPEED_WALK - movementDelta.X, 0));
        }

        public override bool IsActive()
        {
            return true;
        }

        public override void OnIllegalIntersection(Entity other)
        {
            //ignore
        }

        public override void OnHide()
        {
            //empty
        }

        public override void OnReshow()
        {
            //empty
        }
    }
}