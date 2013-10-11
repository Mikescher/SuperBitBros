
namespace SuperBitBros.OpenGL
{
    public class OGLReferenceTextureFragment : OGLTextureFragment
    {
        private OGLTextureSheet reference;

        public OGLReferenceTextureFragment(OGLTextureSheet r, double x, double y, double w, double h)
            : base(-1, x, y, w, h)
        {
            this.reference = r;
        }

        public override int GetID()
        {
            return reference.GetID();
        }

    }
}