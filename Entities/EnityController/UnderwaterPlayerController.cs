using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class UnderwaterPlayerController : UnderwaterNewtonEntityController
    {
        public const int SWIM_UP_COOLDOWN = 15;

        public const double PLAYER_SPEED_FRICTION = 0.15;
        public const double PLAYER_SPEED_ACC = PLAYER_SPEED_FRICTION + 0.1;
        public const double PLAYER_SPEED_MAX = 2.5;
        public const double PLAYER_JUMP_POWER = 5;

        private int canSwimUp = 0;
        private bool lastSpaceState = false;

        private int is_active = 5;

        public UnderwaterPlayerController(Player e)
            : base(e)
        {
        }

        public override void Update(KeyboardDevice keyboard)
        {
            updateMovement(keyboard);

            if (canSwimUp > 0)
                canSwimUp--;
        }

        private void updateMovement(KeyboardDevice keyboard)
        {
            Vec2d delta = Vec2d.Zero;

            delta.X = -Math.Sign(movementDelta.X) * Math.Min(PLAYER_SPEED_FRICTION, Math.Abs(movementDelta.X));

            if (keyboard[Key.Left] && movementDelta.X > -PLAYER_SPEED_MAX)
            {
                delta.X -= PLAYER_SPEED_ACC;
            }

            if (keyboard[Key.Right] && movementDelta.X < PLAYER_SPEED_MAX)
            {
                delta.X += PLAYER_SPEED_ACC;
            }

            if (keyboard[Key.Space] && !lastSpaceState && canSwimUp == 0)
            {
                delta.Y = PLAYER_JUMP_POWER + Gravity_Acc;
                canSwimUp = SWIM_UP_COOLDOWN;
            }
            lastSpaceState = keyboard[Key.Space];

            if (Program.debugViewSwitch.Value && Program.debugFlySwitch.Value)
            {
                delta = Vec2d.Zero;
                if (keyboard[Key.Left])
                    delta.X -= PLAYER_SPEED_MAX * 3;
                if (keyboard[Key.Right])
                    delta.X += PLAYER_SPEED_MAX * 3;
                if (keyboard[Key.Space] || keyboard[Key.Up])
                    delta.Y += PLAYER_SPEED_MAX * 2;
                if (keyboard[Key.Down])
                    delta.Y -= PLAYER_SPEED_MAX * 2;
                MoveBy(delta, !Program.debugFlyoverrideSwitch.Value, !Program.debugFlyoverrideSwitch.Value);
            }
            else
            {
                if ((ent.position.X <= 0 && delta.X < 0) || ((ent.position.X + ent.width) >= ent.owner.mapRealWidth && delta.X > 0))
                {
                    delta.X = 0;
                    movementDelta.X = 0;
                }

                DoGravitationalMovement(delta);
            }

            if (!(owner.GetBlock((Vec2i)(ent.position / Block.BLOCK_SIZE)) is WaterBlock))
                is_active--;
            else
                is_active = 5;
        }

        public override bool IsActive()
        {
            return is_active > 0;
        }
    }
}
