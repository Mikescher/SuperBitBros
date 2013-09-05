using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using SuperBitBros;
using SuperBitBros.OpenGL;

namespace Entities.SuperBitBros
{
    abstract class Entity
    {
        public const int DISTANCE_BACKRGOUND = 100;
        public const int DISTANCE_BLOCKS = 50;
        public const int DISTANCE_MOBS = 40;
        public const int DISTANCE_PLAYER = 30;
        public const int DISTANCE_FOREGROUND = 10;

        public Vector2d position; // bottom left
        public double distance;

        public double width;
        public double height;

        protected OGLTexture texture;

        protected GameModel owner;

        public Entity()
        {
            this.position = new Vector2d(-1, -1);
            this.distance = 1;
            this.width = 1;
            this.height = 1;
        }

        public Rectangle2d GetPosition()
        {
            return new Rectangle2d(position, width, height);
        }

        public Rectangle3d GetPositionWithDistance()
        {
            return new Rectangle3d(new Vector3d(position.X, position.Y, distance), width, height);
        }

        public Vector3d GetTopLeftWithDistance()
        {
            return new Vector3d(position.X, position.Y + height, distance);
        }

        public Vector2d GetTopRight()
        {
            return new Vector2d(position.X + width, position.Y + height);
        }

        public Vector3d GetTopRightWithDistance()
        {
            return new Vector3d(position.X + width, position.Y + height, distance);
        }

        public Vector2d GetBottomLeft()
        {
            return new Vector2d(position.X, position.Y);
        }

        public Vector3d GetBottomLeftWithDistance()
        {
            return new Vector3d(position.X, position.Y, distance);
        }

        public Vector2d GetBottomRight()
        {
            return new Vector2d(position.X + width, position.Y);
        }

        public Vector3d GetBottomRightWithDistance()
        {
            return new Vector3d(position.X + width, position.Y, distance);
        }

        public virtual void OnAdd(GameModel owner)
        {
            this.owner = owner;
        }

        public virtual OGLTexture GetCurrentTexture()
        {
            return texture;
        }

        public virtual void Update(KeyboardDevice keboard) { }

        public abstract bool IsBlocking(Entity sender);
    }
}
