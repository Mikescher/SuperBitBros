using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Input;
using SuperBitBros.Entities.Blocks;

namespace SuperBitBros
{
    class Player : DynamicEntity
    {
        public const int PLAYER_WIDTH = 24;
        public const int PLAYER_HEIGHT = 24;

        public const double PLAYER_SPEED = 3;
        public const double PLAYER_JUMP_POWER = 12.5;

        public Vector2d delta = new Vector2d();

        public Player()
        {
            distance = Entity.DISTANCE_PLAYER;
            width = PLAYER_WIDTH;
            height = PLAYER_HEIGHT;
            texture = Textures.mario_small_sheet.GetTextureWrapper(5, 1);
        }

        public override void Update(KeyboardDevice keyboard) {
            delta.X = 0;
            delta.Y -= DynamicEntity.GRAVITY_ACCELERATION;
            if (delta.Y < -MAX_GRAVITY) delta.Y = -MAX_GRAVITY;

            if (keyboard[Key.Left]) {
                delta.X -= PLAYER_SPEED;
            }
            if (keyboard[Key.Right]) {
                delta.X += PLAYER_SPEED;
            }
            if (keyboard[Key.Space] && IsOnGround()) {
                delta.Y = PLAYER_JUMP_POWER;
            }

            if (IsOnGround()) delta.Y = Math.Max(delta.Y, 0);
            if (IsOnCeiling()) delta.Y = Math.Min(delta.Y, 0);

            moveBy(delta);
        }

        public override bool IsBlocking(Entity sender)
        {
            return false;
        }
    }
}
