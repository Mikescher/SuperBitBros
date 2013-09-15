using SuperBitBros;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.Trigger;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Collections.Generic;

namespace Entities.SuperBitBros {
    abstract class DynamicEntity : Entity {
        public const double PUSH_BACK_FORCE = 2;
        public const double GRAVITY_ACCELERATION = 0.4;
        public const double MAX_GRAVITY = 20;
        public const double DETECTION_TOLERANCE = 0.005; // Touching Detection Tolerance
        public const int BLOCK_DETECTION_RANGE = 3;

        public Vec2d movementDelta = new Vec2d(0, 0);
        protected Vec2d physicPushForce = new Vec2d(0, 0);

        public DynamicEntity()
            : base() {

        }

        public virtual void OnAdd(GameModel model) {
            owner = model;
        }

        public virtual void OnAfterMapGen() { }

        public void MoveBy(Vec2d vec, bool doCollision = true, bool doPhysicPush = true) {
            Rect2d nocollnewpos = new Rect2d(
                new Vec2d(
                    position.X + vec.X - DETECTION_TOLERANCE,
                    position.Y + vec.Y - DETECTION_TOLERANCE),
                width + DETECTION_TOLERANCE * 2,
                height + DETECTION_TOLERANCE * 2);

            if (doCollision) {
                position.X += testXCollision(vec);
                position.Y += testYCollision(vec);
            } else
                position += vec;

            if (doPhysicPush)
                position += physicPushForce;
            physicPushForce.X = 0;
            physicPushForce.Y = 0;

            Rect2d currPosition = GetPosition();

            // Collide with Entities

            foreach (Entity e in owner.GetCurrentEntityList()) {
                if (nocollnewpos.IsColldingWith(e.GetPosition()) && e != this) {
                    bool isColl = currPosition.IsColldingWith(e.GetPosition());
                    bool isTouch = currPosition.IsTouching(e.GetPosition());
                    bool isBlock = Entity.TestBlocking(e, this);

                    e.onCollide(this, false, isBlock, isColl, isTouch);
                    this.onCollide(e, true, isBlock, isColl, isTouch);

                    if (isBlock && isColl) {
                        Console.Error.WriteLine("Entity PUSHBACK !!!");
                        PushBackFrom(e);
                    }
                }
            }

            // Collide with Blocks

            int left = (int)((position.X - BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((position.Y - BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((position.X + width + BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((position.Y + height + BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++) {
                for (int y = bottom; y < top; y++) {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && nocollnewpos.IsColldingWith(b.GetPosition())) {
                        bool isColl = currPosition.IsColldingWith(b.GetPosition());
                        bool isTouch = currPosition.IsTouching(b.GetPosition());
                        bool isBlock = Entity.TestBlocking(b, this);

                        b.onCollide(this, false, isBlock, isColl, isTouch);
                        this.onCollide(b, true, isBlock, isColl, isTouch);

                        if (isBlock && isColl) {
                            Console.Error.WriteLine("Block PUSHBACK !!!");
                            PushBackFrom(b);
                        }
                    }
                }
            }

            // Collide with TriggerZones

            for (int x = left; x < right; x++) {
                for (int y = bottom; y < top; y++) {
                    List<Trigger> tlist = owner.getTriggerList(x, y);

                    if (tlist != null) {
                        foreach (Trigger t in tlist) {
                            bool isColl = currPosition.IsColldingWith(t.GetPosition());

                            if (isColl) {
                                t.OnCollide(this);
                            }
                        }
                    }
                }
            }
        }

        private void PushBackFrom(Entity e) {
            Vec2d force = GetMiddle() - e.GetMiddle();
            if (force.X == 0 && force.Y == 0)
                force.Y = 1;

            force.SetLength(PUSH_BACK_FORCE);
            physicPushForce = force;

            if (e is DynamicEntity)
                ((DynamicEntity)e).physicPushForce = -force;
        }

        private double testXCollision(Vec2d vec) {
            Rect2d newpos = new Rect2d(new Vec2d(position.X + vec.X, position.Y), width, height);

            // TEST ENTITIES

            foreach (Entity e in owner.entityList) {
                if (e != this && Entity.TestBlocking(e, this) && newpos.IsColldingWith(e.GetPosition())) {
                    return this.GetPosition().GetDistanceTo(e.GetPosition()).X;
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
                    if (b != null && Entity.TestBlocking(b, this) && newpos.IsColldingWith(b.GetPosition())) {
                        return this.GetPosition().GetDistanceTo(b.GetPosition()).X;
                    }
                }
            }

            return vec.X;
        }

        private double testYCollision(Vec2d vec) {
            Rect2d newpos = new Rect2d(new Vec2d(position.X, position.Y + vec.Y), width, height);

            // TEST ENTITIES

            foreach (Entity e in owner.entityList) {
                if (e != this && Entity.TestBlocking(e, this) && newpos.IsColldingWith(e.GetPosition())) {
                    return this.GetPosition().GetDistanceTo(e.GetPosition()).Y;
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
                    if (b != null && Entity.TestBlocking(b, this) && newpos.IsColldingWith(b.GetPosition())) {
                        return this.GetPosition().GetDistanceTo(b.GetPosition()).Y;
                    }
                }
            }

            return vec.Y;
        }

        protected bool IsOnGround() {
            return testYCollision(new Vec2d(0, -DETECTION_TOLERANCE)) >= 0;
        }

        protected bool IsOnCeiling() {
            return testYCollision(new Vec2d(0, DETECTION_TOLERANCE)) <= 0;
        }

        protected bool IsCollidingRight() {
            return testXCollision(new Vec2d(DETECTION_TOLERANCE, 0)) <= 0;
        }

        protected bool IsCollidingLeft() {
            return testXCollision(new Vec2d(-DETECTION_TOLERANCE, 0)) >= 0;
        }

        protected void DoGravitationalMovement(Vec2d additionalForce, bool resetXOnCollision = true, bool resetYOnCollision = true) {
            //movementDelta.X = 0;
            movementDelta.Y -= DynamicEntity.GRAVITY_ACCELERATION;
            if (IsOnGround())
                movementDelta.Y = Math.Max(movementDelta.Y, 0);

            if (movementDelta.Y < -MAX_GRAVITY)
                movementDelta.Y = -MAX_GRAVITY;

            movementDelta += additionalForce;

            if (IsOnGround() && resetYOnCollision)
                movementDelta.Y = Math.Max(movementDelta.Y, 0);
            if (IsOnCeiling() && resetYOnCollision)
                movementDelta.Y = Math.Min(movementDelta.Y, 0);
            if (IsCollidingLeft() && resetXOnCollision)
                movementDelta.X = Math.Max(movementDelta.X, 0);
            if (IsCollidingRight() && resetXOnCollision)
                movementDelta.X = Math.Min(movementDelta.X, 0);

            MoveBy(movementDelta);
        }

        public void Kill() {
            owner.RemoveEntity(this);
        }
    }
}
