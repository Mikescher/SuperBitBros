using Entities.SuperBitBros;
using SuperBitBros.Entities;

namespace SuperBitBros.OpenGL.Entities {
    abstract class AnimatedDynamicEntity : DynamicEntity {

        protected AnimatedTexture atexture;

        public AnimatedDynamicEntity()
            : base() {

            atexture = new AnimatedTexture();
        }

        public void UpdateAnimation() {
            atexture.Update();
        }

        public override OGLTexture GetCurrentTexture() {
            return atexture.GetTexture();
        }
    }
}
