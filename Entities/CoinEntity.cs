
using System;
using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Input;
using SuperBitBros.OpenGL.Entities.Blocks;
namespace SuperBitBros.OpenGL.Entities {
    class CoinEntity : AnimatedDynamicEntity {
        public const double PLAYER_SPEED_FRICTION = 0.1;

        private double spawnForce;
        private bool isBouncing;

        public CoinEntity(double cSpawnForce, bool cIsBounce = false) {
            this.spawnForce = cSpawnForce;
            this.isBouncing = cIsBounce;

            distance = Entity.DISTANCE_POWERUPS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            atexture.animation_speed = 15;

            atexture.Add(0, Textures.texture_coin_0);
            atexture.Add(0, Textures.texture_coin_1);
            atexture.Add(0, Textures.texture_coin_2);
            atexture.Add(0, Textures.texture_coin_3);
        }

        public override void OnAdd(GameModel owner) {
            base.OnAdd(owner);
            movementDelta.Y = spawnForce;
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            Vector2d delta = new Vector2d(0, 0);

            if (IsOnGround())
            {
                delta.X = -Math.Sign(movementDelta.X) * Math.Min(PLAYER_SPEED_FRICTION, Math.Abs(movementDelta.X));
            }

            if (isBouncing)
            {
                updateGravitationalMovement(delta, true, false);

                if (IsOnCeiling()) 
                    movementDelta.Y = Math.Min(movementDelta.Y, 0);
                if (IsOnGround())
                {
                    if (movementDelta.Y < 0.25)
                        movementDelta.Y = - movementDelta.Y * (2/3.0);
                    else
                        movementDelta.Y = Math.Max(movementDelta.Y, 0);
                }
                    
            }
            else
            {
                updateGravitationalMovement(delta);
            }
            

            atexture.Update();
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision) {
            if (collidingEntity.GetType() == typeof(Player) && isDirectCollision) {
                owner.RemoveEntity(this);
            }
        }
    }
}
