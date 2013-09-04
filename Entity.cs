using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace SuperBitBros
{
    abstract class Entity
    {
        public Vector2d position;
        public double distance;

        public double width;
        public double height;

        public Entity()
        {
            this.position = new Vector2d(-1, -1);
            this.distance = 1;
            this.width = 1;
            this.height = 1;
        }

        public Vector2d GetTopLeft()
        {
            return new Vector2d(position.X, position.Y + height);
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

        public virtual void OnAdd()
        {
            //override me
        }
    }
}
