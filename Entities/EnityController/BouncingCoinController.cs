using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    class BouncingCoinController : CoinController
    {
        public BouncingCoinController(CoinEntity e, Vec2d spawnForce)
            : base(e, spawnForce)
        {
            //--
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (ent.IsOnCeiling())
                movementDelta.Y = Math.Min(movementDelta.Y, 0);
            if (ent.IsOnGround())
            {
                if (movementDelta.Y < 0.25)
                    movementDelta.Y = -movementDelta.Y * (2 / 3.0);
                else
                    movementDelta.Y = Math.Max(movementDelta.Y, 0);
            }
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
