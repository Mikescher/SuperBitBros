using OpenTK;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.Entities.Blocks;
using System;

namespace Entities.SuperBitBros {
    abstract class DynamicEntity : Entity {
        public const double GRAVITY_ACCELERATION = 0.4;
        public const double MAX_GRAVITY = 20;
        public const double DETECTION_TOLERANCE = 0.005; // Touching Detection Tolerance
        public const int BLOCK_DETECTION_RANGE = 3;

        protected GameModel owner;

        public Vector2d movementDelta = new Vector2d();

        public DynamicEntity()
            : base() {

        }

        public virtual void OnAdd(GameModel model) {
            owner = model;
        }

        public virtual void OnAfterMapGen() { }

        public void moveBy(Vector2d vec) {
            Rectangle2d nocollnewpos = new Rectangle2d(
                new Vector2d(
                    position.X + vec.X - DETECTION_TOLERANCE,
                    position.Y + vec.Y - DETECTION_TOLERANCE),
                width + DETECTION_TOLERANCE * 2,
                height + DETECTION_TOLERANCE * 2);

            position.X += testXCollision(vec);
            position.Y += testYCollision(vec);

            Rectangle2d currPosition = GetPosition();

            foreach (Entity e in owner.GetCurrentEntityList()) {
                if (nocollnewpos.isColldingWith(e.GetPosition()) && e != this) {
                    e.onCollide(this, false, e.IsBlocking(this), currPosition.isColldingWith(e.GetPosition()));
                    this.onCollide(e, true, e.IsBlocking(this), currPosition.isColldingWith(e.GetPosition()));
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
                        b.onCollide(this, false, b.IsBlocking(this), currPosition.isColldingWith(b.GetPosition()));
                        this.onCollide(b, true, b.IsBlocking(this), currPosition.isColldingWith(b.GetPosition()));
                    }
                }
            }
        }

        private double testXCollision(Vector2d vec) {
            Rectangle2d newpos = new Rectangle2d(new Vector2d(position.X + vec.X, position.Y), width, height);

            // TEST ENTITIES

            foreach (Entity e in owner.entityList) {
                if (e != this && e.IsBlocking(this) && newpos.isColldingWith(e.GetPosition())) {
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
                    if (b != null && b.IsBlocking(this) && newpos.isColldingWith(b.GetPosition())) {
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
                if (e != this && e.IsBlocking(this) && newpos.isColldingWith(e.GetPosition())) {
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
                    if (b != null && b.IsBlocking(this) && newpos.isColldingWith(b.GetPosition())) {
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

        protected void updateGravitationalMovement(Vector2d additionalForce) {
            //movementDelta.X = 0;
            movementDelta.Y -= DynamicEntity.GRAVITY_ACCELERATION;
            if (IsOnGround())
                movementDelta.Y = Math.Max(movementDelta.Y, 0);

            if (movementDelta.Y < -MAX_GRAVITY)
                movementDelta.Y = -MAX_GRAVITY;

            movementDelta = Vector2d.Add(movementDelta, additionalForce);

            if (IsOnGround())
                movementDelta.Y = Math.Max(movementDelta.Y, 0);
            if (IsOnCeiling())
                movementDelta.Y = Math.Min(movementDelta.Y, 0);
            if (IsCollidingLeft())
                movementDelta.X = Math.Max(movementDelta.X, 0);
            if (IsCollidingRight())
                movementDelta.X = Math.Min(movementDelta.X, 0);

            moveBy(movementDelta);
        }
    }
}
