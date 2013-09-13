using OpenTK;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.Entities.Blocks;
using System;

namespace Entities.SuperBitBros {
    abstract class DynamicEntity : Entity {
        public const double PUSH_BACK_FORCE = 2;
        public const double GRAVITY_ACCELERATION = 0.4;
        public const double MAX_GRAVITY = 20;
        public const double DETECTION_TOLERANCE = 0.005; // Touching Detection Tolerance
        public const int BLOCK_DETECTION_RANGE = 3;

        protected GameModel owner;

        public Vector2d movementDelta = new Vector2d(0, 0);
        protected Vector2d physicPushForce = new Vector2d(0, 0);

        public DynamicEntity()
            : base() {

        }

        public virtual void OnAdd(GameModel model) {
            owner = model;
        }

        public virtual void OnAfterMapGen() { }

        public void moveBy(Vector2d vec, bool doCollision = true, bool doPhysicPush = true) {
            Rectangle2d nocollnewpos = new Rectangle2d(
                new Vector2d(
                    position.X + vec.X - DETECTION_TOLERANCE,
                    position.Y + vec.Y - DETECTION_TOLERANCE),
                width + DETECTION_TOLERANCE * 2,
                height + DETECTION_TOLERANCE * 2);

            if (doCollision)
            {
                position.X += testXCollision(vec);
                position.Y += testYCollision(vec);
            }
            else
                position = Vector2d.Add(position, vec);

            if (doPhysicPush)
                position = Vector2d.Add(position, physicPushForce);
            physicPushForce.X = 0;
            physicPushForce.Y = 0;

            Rectangle2d currPosition = GetPosition();

            foreach (Entity e in owner.GetCurrentEntityList()) {
                if (nocollnewpos.isColldingWith(e.GetPosition()) && e != this) {
                    bool isColl = currPosition.isColldingWith(e.GetPosition());
                    bool isBlock = Entity.TestBlocking(e, this);

                    e.onCollide(this, false, isBlock, isColl);
                    this.onCollide(e, true, isBlock, isColl);

                    if (isBlock && isColl)
                    {
                        Console.Error.WriteLine("Entity PUSHBACK !!!");
                        PushBackFrom(e);
                    }
                }
            }

            int left = (int)((position.X - BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((position.Y - BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((position.X + width + BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((position.Y + height + BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++) {
                for (int y = bottom; y < top; y++) {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && nocollnewpos.isColldingWith(b.GetPosition())) {
                        bool isColl = currPosition.isColldingWith(b.GetPosition());
                        bool isBlock = Entity.TestBlocking(b, this);

                        b.onCollide(this, false, isBlock, isColl);
                        this.onCollide(b, true, isBlock, isColl);

                        if (isBlock && isColl)
                        {
                            Console.Error.WriteLine("Block PUSHBACK !!!");
                            PushBackFrom(b);
                        }
                    }
                }
            }
        }

        private void PushBackFrom(Entity e)
        {
            Vector2d force = Vector2d.Subtract(GetMiddle(), e.GetMiddle());
            if (force.X == 0 && force.Y == 0)
                force.Y = 1;

            force.Normalize();
            force = Vector2d.Multiply(force, PUSH_BACK_FORCE);
            physicPushForce = force;

            if (e is DynamicEntity)
                ((DynamicEntity)e).physicPushForce = -force;
        }

        private double testXCollision(Vector2d vec) {
            Rectangle2d newpos = new Rectangle2d(new Vector2d(position.X + vec.X, position.Y), width, height);

            // TEST ENTITIES

            foreach (Entity e in owner.entityList) {
                if (e != this && Entity.TestBlocking(e, this) && newpos.isColldingWith(e.GetPosition())) {
                    return this.GetPosition().getDistanceTo(e.GetPosition()).X;
                }
            }

            //TEST BLOCKS IN RANGE

            int left = (int)((newpos.br.X - BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((newpos.br.Y - BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((newpos.tl.X + BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((newpos.tl.Y + BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++) {
                for (int y = bottom; y < top; y++) {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && Entity.TestBlocking(b, this) && newpos.isColldingWith(b.GetPosition())) {
                        return this.GetPosition().getDistanceTo(b.GetPosition()).X;
                    }
                }
            }

            return vec.X;
        }

        private double testYCollision(Vector2d vec) {
            Rectangle2d newpos = new Rectangle2d(new Vector2d(position.X, position.Y + vec.Y), width, height);

            // TEST ENTITIES

            foreach (Entity e in owner.entityList) {
                if (e != this && Entity.TestBlocking(e, this) && newpos.isColldingWith(e.GetPosition())) {
                    return this.GetPosition().getDistanceTo(e.GetPosition()).Y;
                }
            }

            //TEST BLOCKS IN RANGE

            int left = (int)((newpos.br.X - BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((newpos.br.Y - BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((newpos.tl.X + BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((newpos.tl.Y + BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++) {
                for (int y = bottom; y < top; y++) {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && Entity.TestBlocking(b, this) && newpos.isColldingWith(b.GetPosition())) {
                        return this.GetPosition().getDistanceTo(b.GetPosition()).Y;
                    }
                }
            }

            return vec.Y;
        }

        protected bool IsOnGround() {
            return testYCollision(new Vector2d(0, -DETECTION_TOLERANCE)) >= 0;
        }

        protected bool IsOnCeiling() {
            return testYCollision(new Vector2d(0, DETECTION_TOLERANCE)) <= 0;
        }

        protected bool IsCollidingRight() {
            return testXCollision(new Vector2d(DETECTION_TOLERANCE, 0)) <= 0;
        }

        protected bool IsCollidingLeft() {
            return testXCollision(new Vector2d(-DETECTION_TOLERANCE, 0)) >= 0;
        }

        protected void updateGravitationalMovement(Vector2d additionalForce, bool resetXOnCollision = true, bool resetYOnCollision = true)
        {
            //movementDelta.X = 0;
            movementDelta.Y -= DynamicEntity.GRAVITY_ACCELERATION;
            if (IsOnGround())
                movementDelta.Y = Math.Max(movementDelta.Y, 0);

            if (movementDelta.Y < -MAX_GRAVITY)
                movementDelta.Y = -MAX_GRAVITY;

            movementDelta = Vector2d.Add(movementDelta, additionalForce);

            if (IsOnGround() && resetYOnCollision)
                movementDelta.Y = Math.Max(movementDelta.Y, 0);
            if (IsOnCeiling() && resetYOnCollision)
                movementDelta.Y = Math.Min(movementDelta.Y, 0);
            if (IsCollidingLeft() && resetXOnCollision)
                movementDelta.X = Math.Max(movementDelta.X, 0);
            if (IsCollidingRight() && resetXOnCollision)
                movementDelta.X = Math.Min(movementDelta.X, 0);

            moveBy(movementDelta);
        }
    }
}
