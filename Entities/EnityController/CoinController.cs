using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    class CoinController : AbstractNewtonEntityController
    {
        public const double COIN_FRICTION = 0.1;

        public CoinController(CoinEntity e, Vec2d spawnForce)
            : base(e)
        {
            movementDelta = spawnForce;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            Vec2d delta = new Vec2d(0, 0);

            if (ent.IsOnGround())
            {
                delta.X = -Math.Sign(movementDelta.X) * Math.Min(COIN_FRICTION, Math.Abs(movementDelta.X));
            }

            DoGravitationalMovement(delta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
