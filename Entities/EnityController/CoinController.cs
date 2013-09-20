using System;
using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class CoinController : AbstractNewtonEntityController
    {
        public const double COIN_FRICTION = 0.1;

        public CoinController(CoinEntity e, Vec2d spawnForce)
            : base(e)
        {
            movementDelta = spawnForce;
        }

        public override void Update(KeyboardDevice keyboard, double ucorrection)
        {
            Vec2d delta = new Vec2d(0, 0);

            if (ent.IsOnGround())
            {
                delta.X = -Math.Sign(movementDelta.X) * Math.Min(COIN_FRICTION, Math.Abs(movementDelta.X));
            }

            DoGravitationalMovement(delta, ucorrection);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}