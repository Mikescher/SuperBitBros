using SuperBitBros.OpenGL;

namespace SuperBitBros.Entities
{
    public abstract class AnimatedDynamicEntity : DynamicEntity
    {
        protected AnimatedTexture atexture;

        public AnimatedDynamicEntity()
            : base()
        {
            atexture = new AnimatedTexture();
        }

        public void UpdateAnimation(double ucorrection)
        {
            atexture.Update(ucorrection);
        }

        public override OGLTexture GetCurrentTexture()
        {
            return atexture.GetTexture();
        }
    }
}