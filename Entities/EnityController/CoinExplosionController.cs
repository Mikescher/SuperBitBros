﻿using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Particles;
using SuperBitBros.HUD;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    class CoinExplosionController : AbstractEntityController
    {
        private const double SEEK_SPEED = 0.5;
        private const double MAX_SPEED = 3.5;

        private const double KILL_DISTANCE = Block.BLOCK_WIDTH;

        private Vec2d target;
        private OffsetCalculator offset;
        private StandardGameHUD hud;

        public Vec2d movementDelta = Vec2d.Zero;

        public CoinExplosionController(CoinExplosionParticle e, Vec2d spawnForce, StandardGameHUD pHud, OffsetCalculator pOffset)
            : base(e)
        {
            hud = pHud;
            movementDelta = spawnForce;
            target = pHud.GetCoinTarget();
            offset = pOffset;
        }

        public override void OnHide()
        {
            //
        }

        public override void OnReshow()
        {
            //
        }

        public override void Update(KeyboardDevice keyboard)
        {
            Vec2d rtarg = (offset.Value + target);
            Vec2d delta = rtarg - ent.position;
            delta.SetLength(SEEK_SPEED);

            MoveBy(delta);

            if ((rtarg - ent.position).GetLength() < KILL_DISTANCE)
            {
                hud.AddExplosionParticle(ent as CoinExplosionParticle);
                ent.KillLater();
            }
        }

        private void MoveBy(Vec2d delta)
        {
            movementDelta += delta;

            ent.position += movementDelta;

            movementDelta.DoMaxLength(MAX_SPEED);

            //ent.DoCollisions(); // No Collisions with C-Particles
        }

        public override void OnIllegalIntersection(Entity other)
        {
            //
        }

        public override Vec2d GetDelta()
        {
            return movementDelta;
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
