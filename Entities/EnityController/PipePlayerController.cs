using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;

namespace SuperBitBros.Entities.EnityController
{
    class PipePlayerController : AbstractEntityController
    {
        private PipeDirection direction;
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

            if (zone == null) {
                if (hasConnected) 
                    hasFinished = true;
            } else {
                hasConnected = true;
                if (zone.GetDirection() != direction)
                {
                    direction = zone.GetDirection();
                }
            }

            Vec2d delta = Vec2d.Zero;

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

            delta *= 3.3;

            ent.position += delta;
        }

        private PipeZone GetUnderlyingZone()
        {
            Vec2i blockpos = (Vec2i)(ent.position / Block.BLOCK_SIZE);

            List<Trigger> triggerlist = owner.getTriggerList(blockpos.X, blockpos.Y);
            if (triggerlist != null)
                foreach (Trigger t in owner.getTriggerList(blockpos.X, blockpos.Y))
                    return t as PipeZone; // returned null wen t != PipeZone

            return null;
        }

        public override bool IsActive()
        {
            return !hasFinished;
        }

        public override void OnIllegalIntersection(Entity other)
        {
            //ignore
        }
    }
}
