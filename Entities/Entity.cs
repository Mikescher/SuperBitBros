using System;
using OpenTK;
using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities
{
    public abstract class Entity
    {
        // 0 < DEPTH <= 100
        public const int DISTANCE_BACKRGOUND = 100;

        public const int DISTANCE_BLOCKS = 50;
        public const int DISTANCE_CORPSE = 45;
        public const int DISTANCE_MOBS = 40;
        public const int DISTANCE_STRUCTURES = 35;
        public const int DISTANCE_PLAYER = 30;
        public const int DISTANCE_POWERUPS = 25;
        public const int DISTANCE_PARTICLES = 15;
        public const int DISTANCE_FOREGROUND = 10;

        public const int DISTANCE_DEBUG_ZONE = 8;
        public const int DISTANCE_DEBUG_MINIMAP = 6;
        public const int DISTANCE_DEBUG_MARKER = 4;

        public GameModel owner;

        public Vec2d position; // bottom left
        public double distance;

        public double width;
        public double height;

        protected OGLTexture texture;

        public Entity()
        {
            this.position = new Vec2d(-1, -1);
            this.distance = 1;
            this.width = 1;
            this.height = 1;
        }

        public virtual Rect2d GetPosition()
        {
            return new Rect2d(position, width, height);
        }

        public virtual Rect2d GetTexturePosition()
        {
            return GetPosition();
        }

        public virtual Vec2d GetTopLeft()
        {
            return new Vec2d(position.X, position.Y + height);
        }

        public virtual Vec2d GetTopRight()
        {
            return new Vec2d(position.X + width, position.Y + height);
        }

        public virtual Vec2d GetBottomLeft()
        {
            return new Vec2d(position.X, position.Y);
        }

        public virtual Vec2d GetBottomRight()
        {
            return new Vec2d(position.X + width, position.Y);
        }

        public virtual Vector3d GetBottomRightWithDistance()
        {
            return new Vector3d(position.X + width, position.Y, distance);
        }

        public virtual Vec2d GetMiddle()
        {
            return new Vec2d(position.X + width / 2, position.Y + height / 2);
        }

        public virtual void OnRemove()
        {
        }

        public virtual OGLTexture GetCurrentTexture()
        {
            return texture;
        }

        public virtual void Update(KeyboardDevice keyboard)
        {
        }

        protected abstract bool IsBlockingOther(Entity sender);

        public virtual void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
        }

        public static bool TestBlocking(Entity e1, Entity e2)
        {
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