using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Triggers.PipeZones {
    public class PipeZoneTypeWrapper {

        private Type type;

        public PipeZoneTypeWrapper(Type t) {
            this.type = t;
        }

        public PipeZone Get(Vec2i pos) {
            return (PipeZone)type.GetConstructor(new Type[] { typeof(Vec2i) }).Invoke(new Object[] { pos });
        }

        public bool IsSet() {
            return type != null;
        }
    }
}
