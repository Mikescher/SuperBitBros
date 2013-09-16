
using SuperBitBros.Entities;
using SuperBitBros.OpenGL.OGLMath;
namespace SuperBitBros.Triggers.PipeZones {
    public enum PipeDirection { NORTH, EAST, SOUTH, WEST }

    abstract class PipeZone : Trigger {
        public PipeZone(Vec2i pos)
            : base(pos) {
            //--
        }

        public override void OnCollide(DynamicEntity collider)
        {
            //nothing
        }

        public abstract PipeDirection GetDirection();
    }
}
