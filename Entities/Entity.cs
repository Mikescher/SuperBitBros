using OpenTK;
using OpenTK.Input;
using SuperBitBros.OpenGL;
using SuperBitBros.Entities.Blocks;
using System;

namespace Entities.SuperBitBros {
    abstract class Entity {
        // 0 < DEPTH <= 100
        public const int DISTANCE_BACKRGOUND = 100;
        public const int DISTANCE_BLOCKS = 50;
        public const int DISTANCE_CORPSE = 45;
        public const int DISTANCE_MOBS = 40;
        public const int DISTANCE_POWERUPS = 25;
        public const int DISTANCE_PLAYER = 30;
        public const int DISTANCE_FOREGROUND = 10;

        public Vector2d position; // bottom left
        public double distance;

        public double width;
        public double height;

        protected OGLTexture texture;

        public Entity() {
            this.position = new Vector2d(-1, -1);
            this.distance = 1;
            this.width = 1;
            this.height = 1;
        }

        public virtual Rectangle2d GetPosition() {
            return new Rectangle2d(position, width, height);
        }

        public virtual Rectangle2d GetTexturePosition() {
            return GetPosition();
        }

        public virtual Rectangle3d GetPositionWithDistance() {
            return new Rectangle3d(new Vector3d(position.X, position.Y, distance), width, height);
        }

        public virtual Vector2d GetTopLeft() {
            return new Vector2d(position.X, position.Y + height);
        }

        public virtual Vector3d GetTopLeftWithDistance() {
            return new Vector3d(position.X, position.Y + height, distance);
        }

        public virtual Vector2d GetTopRight() {
            return new Vector2d(position.X + width, position.Y + height);
        }

        public virtual Vector3d GetTopRightWithDistance() {
            return new Vector3d(position.X + width, position.Y + height, distance);
        }

        public virtual Vector2d GetBottomLeft() {
            return new Vector2d(position.X, position.Y);
        }

        public virtual Vector3d GetBottomLeftWithDistance() {
            return new Vector3d(position.X, position.Y, distance);
        }

        public virtual Vector2d GetBottomRight() {
            return new Vector2d(position.X + width, position.Y);
        }

        public virtual Vector3d GetBottomRightWithDistance() {
            return new Vector3d(position.X + width, position.Y, distance);
        }

        public virtual Vector2d GetMiddle() {
            return new Vector2d(position.X + width / 2, position.Y + height / 2);
        }

        public virtual void OnRemove() { }

        public virtual OGLTexture GetCurrentTexture() {
            return texture;
        }

        public virtual void Update(KeyboardDevice keyboard) { }

        protected abstract bool IsBlockingOther(Entity sender);

        public virtual void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching) { }

        public static bool TestBlocking(Entity e1, Entity e2) {
            if (e1 is DynamicEntity && e2 is DynamicEntity) // 2 DynEntities
                return e1.IsBlockingOther(e1) && e2.IsBlockingOther(e2);
            else if (e1 is Block) // Der Block zählt
                return e1.IsBlockingOther(e1);
            else if (e2 is Block) // Der Block zählt
                return e2.IsBlockingOther(e1);
            else // Wad Wad Wad ???
            {
                Console.Error.WriteLine("2 Block Collision ???? {0} <-> {1}", e1, e2);
                return true;
            }

        }
    }
}
