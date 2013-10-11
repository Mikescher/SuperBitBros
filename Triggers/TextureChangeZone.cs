using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Triggers
{
    public class TextureChangeZone : Trigger
    {
        private const int ANY = 0;

        private int texID;

        public TextureChangeZone(Vec2i pos, int ident)
            : base(pos)
        {
            texID = ident;
        }

        public override void OnCollide(DynamicEntity collider)
        {
            Player p = collider as Player;
            if (p != null)
            {
                Textures.block_textures.ChangeReference(Textures.texturePacks[texID]);
            }
        }

        public static Color color = Color.FromArgb(200, ANY, 0);

        public static Color GetColor()
        {
            return color;
        }

        public override Color GetTriggerColor()
        {
            return GetColor();
        }
    }
}
