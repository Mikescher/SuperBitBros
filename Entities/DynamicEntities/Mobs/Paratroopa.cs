﻿using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using System;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class Paratroopa : Mob
    {
        public const double KOOPA_ACC = 0.2;
        public const double KOOPA_SPEED = 1;

        private bool suppressExplosion = false;

        public Paratroopa()
            : base()
        {
            distance = Entity.DISTANCE_MOBS;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_paratroopa;

            AddController(new ParatroopaController(this));
        }

        public override void OnHeadJump(Entity e)
        {
            suppressExplosion = true;
            KillLater();
            owner.AddEntity(new Koopa(), position.X, position.Y);
        }

        public override void OnTouch(Entity e, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            Player p = e as Player;
            if (p != null)
                p.DoDeath(this);
        }

        protected override void OnKill()
        {
            base.OnKill();

            if (!suppressExplosion)
                Explode();
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}