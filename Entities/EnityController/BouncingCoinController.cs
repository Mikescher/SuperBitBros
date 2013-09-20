using System;
using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class BouncingCoinController : CoinController
    {
        public BouncingCoinController(CoinEntity e, Vec2d spawnForce)
            : base(e, spawnForce)
        {
            //--
        }

        public override void Update(KeyboardDevice keyboard, double ucorrection)
        {
            base.Update(keyboard, ucorrection);

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