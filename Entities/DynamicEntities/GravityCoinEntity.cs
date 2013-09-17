using SuperBitBros.Entities.EnityController;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.DynamicEntities
{
    public class GravityCoinEntity : CoinEntity
    {
        public GravityCoinEntity(Vec2d spawnForce, bool isBounce = false)
            : base()
        {
            AddController(isBounce ? (new BouncingCoinController(this, spawnForce)) : (new CoinController(this, spawnForce)));
        }
    }
}