using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class LakituController : AbstractNewtonEntityController
    {
        public const double ACC = 0.01;
        public const double SPEED = 1.5;

        public int direction = -1;

        private bool IsInit = false;
        private double minPos;
        private double maxPos;

        public LakituController(Lakitu p)
            : base(p)
        {

        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (!IsInit)
            {
                minPos = ent.position.X - Block.BLOCK_WIDTH * 40;
                maxPos = ent.position.X + Block.BLOCK_WIDTH * 40;
                IsInit = true;
            }

            if ((direction > 0 && ent.IsCollidingRight(typeof(ShootingSpikeBallController))) || (direction < 0 && ent.IsCollidingLeft(typeof(ShootingSpikeBallController))) || (direction > 0 && ent.position.X >= maxPos) || (direction < 0 && ent.position.X <= minPos))
            {
                direction *= -1;
            }

            if ((movementDelta.X < SPEED && direction > 0) || (movementDelta.X > -SPEED && direction < 0))
            {
                movementDelta += new Vec2d(direction * ACC, 0);
            }

            MoveBy(movementDelta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
