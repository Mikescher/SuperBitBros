using System;
using SuperBitBros.Entities;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Triggers.PipeZones
{
    public enum PipeDirection { NORTH, EAST, SOUTH, WEST, NORTHSOUTH, EASTWEST, ANY }

    public abstract class PipeZone : Trigger
    {
        public const double PIPESPEED_NORMAL = 3.5;

        public PipeZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        public override void OnCollide(DynamicEntity collider)
        {
            //nothing
        }

        public bool IsDirection(PipeDirection d)
        {
            switch (GetDirection())
            {
                case PipeDirection.NORTH:
                    return d == PipeDirection.NORTH;

                case PipeDirection.EAST:
                    return d == PipeDirection.EAST;

                case PipeDirection.SOUTH:
                    return d == PipeDirection.SOUTH;

                case PipeDirection.WEST:
                    return d == PipeDirection.WEST;

                case PipeDirection.NORTHSOUTH:
                    return d == PipeDirection.NORTH || d == PipeDirection.SOUTH;

                case PipeDirection.EASTWEST:
                    return d == PipeDirection.EAST || d == PipeDirection.WEST;

                case PipeDirection.ANY:
                    return d == PipeDirection.NORTH || d == PipeDirection.SOUTH || d == PipeDirection.EAST || d == PipeDirection.WEST;
            }
            return false;
        }

        public PipeDirection GetOneDirection()
        {
            switch (GetDirection())
            {
                case PipeDirection.ANY:
                case PipeDirection.NORTH:
                case PipeDirection.NORTHSOUTH:
                    return PipeDirection.NORTH;

                case PipeDirection.EAST:
                case PipeDirection.EASTWEST:
                    return PipeDirection.EAST;

                case PipeDirection.SOUTH:
                    return PipeDirection.SOUTH;

                case PipeDirection.WEST:
                    return PipeDirection.WEST;
            }
            throw new Exception();
        }

        public PipeDirection GetRealDirection()
        {
            return GetDirection();
        }

        protected abstract PipeDirection GetDirection();

        public static Vec2i GetVectorForDirection(PipeDirection direction)
        {
            Vec2i delta = Vec2i.Zero;

            switch (direction)
            {
                case PipeDirection.NORTH:
                    delta.Y = 1;
                    break;

                case PipeDirection.EAST:
                    delta.X = 1;
                    break;

                case PipeDirection.SOUTH:
                    delta.Y = -1;
                    break;

                case PipeDirection.WEST:
                    delta.X = -1;
                    break;
            }
            return delta;
        }

        public virtual double GetSpeed()
        {
            return PIPESPEED_NORMAL;
        }

        public abstract bool CanEnter();
    }
}