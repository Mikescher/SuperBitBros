using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public abstract class AbstractNewtonEntityController : AbstractEntityController
    {
        public const double PUSH_BACK_FORCE = 2;
        private const double STANDARD_GRAVITY_ACCELERATION = 0.4;
        private const double STANDARD_MAX_GRAVITY = 20;

        public Vec2d movementDelta = Vec2d.Zero;
        protected Vec2d physicPushForce = Vec2d.Zero;

        protected double Gravity_Acc = STANDARD_GRAVITY_ACCELERATION;
        protected double Gravity_Max = STANDARD_MAX_GRAVITY;

        public AbstractNewtonEntityController(DynamicEntity e)
            : base(e)
        {
            //--
        }

        public override void OnHide()
        {
            movementDelta = Vec2d.Zero;
            physicPushForce = Vec2d.Zero;
        }

        public override void OnReshow()
        {
            movementDelta = Vec2d.Zero;
            physicPushForce = Vec2d.Zero;
        }

        public void DoGravitationalMovement(Vec2d additionalForce, bool resetXOnCollision = true, bool resetYOnCollision = true, bool doCollision = true, bool doPhysicPush = true)
        {
            //movementDelta.X = 0;
            movementDelta.Y -= Gravity_Acc;
            if (doCollision && ent.IsOnGround())
                movementDelta.Y = Math.Max(movementDelta.Y, 0);

            if (movementDelta.Y < -Gravity_Max)
                movementDelta.Y = -Gravity_Max;

            movementDelta += additionalForce;

            if (ent.IsOnGround() && resetYOnCollision)
                movementDelta.Y = Math.Max(movementDelta.Y, 0);
            if (ent.IsOnCeiling() && resetYOnCollision)
                movementDelta.Y = Math.Min(movementDelta.Y, 0);
            if (ent.IsCollidingLeft() && resetXOnCollision)
                movementDelta.X = Math.Max(movementDelta.X, 0);
            if (ent.IsCollidingRight() && resetXOnCollision)
                movementDelta.X = Math.Min(movementDelta.X, 0);

            MoveBy(movementDelta, doCollision, doPhysicPush);
        }

        public void MoveBy(Vec2d vec, bool doCollision = true, bool doPhysicPush = true)
        {
            if (doCollision)
            {
                ent.position.X += DoXCollisionMove(vec);
                ent.position.Y += DoYCollisionMove(vec);
            }
            else
                ent.position += vec;

            if (doPhysicPush)
                ent.position += physicPushForce;
            physicPushForce.X = 0;
            physicPushForce.Y = 0;

            ent.DoCollisions();
        }

        private double DoXCollisionMove(Vec2d vec)
        {
            Rect2d newpos = new Rect2d(new Vec2d(ent.position.X + vec.X, ent.position.Y), ent.width, ent.height);

            // TEST ENTITIES

            foreach (Entity e in owner.dynamicEntityList)
            {
                if (e != ent && Entity.TestBlocking(e, ent) && newpos.IsColldingWith(e.GetPosition()))
                {
                    return ent.GetPosition().GetDistanceTo(e.GetPosition()).X;
                }
            }

            //TEST BLOCKS IN RANGE

            int left = (int)((newpos.br.X - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((newpos.br.Y - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((newpos.tl.X + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((newpos.tl.Y + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++)
            {
                for (int y = bottom; y < top; y++)
                {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && Entity.TestBlocking(b, ent) && newpos.IsColldingWith(b.GetPosition()))
                    {
                        return ent.GetPosition().GetDistanceTo(b.GetPosition()).X;
                    }
                }
            }

            return vec.X;
        }

        private double DoYCollisionMove(Vec2d vec)
        {
            Rect2d newpos = new Rect2d(new Vec2d(ent.position.X, ent.position.Y + vec.Y), ent.width, ent.height);

            // TEST ENTITIES

            foreach (Entity e in owner.dynamicEntityList)
            {
                if (e != ent && Entity.TestBlocking(e, ent) && newpos.IsColldingWith(e.GetPosition()))
                {
                    return ent.GetPosition().GetDistanceTo(e.GetPosition()).Y;
                }
            }

            //TEST BLOCKS IN RANGE

            int left = (int)((newpos.br.X - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((newpos.br.Y - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((newpos.tl.X + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((newpos.tl.Y + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++)
            {
                for (int y = bottom; y < top; y++)
                {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && Entity.TestBlocking(b, ent) && newpos.IsColldingWith(b.GetPosition()))
                    {
                        return ent.GetPosition().GetDistanceTo(b.GetPosition()).Y;
                    }
                }
            }

            return vec.Y;
        }

        public override void OnIllegalIntersection(Entity other)
        {
            Console.Error.WriteLine("PUSHBACK {0} <-> {1} !!!", ent.GetType().Name, other.GetType().Name);

            PushBackFrom(other);
        }

        private void PushBackFrom(Entity e)
        {
            Vec2d force = ent.GetMiddle() - e.GetMiddle();
            if (force.X == 0 && force.Y == 0)
                force.Y = 1;

            force.SetLength(PUSH_BACK_FORCE);
            physicPushForce = force;

            //if (e is DynamicEntity)
            //    ((DynamicEntity)e).physicPushForce = -force;
        }

        public override Vec2d GetDelta()
        {
            return movementDelta;
        }
    }
}