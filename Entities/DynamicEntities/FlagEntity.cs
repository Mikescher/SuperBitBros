using SuperBitBros.Entities.Blocks;
using System;
using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities
{
    internal class FlagEntity : DynamicEntity
    {
        private const double OTHER_FLAG_DETECTIONRANGE = 5.0;

        public bool IsStatic { get; private set; }

        public FlagEntity()
            : base()
        {
            IsStatic = false;
            distance = Entity.DISTANCE_STRUCTURES;
            width = Block.BLOCK_WIDTH;
            height = Block.BLOCK_HEIGHT;

            texture = Textures.texture_flag;

            AddController(new StaticEntityController(this));
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override void onCollide(Entity collidingEntity, bool isCollider, bool isBlockingMovement, bool isDirectCollision, bool isTouching)
        {
            Player p = collidingEntity as Player;
            if (p != null && isDirectCollision && !IsStatic)
            {
                for (int i = 0; i < owner.dynamicEntityList.Count; i++)
                {
                    FlagEntity fe = owner.dynamicEntityList[i] as FlagEntity;

                    if (fe != null && Math.Abs(fe.position.X - position.X) < OTHER_FLAG_DETECTIONRANGE)
                    {
                        fe.IsStatic = true;

                        if (fe.position.Y >= position.Y)
                        {
                            fe.KillLater();
                        }
                    }
                }

                p.OnCollideNonStaticFlag(position);
            }
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}