
using OpenTK.Input;
using SuperBitBros.OpenGL.OGLMath;
using System;
namespace SuperBitBros.Entities {
    class GravityCoinEntity : CoinEntity {
        public const double COIN_FRICTION = 0.1;

        private double spawnForce;
        private bool isBouncing;

        public GravityCoinEntity(double cSpawnForce = 0, bool cIsBounce = false)
            : base() {
            this.spawnForce = cSpawnForce;
            this.isBouncing = cIsBounce;
        }

        public override void OnAdd(GameModel owner) {
            base.OnAdd(owner);
            movementDelta.Y = spawnForce;
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            Vec2d delta = new Vec2d(0, 0);

            if (IsOnGround()) {
                delta.X = -Math.Sign(movementDelta.X) * Math.Min(COIN_FRICTION, Math.Abs(movementDelta.X));
            }

            if (isBouncing) {
                DoGravitationalMovement(delta, true, false);

                if (IsOnCeiling())
                    movementDelta.Y = Math.Min(movementDelta.Y, 0);
                if (IsOnGround()) {
                    if (movementDelta.Y < 0.25)
                        movementDelta.Y = -movementDelta.Y * (2 / 3.0);
                    else
                        movementDelta.Y = Math.Max(movementDelta.Y, 0);
                }

            } else {
                DoGravitationalMovement(delta);
            }
        }
    }
}
