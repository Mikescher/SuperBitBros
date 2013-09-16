using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;
using System;
using System.Collections.Generic;

namespace SuperBitBros.Entities.EnityController {

    public class PipePlayerController : AbstractEntityController {
        public const double PIPECORRECTIONSPEED = 0.5;

        private PipeDirection direction;
        private bool hasConnected = false;
        private bool hasFinished = false;

        public PipePlayerController(Player p, PipeDirection initDirection)
            : base(p) {
            this.direction = initDirection;
        }

        public override void Update(KeyboardDevice keyboard) {
            PipeZone zone = GetUnderlyingZone();

            if (zone == null) {
                if (hasConnected)
                    hasFinished = true;
            } else {
                hasConnected = true;
                if (!zone.IsDirection(direction)) {
                    direction = zone.GetOneDirection();
                }
            }

            Vec2d delta = PipeZone.GetVectorForDirection(direction);

            if (direction == PipeDirection.SOUTH || direction == PipeDirection.NORTH) {
                double corr = GetXCorrection();
                Console.Out.WriteLine("CorrX:" + corr);
                delta.X += Math.Min(Math.Abs(corr), PIPECORRECTIONSPEED) * Math.Sign(corr);
            }

            if (direction == PipeDirection.EAST || direction == PipeDirection.WEST) {
                double corr = GetYCorrection();
                Console.Out.WriteLine("CorrY:" + corr);
                delta.Y += Math.Min(Math.Abs(corr), PIPECORRECTIONSPEED) * Math.Sign(corr);
            }

            delta *= 3.3;

            ent.position += delta;
        }

        private PipeZone GetUnderlyingZone() {
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

            return null;
        }

        private double GetXCorrection() {
            Vec2i blockPos = (Vec2i)(ent.GetMiddle() / Block.BLOCK_SIZE);
            int lowest = blockPos.X;
            int highest = blockPos.X;

            while (ContainsPipeZone(owner.getTriggerList(lowest - 1, blockPos.Y), direction))
                lowest--;

            while (ContainsPipeZone(owner.getTriggerList(highest + 1, blockPos.Y), direction))
                highest++;

            highest++;

            double pos = (lowest + (highest - lowest) / 2.0) * Block.BLOCK_WIDTH;

            return pos - ent.GetMiddle().X;
        }

        private double GetYCorrection() {
            Vec2i blockPos = (Vec2i)(ent.GetMiddle() / Block.BLOCK_SIZE);
            int lowest = blockPos.Y;
            int highest = blockPos.Y;

            while (ContainsPipeZone(owner.getTriggerList(blockPos.X, lowest - 1), direction))
                lowest--;

            while (ContainsPipeZone(owner.getTriggerList(blockPos.X, highest + 1), direction))
                highest++;

            highest++;

            double pos = (lowest + (highest - lowest) / 2.0) * Block.BLOCK_HEIGHT;

            return pos - ent.GetMiddle().Y;
        }

        private bool ContainsPipeZone(List<Trigger> list, PipeDirection d) {
            if (list != null)
                foreach (Trigger t in list)
                    if (t is PipeZone && (t as PipeZone).IsDirection(d))
                        return true;
            return false;
        }

        public override bool IsActive() {
            return !hasFinished;
        }

        public override void OnIllegalIntersection(Entity other) {
            //ignore
        }


        public override void OnHide() {
            //empty
        }

        public override void OnReshow() {
            //empty
        }
    }
}