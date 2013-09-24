using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class CoinBoxBlock : BoxBlock
    {
        private const double COIN_SPAWN_FORCE = 3;

        public static Color color = Color.FromArgb(0, 0, 255);

        public static Color GetColor()
        {
            return color;
        }

        public override void OnActivate(Player p)
        {
            owner.AddEntity(new GravityCoinEntity(new Vec2d(0, COIN_SPAWN_FORCE)), GetTopLeft().X, GetTopLeft().Y);
            Deactivate();
        }

        public override Color GetBlockColor()
        {
            return color;
        }
    }
}