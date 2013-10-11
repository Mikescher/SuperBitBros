
namespace SuperBitBros.OpenGL
{
    public class OGLReferenceTextureSheet : OGLTextureSheet
    {
        private OGLTextureSheet reference;

        private OGLReferenceTextureSheet(OGLTextureSheet refsheet, int w, int h)
            : base(-1, w, h)
        {
            this.reference = refsheet;
        }

        public static OGLReferenceTextureSheet LoadTextureFromReference(OGLTextureSheet r, int width, int height)
        {
            return new OGLReferenceTextureSheet(r, width, height);
        }

        public override int GetID()
        {
            return reference.GetID();
        }

        public void ChangeReference(OGLTextureSheet nref)
        {
            reference = nref;
        }
    }
}