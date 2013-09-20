using System;
using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    public class DefaultPlayerController : AbstractNewtonEntityController
    {
        public const double PLAYER_SPEED_FRICTION = 0.15;
        public const double PLAYER_SPEED_ACC = PLAYER_SPEED_FRICTION + 0.1;
        public const double PLAYER_SPEED_MAX = 4.5;
        public const double PLAYER_JUMP_POWER = 9;

        public const double PLAYER_MOB_KILL_JUMP = 4.5;

        private BooleanKeySwitch debugFlySwitch = new BooleanKeySwitch(false, Key.LControl, KeyTriggerMode.WHILE_DOWN);
        private BooleanKeySwitch debugFlyoverrideSwitch = new BooleanKeySwitch(false, Key.ShiftLeft, KeyTriggerMode.WHILE_DOWN);

        public DefaultPlayerController(Player e)
            : base(e)
        {
            debugFlySwitch.TurnOnEvent += delegate { Console.Out.WriteLine("DebugFlyMode enabled"); };
            debugFlySwitch.TurnOffEvent += delegate { Console.Out.WriteLine("DebugFlyMode disabled"); };

            debugFlyoverrideSwitch.TurnOnEvent += delegate { if (debugFlySwitch.Value) Console.Out.WriteLine("DebugFlyMode++ enabled"); };
            debugFlyoverrideSwitch.TurnOffEvent += delegate { if (debugFlySwitch.Value) Console.Out.WriteLine("DebugFlyMode++ disabled"); };
        }

        public override void Update(KeyboardDevice keyboard, double ucorrection)
        {
            updateMovement(keyboard, ucorrection);
        }

        private void updateMovement(KeyboardDevice keyboard, double ucorrection)
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
                if (movementDelta.Y >= 0)
                {
                    delta.Y = Math.Max((PLAYER_JUMP_POWER + GRAVITY_ACCELERATION) - movementDelta.Y, 0);
                } else
                    delta.Y = PLAYER_JUMP_POWER + GRAVITY_ACCELERATION;
            }

            if (Program.debugViewSwitch.Value && debugFlySwitch.Value)
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
                MoveBy(delta, !debugFlyoverrideSwitch.Value, !debugFlyoverrideSwitch.Value);
            }
            else
                DoGravitationalMovement(delta, ucorrection);
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