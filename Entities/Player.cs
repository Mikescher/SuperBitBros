using OpenTK;
using OpenTK.Input;
using SuperBitBros;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL.OGLMath;
using System;
using SuperBitBros.Entities.EnityController;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;
using System.Collections.Generic;

namespace SuperBitBros.Entities {
    enum Direction { LEFT, RIGHT };

    class Player : AnimatedDynamicEntity
    {
        public const double PLAYER_SCALE = 0.9;

        public const int PLAYER_WIDTH = Block.BLOCK_WIDTH;
        public const int PLAYER_HEIGHT = Block.BLOCK_HEIGHT;

        public Direction direction;

        public Player() {
            direction = Direction.RIGHT;
            distance = Entity.DISTANCE_PLAYER;
            width = PLAYER_WIDTH * PLAYER_SCALE;
            height = PLAYER_HEIGHT * PLAYER_SCALE;

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

            AddController(new DefaultPlayerController(this));
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            if (keyboard[Key.Down] && IsOnGround())
                TestForSouthPipe();

            UpdateTexture();
        }

        private void TestForSouthPipe()
        {
            Vec2i blockpos = (Vec2i)(position / Block.BLOCK_SIZE);

            blockpos.Y--;

            List<Trigger> triggerlist = owner.getTriggerList(blockpos.X, blockpos.Y);
            if (triggerlist != null)
                foreach (Trigger t in owner.getTriggerList(blockpos.X, blockpos.Y))
                    if (t is PipeZone && (t as PipeZone).GetDirection() == PipeDirection.SOUTH)
                        AddController(new PipePlayerController(this, PipeDirection.SOUTH));
        }

        private void UpdateTexture() {
            if (IsOnGround()) {
                if (GetMovement().X > 0)
                {
                    atexture.SetLayer(1);
                    UpdateAnimation();
                    direction = Direction.RIGHT;
                } else if (GetMovement().X < 0) {
                    atexture.SetLayer(0);
                    UpdateAnimation();
                    direction = Direction.LEFT;
                } else {
                    atexture.Set(2, (direction == Direction.LEFT) ? 0 : 1);
                }
            } else {
                if (GetMovement().X > 0)
                {
                    direction = Direction.RIGHT;
                }
                else if (GetMovement().X < 0)
                {
                    direction = Direction.LEFT;
                }

                atexture.Set(2, (direction == Direction.LEFT) ? 2 : 3);
            }
        }

        protected override bool IsBlockingOther(Entity sender) {
            return true;
        }
    }
}
