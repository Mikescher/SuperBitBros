using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.HUD
{
    public class StandardGameHUD : HUDModel
    {   
        private const int COIN_PARTICLE_COMPLETIONCOUNT = CoinEntity.COIN_EXPLOSIONFRAGMENTS_X * CoinEntity.COIN_EXPLOSIONFRAGMENTS_Y;

        private BooleanKeySwitch debugCoinCheatSwitch = new BooleanKeySwitch(false, Key.P, KeyTriggerMode.FLICKER_DOWN);

        private int coinParticleCount = 0;

        private HUDAnimatedImage coinImage;
        private HUDNumberDisplay coinNumberDisplay_1;
        private HUDNumberDisplay coinNumberDisplay_2;

        private HUDNumberCounter coinCounter;

        public StandardGameHUD(GameWorld model)
            : base(model)
        {

        }

        public Vec2d GetCoinTarget()
        {
            return new Vec2d(10 + Block.BLOCK_WIDTH / 2, owner.viewPortHeight - 10 - Block.BLOCK_HEIGHT / 2);
        }

        public void AddCoinParticle()
        {
            coinParticleCount++;
            if (coinParticleCount >= COIN_PARTICLE_COMPLETIONCOUNT)
            {
                coinParticleCount = 0;
                coinCounter.Value++; 
            }
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (debugCoinCheatSwitch.Value) 
            { 
                coinCounter.Value++; 
            }
        }

        public override void CreateHUD()
        {
            Add(coinImage = new HUDAnimatedImage(15, Textures.array_coin, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT), 10, 10, HUDElementAlign.HEA_TL);
            Add(coinNumberDisplay_1 = new HUDNumberDisplay(0, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT), 30, 10, HUDElementAlign.HEA_TL);
            Add(coinNumberDisplay_2 = new HUDNumberDisplay(0, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT), 50, 10, HUDElementAlign.HEA_TL);

            coinCounter = new HUDNumberCounter();
            coinCounter.AddCounter(coinNumberDisplay_1);
            coinCounter.AddCounter(coinNumberDisplay_2);
        }
    }
}
