using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    public class BowserController : AbstractNewtonEntityController
    {
        protected int walkDirection = -1; // +1 ||-1 || 0

        public const double MOB_ACC = 0.2;
        public const double MOB_SPEED = 0.75;
        public const double JUMP_POWER = 5.5;

        public bool active = false;

        private double borderLeft = Double.MaxValue;
        private double borderRight = Double.MinValue;

        Random rand = new Random();

        public BowserController(Bowser e)
            : base(e)
        {
            //---
        }

        public void OnAfterMapGen()
        {
            foreach (DynamicEntity e in ent.owner.dynamicEntityList)
            {
                if (e is BridgeEntity)
                {
                    borderLeft = Math.Min(borderLeft, e.position.X);
                    borderRight = Math.Max(borderRight, e.position.X + e.width);
                }
            }

            borderRight -= ent.width;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (!active)
            {
                Vec2d p_pos = (ent.owner as GameWorld).player.position;
                if (p_pos.X >= borderLeft && p_pos.X <= borderRight)
                    active = true;
                else
                    return;
            }

            Vec2d delta = new Vec2d(0, 0);

            if ((walkDirection > 0 && ent.IsCollidingRight()) || (walkDirection < 0 && ent.IsCollidingLeft()))
            {
                walkDirection *= -1;
            }
            else if (rand.Next() % 220 == 0)
            {
                walkDirection *= -1;
            }
            if (ent.position.X <= borderLeft)
                walkDirection = 1;
            if (ent.position.X >= borderRight)
                walkDirection = -1;

            if ((walkDirection < 0 && movementDelta.X > -MOB_SPEED) || (walkDirection > 0 && movementDelta.X < MOB_SPEED))
            {
                delta.X = MOB_ACC * walkDirection;
            }

            if (ent.IsOnGround())
            {
                if (rand.Next() % 60 == 0)
                {
                    delta.Y = JUMP_POWER;
                }
            }

            if ((ent.position.X <= borderLeft && delta.X < 0) || ((ent.position.X + ent.width) >= borderRight && delta.X > 0)) // stay on Bridge
            {
                delta.X = 0;
                movementDelta.X = 0;
            }

            DoGravitationalMovement(delta);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
