using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Particles;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class SimpleExplosionController : AbstractEntityController
    {
        private Vec2d movementDelta;

        public SimpleExplosionController(Particle e, Vec2d spawnForce)
            : base(e)
        {
            movementDelta = spawnForce;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            ent.position += movementDelta;
        }

        public override bool IsActive()
        {
            return true;
        }


        public override void OnIllegalIntersection(Entity other)
        {
            //
        }

        public override void OnHide()
        {
            //
        }

        public override void OnReshow()
        {
            //
        }

        public override Vec2d GetDelta()
        {
            return movementDelta;
        }
    }
}
