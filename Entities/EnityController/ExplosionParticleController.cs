using System;
using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Particles;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class ExplosionParticleController : AbstractNewtonEntityController
    {
        public const double FRICTION = 0.1;

        public ExplosionParticleController(Particle e, Vec2d spawnForce)
            : base(e)
        {
            movementDelta = spawnForce;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            Vec2d delta = new Vec2d(0, 0);

            if (ent.IsOnGround())
            {
                delta.X = -Math.Sign(movementDelta.X) * Math.Min(FRICTION, Math.Abs(movementDelta.X));
            }

            DoGravitationalMovement(delta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}