
using OpenTK.Input;
using SuperBitBros.OpenGL.OGLMath;
using System;
using SuperBitBros.Entities.EnityController;
namespace SuperBitBros.Entities {
    class GravityCoinEntity : CoinEntity {
        public GravityCoinEntity(Vec2d spawnForce, bool isBounce = false)
            : base() {
                AddController(isBounce ? (new BouncingCoinController(this, spawnForce)) : (new CoinController(this, spawnForce)));
        }
    }
}
