using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL;

namespace Entities.SuperBitBros
{
    abstract class DynamicEntity : Entity
    {
        public const double GRAVITY_ACCELERATION = 0.8;
        public const double MAX_GRAVITY = 10;
        public const double GC_DETECTION_TOLERANCE = 0.25; // Ground/Ceiling Detection Tolerance

        public DynamicEntity() : base()
        {

        }

        public void moveBy(Vector2d vec)
        {

            position.X += testXCollision(vec);
            position.Y += testYCollision(vec);
        }

        private double testXCollision(Vector2d vec)
        {
            Rectangle2d newpos = new Rectangle2d(new Vector2d(position.X + vec.X, position.Y), width, height);

            foreach(Entity e in owner.entityList)
            {
                if (newpos.isColldingWith(e.GetPosition()) && e != this && e.IsBlocking(this))
                    return this.GetPosition().getDistanceTo(e.GetPosition()).X;
            }
            return vec.X;
        }

        private double testYCollision(Vector2d vec)
        {
            Rectangle2d newpos = new Rectangle2d(new Vector2d(position.X, position.Y + vec.Y), width, height);

            foreach (Entity e in owner.entityList)
            {
                if (newpos.isColldingWith(e.GetPosition()) && e != this && e.IsBlocking(this))
                    return this.GetPosition().getDistanceTo(e.GetPosition()).Y;
            }
            return vec.Y;
        }

        protected bool IsOnGround()
        {
            return testYCollision(new Vector2d(0, -GC_DETECTION_TOLERANCE)) >= 0;
        }

        protected bool IsOnCeiling()
        {
            return testYCollision(new Vector2d(0, GC_DETECTION_TOLERANCE)) <= 0;
        }
    }
}
