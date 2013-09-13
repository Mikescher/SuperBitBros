using OpenTK;
using OpenTK.Input;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.Entities;
using System;

namespace Entities.SuperBitBros {
    enum Direction { LEFT, RIGHT };

    class Player : AnimatedDynamicEntity {
        public const int PLAYER_WIDTH = 24;
        public const int PLAYER_HEIGHT = 24;

        public const double PLAYER_SPEED_FRICTION = 0.15;
        public const double PLAYER_SPEED_ACC = PLAYER_SPEED_FRICTION + 0.1;
        public const double PLAYER_SPEED_MAX = 4.5;
        public const double PLAYER_JUMP_POWER = 9;

        public Direction direction;

        public Player() {
            direction = Direction.RIGHT;
            distance = Entity.DISTANCE_PLAYER;
            width = PLAYER_WIDTH;
            height = PLAYER_HEIGHT;

            atexture.animation_speed = 5;

            atexture.Add(0, Textures.mario_small_sheet.GetTextureWrapper(2, 0));
            atexture.Add(0, Textures.mario_small_sheet.GetTextureWrapper(3, 0));
            atexture.Add(0, Textures.mario_small_sheet.GetTextureWrapper(4, 0));

            atexture.Add(1, Textures.mario_small_sheet.GetTextureWrapper(2, 1));
            atexture.Add(1, Textures.mario_small_sheet.GetTextureWrapper(3, 1));
            atexture.Add(1, Textures.mario_small_sheet.GetTextureWrapper(4, 1));

            atexture.Add(2, Textures.mario_small_sheet.GetTextureWrapper(5, 0));
            atexture.Add(2, Textures.mario_small_sheet.GetTextureWrapper(5, 1));
            atexture.Add(2, Textures.mario_small_sheet.GetTextureWrapper(0, 0));
            atexture.Add(2, Textures.mario_small_sheet.GetTextureWrapper(0, 1));
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            Vector2d delta = new Vector2d(0, 0);

            delta.X = -Math.Sign(movementDelta.X) * Math.Min(PLAYER_SPEED_FRICTION, Math.Abs(movementDelta.X));

            if (keyboard[Key.Left] && movementDelta.X > -PLAYER_SPEED_MAX) {
                delta.X -= PLAYER_SPEED_ACC;
            }

            if (keyboard[Key.Right] && movementDelta.X < PLAYER_SPEED_MAX) {
                delta.X += PLAYER_SPEED_ACC;
            }

            if (keyboard[Key.Space] && IsOnGround()) {
                delta.Y = PLAYER_JUMP_POWER + GRAVITY_ACCELERATION;
            }

            if (keyboard[Key.ShiftLeft]) // DEBUG
            {
                delta = Vector2d.Zero;
                if (keyboard[Key.Left])
                    delta.X -= PLAYER_SPEED_MAX*2;
                if (keyboard[Key.Right])
                    delta.X += PLAYER_SPEED_MAX*2;
                if (keyboard[Key.Space] || keyboard[Key.Up])
                    delta.Y += PLAYER_SPEED_MAX;
                if (keyboard[Key.Down])
                    delta.Y -= PLAYER_SPEED_MAX;
                moveBy(delta, false);
            }
            else
                updateGravitationalMovement(delta);
            

            UpdateTexture();
        }

        private void UpdateTexture() {
            if (IsOnGround()) {
                if (movementDelta.X > 0) {
                    atexture.SetLayer(1);
                    UpdateAnimation();
                    direction = Direction.RIGHT;
                } else if (movementDelta.X < 0) {
                    atexture.SetLayer(0);
                    UpdateAnimation();
                    direction = Direction.LEFT;
                } else {
                    atexture.Set(2, (direction == Direction.LEFT) ? 0 : 1);
                }
            } else {
                if (movementDelta.X > 0) {
                    direction = Direction.RIGHT;
                } else if (movementDelta.X < 0) {
                    direction = Direction.LEFT;
                }

                atexture.Set(2, (direction == Direction.LEFT) ? 2 : 3);
            }
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return true;
        }
    }
}
