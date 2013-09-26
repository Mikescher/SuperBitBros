using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class BlooperController : UnderwaterNewtonEntityController
    {
        public const double BLOOPER_JUMP_POWER = 8;

        private int jumpcooldown = 30;

        private double startY = int.MinValue;

        public BlooperController(Blooper e)
            : base(e)
        {
            Gravity_Max = 1.5;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (startY == int.MinValue)
                startY = ent.position.Y + Block.BLOCK_HEIGHT;

            updateMovement(keyboard);
        }

        private void updateMovement(KeyboardDevice keyboard)
        {
            Vec2d delta = Vec2d.Zero;

            if (jumpcooldown > 0)
                jumpcooldown--;
            if (ent.position.Y <= startY && jumpcooldown <= 0)
            {
                jumpcooldown = 30;
                delta.Y = BLOOPER_JUMP_POWER;
            }

            DoGravitationalMovement(delta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
