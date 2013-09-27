using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class DefaultPlayerController : AbstractNewtonEntityController
    {
        public const double PLAYER_SPEED_FRICTION = 0.15;
        public const double PLAYER_SPEED_ACC = PLAYER_SPEED_FRICTION + 0.1;
        public const double PLAYER_SPEED_MAX = 4.5;
        public const double PLAYER_JUMP_POWER = 9;

        public const double PLAYER_MOB_KILL_JUMP = 4.5;

        public DefaultPlayerController(Player e)
            : base(e)
        {
        }

        public override void Update(KeyboardDevice keyboard)
        {
            updateMovement(keyboard);
        }

        private void updateMovement(KeyboardDevice keyboard)
        {
            Vec2d delta = Vec2d.Zero;

            delta.X = -Math.Sign(movementDelta.X) * Math.Min(PLAYER_SPEED_FRICTION, Math.Abs(movementDelta.X));

            if (!(ent as Player).IsCrouching)
            {
                if (keyboard[Key.Left] && movementDelta.X > -PLAYER_SPEED_MAX)
                {
                    delta.X -= PLAYER_SPEED_ACC;
                }

                if (keyboard[Key.Right] && movementDelta.X < PLAYER_SPEED_MAX)
                {
                    delta.X += PLAYER_SPEED_ACC;
                }

                if (keyboard[Key.Space] && ent.IsOnGround())
                {
                    if (movementDelta.Y >= 0)
                    {
                        delta.Y = Math.Max((PLAYER_JUMP_POWER + Gravity_Acc) - movementDelta.Y, 0);
                    }
                    else
                        delta.Y = PLAYER_JUMP_POWER + Gravity_Acc;
                }
            }

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
        }

        public override bool IsActive()
        {
            return true;
        }

        public void DoMobKillPushback()
        {
            if (movementDelta.Y <= 0)
            {
                movementDelta.Y = PLAYER_MOB_KILL_JUMP;
            }
        }
    }
}