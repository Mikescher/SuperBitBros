using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.Entities.EnityController;
using SuperBitBros.MarioPower;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;
using System;
using System.Collections.Generic;

namespace SuperBitBros.Entities.DynamicEntities
{
    public enum Direction { LEFT, RIGHT };

    public class Player : AnimatedDynamicEntity
    {
        private const int INVINCIBLE_TIME = 45;

        public const double PLAYER_SCALE = 1.25;

        public const int PLAYER_WIDTH = Block.BLOCK_WIDTH;
        public const int PLAYER_HEIGHT = Block.BLOCK_WIDTH;

        private const int PLAYER_EXPLOSIONFRAGMENTS_X = 8;
        private const int PLAYER_EXPLOSIONFRAGMENTS_Y = 8;
        private const double PLAYER_EXPLOSIONFRAGMENTS_FORCE = 24.0;

        private BooleanKeySwitch debugExplosionSwitch = new BooleanKeySwitch(false, Key.F5, KeyTriggerMode.FLICKER_DOWN);

        public Direction direction;

        public AbstractMarioPower power { get; private set; }

        private int invincTime = 0;

        public Player()
            : base()
        {
            direction = Direction.RIGHT;
            distance = Entity.DISTANCE_PLAYER;
            width = PLAYER_WIDTH * PLAYER_SCALE;
            height = PLAYER_HEIGHT * PLAYER_SCALE;

            ChangePower(new StandardMarioPower());

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

            if (invincTime > 0)
                invincTime--;

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
            return EntityRenderType.BRT_MARIO;
        }

        public void OnMobHeadJump(Mob m)
        {
            if (HasController())
            {
                DefaultPlayerController con = controllerStack.Peek() as DefaultPlayerController;
                if (con != null)
                {
                    if (m is PiranhaPlant && (m as PiranhaPlant).state == 0)
                        return;
                    con.DoMobKillPushback();
                }
            }
        }

        public void DoDeath(DynamicEntity e)
        {
            Console.Out.WriteLine("Death by Entity: " + e.GetType().Name);

            DoDeath();
        }

        public void DoDeath(Mob m)
        {
            Console.Out.WriteLine("Death by Mob: " + m.GetType().Name);

            DoDeath();
        }

        public void DoDeath(Trigger t)
        {
            Console.Out.WriteLine("Death by Zone: " + t.GetType().Name);

            DoDeath(true);
        }

        private void DoDeath(bool direct = false)
        {
            if (invincTime == 0 && !Program.debugViewSwitch.Value)
            {
                AbstractMarioPower sub = power.GetSubPower();
                if (sub == null || direct)
                {
                    Explode();
                    KillLater();
                }
                else
                {
                    ChangePower(sub);
                    invincTime = INVINCIBLE_TIME;
                }
            }
        }

        public bool IsInPipe()
        {
            return HasController() && controllerStack.Peek() is PipePlayerController;
        }

        public void MakeStatic()
        {
            AddController(new StaticEntityController(this));
        }

        public void ChangePower(AbstractMarioPower p)
        {
            power = p;
            height = PLAYER_HEIGHT * PLAYER_SCALE * p.GetHeightMultiplier();
            atexture = p.GetTexture();
        }

        public void growToBigPlayer()
        {
            if (power is StandardMarioPower)
                ChangePower(new BigMarioPower());
        }

        public bool IsBig()
        {
            return !(power is StandardMarioPower);
        }

        public void WalkToLevelEnd()
        {
            AddController(new LevelEndWalkPlayerController(this));
        }
    }
}