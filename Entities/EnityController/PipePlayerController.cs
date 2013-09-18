using System;
using System.Collections.Generic;
using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;

namespace SuperBitBros.Entities.EnityController
{
    public class PipePlayerController : AbstractEntityController
    {
        private const double PIPECORRECTION_SPEEDFACTOR = 0.5;
        private const double PIPE_ENTER_SPEED = 2.0;

        private PipeDirection direction;
        private double speed = PIPE_ENTER_SPEED;

        private Vec2d deltaCache = Vec2d.Zero;

        private bool hasConnected = false;
        private bool hasFinished = false;

        public PipePlayerController(Player p, PipeDirection initDirection)
            : base(p)
        {
            this.direction = initDirection;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            PipeZone zone = GetUnderlyingZone();

            if (zone == null)
            {
                if (hasConnected)
                    hasFinished = true;
            }
            else
            {
                hasConnected = true;
                if (!zone.IsDirection(direction))
                {
                    direction = zone.GetOneDirection();
                }
                speed = zone.GetSpeed();
            }

            Vec2d delta = PipeZone.GetVectorForDirection(direction);
            delta.SetLength(speed);

            if (direction == PipeDirection.SOUTH || direction == PipeDirection.NORTH)
            {
                double corr = GetXCorrection();
                delta.X += Math.Min(Math.Abs(corr), Math.Abs(speed * PIPECORRECTION_SPEEDFACTOR)) * Math.Sign(corr);
            }

            if (direction == PipeDirection.EAST || direction == PipeDirection.WEST)
            {
                double corr = GetYCorrection();
                delta.Y += Math.Min(Math.Abs(corr), Math.Abs(speed * PIPECORRECTION_SPEEDFACTOR)) * Math.Sign(corr);
            }

            ent.position += delta;

            deltaCache = delta;
        }

        private PipeZone GetUnderlyingZone()
        {
            Vec2i blockpos = (Vec2i)(ent.GetMiddle() / Block.BLOCK_SIZE);

            List<Trigger> triggerlist = owner.getTriggerList(blockpos.X, blockpos.Y);
            if (triggerlist != null)
                foreach (Trigger t in owner.getTriggerList(blockpos.X, blockpos.Y))
                    if (t is PipeZone)
                        return t as PipeZone;

            blockpos = (Vec2i)(ent.GetBottomLeft() / Block.BLOCK_SIZE);

            triggerlist = owner.getTriggerList(blockpos.X, blockpos.Y);
            if (triggerlist != null)
                foreach (Trigger t in owner.getTriggerList(blockpos.X, blockpos.Y))
                    if (t is PipeZone)
                        return t as PipeZone;

            blockpos = (Vec2i)(ent.GetTopRight() / Block.BLOCK_SIZE);

            triggerlist = owner.getTriggerList(blockpos.X, blockpos.Y);
            if (triggerlist != null)
                foreach (Trigger t in owner.getTriggerList(blockpos.X, blockpos.Y))
                    if (t is PipeZone)
                        return t as PipeZone;

            blockpos = (Vec2i)(ent.GetBottomRight() / Block.BLOCK_SIZE);

            triggerlist = owner.getTriggerList(blockpos.X, blockpos.Y);
            if (triggerlist != null)
                foreach (Trigger t in owner.getTriggerList(blockpos.X, blockpos.Y))
                    if (t is PipeZone)
                        return t as PipeZone;

            blockpos = (Vec2i)(ent.GetTopLeft() / Block.BLOCK_SIZE);

            triggerlist = owner.getTriggerList(blockpos.X, blockpos.Y);
            if (triggerlist != null)
                foreach (Trigger t in owner.getTriggerList(blockpos.X, blockpos.Y))
                    if (t is PipeZone)
                        return t as PipeZone;

            return null;
        }

        private double GetXCorrection()
        {
            Vec2i blockPos = (Vec2i)(ent.GetMiddle() / Block.BLOCK_SIZE);
            int lowest = blockPos.X;
            int highest = blockPos.X;

            if (! ContainsPipeZone(owner.getTriggerList(blockPos.X, blockPos.Y)))
                return 0;

            while (ContainsPipeZone(owner.getTriggerList(lowest - 1, blockPos.Y), direction))
                lowest--;

            while (ContainsPipeZone(owner.getTriggerList(highest + 1, blockPos.Y), direction))
                highest++;

            highest++;

            double pos = (lowest + (highest - lowest) / 2.0) * Block.BLOCK_WIDTH;

            return pos - ent.GetMiddle().X;
        }

        private double GetYCorrection()
        {
            Vec2i blockPos = (Vec2i)(ent.GetMiddle() / Block.BLOCK_SIZE);
            int lowest = blockPos.Y;
            int highest = blockPos.Y;

            if (!ContainsPipeZone(owner.getTriggerList(blockPos.X, blockPos.Y)))
                return 0;

            while (ContainsPipeZone(owner.getTriggerList(blockPos.X, lowest - 1), direction))
                lowest--;

            while (ContainsPipeZone(owner.getTriggerList(blockPos.X, highest + 1), direction))
                highest++;

            highest++;

            double pos = (lowest + (highest - lowest) / 2.0) * Block.BLOCK_HEIGHT;

            return pos - ent.GetMiddle().Y;
        }

        private bool ContainsPipeZone(List<Trigger> list, PipeDirection d)
        {
            if (list != null)
                foreach (Trigger t in list)
                    if (t is PipeZone && (t as PipeZone).IsDirection(d))
                        return true;
            return false;
        }

        private bool ContainsPipeZone(List<Trigger> list)
        {
            if (list != null)
                foreach (Trigger t in list)
                    if (t is PipeZone)
                        return true;
            return false;
        }

        public override bool IsActive()
        {
            return !hasFinished;
        }

        public override void OnIllegalIntersection(Entity other)
        {
            //ignore
        }

        public override Vec2d GetDelta()
        {
            return deltaCache;
        }

        public override void OnHide()
        {
            //empty
        }

        public override void OnReshow()
        {
            //empty
        }
    }
}