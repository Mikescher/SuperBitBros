using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    class DefaultPlayerController : AbstractNewtonEntityController
    {
        public const double PLAYER_SPEED_FRICTION = 0.15;
        public const double PLAYER_SPEED_ACC = PLAYER_SPEED_FRICTION + 0.1;
        public const double PLAYER_SPEED_MAX = 4.5;
        public const double PLAYER_JUMP_POWER = 9;

        public DefaultPlayerController(Player e) : base(e)
        {
            //---
        }

        public override void Update(KeyboardDevice keyboard)
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

            if (keyboard[Key.Space] && ent.IsOnGround())
            {
                delta.Y = PLAYER_JUMP_POWER + GRAVITY_ACCELERATION;
            }

            if (Program.IS_DEBUG && keyboard[Key.ShiftLeft])
            {
                delta = Vec2d.Zero;
                if (keyboard[Key.Left])
                    delta.X -= PLAYER_SPEED_MAX * 2;
                if (keyboard[Key.Right])
                    delta.X += PLAYER_SPEED_MAX * 2;
                if (keyboard[Key.Space] || keyboard[Key.Up])
                    delta.Y += PLAYER_SPEED_MAX;
                if (keyboard[Key.Down])
                    delta.Y -= PLAYER_SPEED_MAX;
                MoveBy(delta, false);
            }
            else
                DoGravitationalMovement(delta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
