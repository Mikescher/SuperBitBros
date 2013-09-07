using OpenTK;
using SuperBitBros.OpenGL;
using System;

namespace Entities.SuperBitBros {
    abstract class DynamicEntity : Entity {
        public const double GRAVITY_ACCELERATION = 0.4;
        public const double MAX_GRAVITY = 20;
        public const double GC_DETECTION_TOLERANCE = 0.005; // Ground/Ceiling Detection Tolerance

        public Vector2d movementDelta = new Vector2d();

        public DynamicEntity()
            : base() {

        }

        public void moveBy(Vector2d vec) {
            Rectangle2d nocollnewpos = new Rectangle2d(
                new Vector2d(
                    position.X + vec.X - GC_DETECTION_TOLERANCE,
                    position.Y + vec.Y - GC_DETECTION_TOLERANCE),
                width + GC_DETECTION_TOLERANCE * 2,
                height + GC_DETECTION_TOLERANCE * 2);

            position.X += testXCollision(vec);
            position.Y += testYCollision(vec);

            foreach (Entity e in owner.GetCurrentEntityList()) {
                if (nocollnewpos.isColldingWith(e.GetPosition()) && e != this) {
                    e.onCollide(this, false, e.IsBlocking(this));
                    this.onCollide(e, true, e.IsBlocking(this));
                }
            }
        }

        private double testXCollision(Vector2d vec) {
            Rectangle2d newpos = new Rectangle2d(new Vector2d(position.X + vec.X, position.Y), width, height);

            foreach (Entity e in owner.entityList) {
                if (newpos.isColldingWith(e.GetPosition()) && e != this && e.IsBlocking(this)) {
                    return this.GetPosition().getDistanceTo(e.GetPosition()).X;
                }
            }
            return vec.X;
        }

        private double testYCollision(Vector2d vec) {
            Rectangle2d newpos = new Rectangle2d(new Vector2d(position.X, position.Y + vec.Y), width, height);

            foreach (Entity e in owner.entityList) {
                if (newpos.isColldingWith(e.GetPosition()) && e != this && e.IsBlocking(this)) {
                    return this.GetPosition().getDistanceTo(e.GetPosition()).Y;
                }
            }
            return vec.Y;
        }

        protected bool IsOnGround() {
            return testYCollision(new Vector2d(0, -GC_DETECTION_TOLERANCE)) >= 0;
        }

        protected bool IsOnCeiling() {
            return testYCollision(new Vector2d(0, GC_DETECTION_TOLERANCE)) <= 0;
        }

        protected void updateGravitationalMovement(Vector2d additionalForce) {
            movementDelta.X = 0;
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

            moveBy(movementDelta);
        }
    }
}
