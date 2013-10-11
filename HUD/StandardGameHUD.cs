using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.Entities.DynamicEntities.Particles;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Diagnostics;

namespace SuperBitBros.HUD
{
    public class StandardGameHUD : HUDModel
    {
        private const int HEAD_EXPLOSIONFRAGMENTS_X = 5;
        private const int HEAD_EXPLOSIONFRAGMENTS_Y = 5;
        private const double HEAD_EXPLOSIONFRAGMENTS_FORCE = 48.0;

        public const int COIN_PARTICLE_COMPLETIONCOUNT = CoinEntity.COIN_EXPLOSIONFRAGMENTS_X * CoinEntity.COIN_EXPLOSIONFRAGMENTS_Y;
        public const int HEAD_PARTICLE_COMPLETIONCOUNT = HEAD_EXPLOSIONFRAGMENTS_X * HEAD_EXPLOSIONFRAGMENTS_Y;

        private Random random = new Random();

        private int coinParticleCount = 0;
        private HUDAnimatedImage coinImage;
        private HUDNumberDisplay coinNumberDisplay_1;
        private HUDNumberDisplay coinNumberDisplay_2;
        public HUDNumberCounter coinCounter;

        private int headParticleCount = 0;
        private HUDImage headImage;
        private HUDNumberDisplay headNumberDisplay_1;
        private HUDNumberDisplay headNumberDisplay_2;
        public HUDNumberCounter headCounter;

        private HUDNumberDisplay timeDisplay_1;
        private HUDNumberDisplay timeDisplay_2;
        private HUDNumberDisplay timeDisplay_3;
        private HUDNumberDisplay timeDisplay_4;
        private HUDNumberDisplay timeDisplay_5;
        private HUDNumberCounter timeCounter;
        public Stopwatch timeWatch = new Stopwatch();

        public StandardGameHUD(GameWorld model)
            : base(model)
        {
            timeWatch.Restart();
        }

        public Vec2d GetCoinTarget()
        {
            return new Vec2d(10 + Block.BLOCK_WIDTH / 2, owner.viewPortHeight - 10 - Block.BLOCK_HEIGHT / 2);
        }

        public Vec2d GetHeadTarget()
        {
            return new Vec2d(90 + Block.BLOCK_WIDTH / 2, owner.viewPortHeight - 10 - Block.BLOCK_HEIGHT / 2);
        }

        public void AddCoinParticle(int c)
        {
            coinParticleCount += c;
            if (coinParticleCount >= COIN_PARTICLE_COMPLETIONCOUNT)
            {
                coinParticleCount = 0;
                AddCoin();
            }
        }

        public void AddExplosionParticle(Particle p)
        {
            if (p is CoinExplosionParticle)
            {
                AddCoinParticle(1);
            }
            else if (p is HUDHeadExplosionParticle)
            {
                headParticleCount++;
                if (headParticleCount >= HEAD_PARTICLE_COMPLETIONCOUNT)
                {
                    headParticleCount = 0;
                    AddHead();
                }
            }
        }

        private void AddCoin()
        {
            coinCounter.Value++;
            if (coinCounter.Value >= 100)
            {
                coinCounter.Value -= 100;
                DoHeadExplode();
            }
        }

        private void AddHead()
        {
            if (headCounter.Value < 100)
                headCounter.Value++;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            base.Update(keyboard);

            if (Program.debugViewSwitch.Value && Program.debugCoinCheatSwitch.Value)
            {
                for (int i = 0; i < 10; i++)
                    AddCoin();
            }
            timeCounter.Value = (int)timeWatch.Elapsed.TotalSeconds;
        }

        public override void CreateHUD()
        {
            Add(coinImage = new HUDAnimatedImage(15, Textures.array_coin, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT), 10, 10, HUDElementAlign.HEA_TL);
            Add(coinNumberDisplay_1 = new HUDNumberDisplay(0, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT), 30, 10, HUDElementAlign.HEA_TL);
            Add(coinNumberDisplay_2 = new HUDNumberDisplay(0, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT), 50, 10, HUDElementAlign.HEA_TL);

            Add(headImage = new HUDImage(Textures.texture_mariohead, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT), 90, 10, HUDElementAlign.HEA_TL);
            Add(headNumberDisplay_1 = new HUDNumberDisplay(2, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT), 110, 10, HUDElementAlign.HEA_TL);
            Add(headNumberDisplay_2 = new HUDNumberDisplay(2, Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT), 130, 10, HUDElementAlign.HEA_TL);

            Add(timeDisplay_1 = new HUDNumberDisplay(1, Block.BLOCK_WIDTH / 2.0, Block.BLOCK_HEIGHT / 2.0), 40, 0, HUDElementAlign.HEA_TR);
            Add(timeDisplay_2 = new HUDNumberDisplay(1, Block.BLOCK_WIDTH / 2.0, Block.BLOCK_HEIGHT / 2.0), 30, 0, HUDElementAlign.HEA_TR);
            Add(timeDisplay_3 = new HUDNumberDisplay(1, Block.BLOCK_WIDTH / 2.0, Block.BLOCK_HEIGHT / 2.0), 20, 0, HUDElementAlign.HEA_TR);
            Add(timeDisplay_4 = new HUDNumberDisplay(1, Block.BLOCK_WIDTH / 2.0, Block.BLOCK_HEIGHT / 2.0), 10, 0, HUDElementAlign.HEA_TR);
            Add(timeDisplay_5 = new HUDNumberDisplay(1, Block.BLOCK_WIDTH / 2.0, Block.BLOCK_HEIGHT / 2.0), 00, 0, HUDElementAlign.HEA_TR);

            coinCounter = new HUDNumberCounter();
            coinCounter.AddCounter(coinNumberDisplay_1);
            coinCounter.AddCounter(coinNumberDisplay_2);

            headCounter = new HUDNumberCounter();
            headCounter.AddCounter(headNumberDisplay_1);
            headCounter.AddCounter(headNumberDisplay_2);

            timeCounter = new HUDNumberCounter();
            timeCounter.AddCounter(timeDisplay_1);
            timeCounter.AddCounter(timeDisplay_2);
            timeCounter.AddCounter(timeDisplay_3);
            timeCounter.AddCounter(timeDisplay_4);
            timeCounter.AddCounter(timeDisplay_5);
        }

        private void DoHeadExplode()
        {
            double forceMult = HEAD_EXPLOSIONFRAGMENTS_FORCE / (Math.Sqrt(Block.BLOCK_WIDTH * Block.BLOCK_WIDTH + Block.BLOCK_HEIGHT * Block.BLOCK_HEIGHT) / 2.0);

            for (int y = 0; y < HEAD_EXPLOSIONFRAGMENTS_Y; y++)
            {
                for (int x = 0; x < HEAD_EXPLOSIONFRAGMENTS_X; x++)
                {
                    owner.AddEntity(
                        new HUDHeadExplosionParticle(
                            owner as GameWorld,
                            x,
                            y,
                            HEAD_EXPLOSIONFRAGMENTS_X,
                            HEAD_EXPLOSIONFRAGMENTS_Y,
                            forceMult + (random.NextDouble() * 6 - 3),
                            this),
                        owner.GetOffset().X + GetCoinTarget().X,
                        owner.GetOffset().Y + GetCoinTarget().Y);
                }
            }
        }

        public void Reset()
        {
            coinCounter.Value = 0;
            coinParticleCount = 0;

            headCounter.Value = 0;
            headParticleCount = 0;

            timeWatch.Restart();
        }
    }
}
