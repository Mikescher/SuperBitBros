using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class BeanStalkPlayerController : AbstractNewtonEntityController
    {
        private const double SPEED_VERTICAL = 3.5;
        private const double SPEED_HORIZONTAL = 1.5;

        public BeanStalkPlayerController(Player p)
            : base(p)
        {

        }

        public override void Update(KeyboardDevice keyboard)
        {
            Vec2d delta = Vec2d.Zero;

            if (keyboard[Key.Left])
                delta.X -= SPEED_HORIZONTAL;
            if (keyboard[Key.Right])
                delta.X += SPEED_HORIZONTAL;
            if (keyboard[Key.Space] || keyboard[Key.Up])
                delta.Y += SPEED_VERTICAL;
            if (keyboard[Key.Down])
                delta.Y -= SPEED_VERTICAL;


            MoveBy(delta);

        }

        public override bool IsActive()
        {
            return false; // instant deactivate
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