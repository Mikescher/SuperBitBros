using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;
using System.Collections.Generic;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using System;

namespace SuperBitBros.Entities.DynamicEntities
{
    public enum Direction { LEFT, RIGHT };

    public class Player : AnimatedDynamicEntity
    {
        public const double PLAYER_SCALE = 0.9;

        public const int PLAYER_WIDTH = Block.BLOCK_WIDTH;
        public const int PLAYER_HEIGHT = Block.BLOCK_HEIGHT;

        private const int PLAYER_EXPLOSIONFRAGMENTS_X = 8;
        private const int PLAYER_EXPLOSIONFRAGMENTS_Y = 8;
        private const double PLAYER_EXPLOSIONFRAGMENTS_FORCE = 24.0;

        private BooleanKeySwitch debugExplosionSwitch = new BooleanKeySwitch(false, Key.F8, KeyTriggerMode.FLICKER_DOWN);

        public Direction direction;

        public Player() 
            : base()
        {
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

        private bool IsUserControlled()
        {
            return controllerStack.Count != 0 && controllerStack.Peek() is DefaultPlayerController;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (IsUserControlled())
            {
                if (keyboard[Key.Down] && IsOnGround())
                    TestForPipe(PipeDirection.SOUTH);
                if (keyboard[Key.Right] && IsCollidingRight())
                    TestForPipe(PipeDirection.EAST);
                if (keyboard[Key.Up] && IsOnCeiling())
                    TestForPipe(PipeDirection.NORTH);
                if (keyboard[Key.Down] && IsCollidingLeft())
                    TestForPipe(PipeDirection.WEST);
            }

            if (debugExplosionSwitch.Value) { Explode(); KillLater(); }

            UpdateTexture();
        }

        private void TestForPipe(PipeDirection d)
        {
            Vec2i blockpos = (Vec2i)(GetMiddle() / Block.BLOCK_SIZE);

            blockpos += PipeZone.GetVectorForDirection(d);

            List<Trigger> triggerlist = owner.getTriggerList(blockpos.X, blockpos.Y);
            if (triggerlist != null)
                foreach (Trigger t in owner.getTriggerList(blockpos.X, blockpos.Y))
                    if (t is PipeZone && (t as PipeZone).IsDirection(d) && (t as PipeZone).CanEnter())
                        AddController(new PipePlayerController(this, d));
        }

        private void UpdateTexture()
        {
            if (IsOnGround())
            {
                if (GetMovement().X > 0)
                {
                    atexture.SetLayer(1);
                    UpdateAnimation();
                    direction = Direction.RIGHT;
                }
                else if (GetMovement().X < 0)
                {
                    atexture.SetLayer(0);
                    UpdateAnimation();
                    direction = Direction.LEFT;
                }
                else
                {
                    atexture.Set(2, (direction == Direction.LEFT) ? 0 : 1);
                }
            }
            else
            {
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

        protected override bool IsBlockingOther(Entity sender)
        {
            return true;
        }

        public void OnCollideNonStaticFlag(Vec2d flagpos)
        {
            AddController(new FlagPlayerController(this, flagpos));
        }

        public void Explode()
        {
            DoExplosionEffect(PLAYER_EXPLOSIONFRAGMENTS_X, PLAYER_EXPLOSIONFRAGMENTS_Y, PLAYER_EXPLOSIONFRAGMENTS_FORCE);
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_MISC;
        }

        public void OnMobHeadJump(Mob m)
        {
            if (HasController()) {
                DefaultPlayerController con = controllerStack.Peek() as DefaultPlayerController;
                if (con != null) {
                    con.DoMobKillPushback();
                }
            }
        }

        public void DoDeath(Mob m)
        {
            Console.Out.WriteLine("Death by Mob: " + m.GetType().Name);

            //Explode();
            //KillLater();
        }

        public void DoDeath(Trigger t)
        {
            Console.Out.WriteLine("Death by Zone: " + t.GetType().Name);

            //Explode();
            //KillLater();
        }
    }
}