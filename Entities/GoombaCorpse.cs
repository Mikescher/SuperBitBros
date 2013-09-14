﻿
using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
namespace SuperBitBros.Entities {
    class GoombaCorpse : DynamicEntity {
        public GoombaCorpse() {
            distance = Entity.DISTANCE_CORPSE;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_goomba_dead;
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            DoGravitationalMovement(Vector2d.Zero);
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }
    }
}
