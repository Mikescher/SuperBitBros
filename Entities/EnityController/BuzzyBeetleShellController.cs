using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    class BuzzyBeetleShellController : AbstractNewtonEntityController
    {
        protected bool doSlide = false;
        protected int slideDirection = -1; // +1 ||-1 || 0

        public const double SHELL_ACC = 0.7;
        public const double SHELL_SPEED = 4;

        public BuzzyBeetleShellController(BuzzyBeetleShell e)
            : base(e)
        {
            //---
        }

        public override void Update(KeyboardDevice keyboard)
        {
            Vec2d delta = new Vec2d(0, 0);

            if ((slideDirection > 0 && ent.IsCollidingRight(typeof(Mob))) || (slideDirection < 0 && ent.IsCollidingLeft(typeof(Mob))))
            {
                slideDirection *= -1;
                ent.IsCollidingRight(typeof(Mob));
            }

            if ((slideDirection < 0 && movementDelta.X > -SHELL_SPEED) || (slideDirection > 0 && movementDelta.X < SHELL_SPEED))
            {
                delta.X = SHELL_ACC * slideDirection;
            }

            if (!doSlide)
                delta.X = -movementDelta.X;

            DoGravitationalMovement(delta);
        }

        public void ToogleSlide()
        {
            doSlide = !doSlide;
        }

        public bool isStill()
        {
            return !doSlide;
        }

        public void DoSlide(double direction)
        {
            doSlide = true;
            slideDirection = Math.Sign(direction);
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
