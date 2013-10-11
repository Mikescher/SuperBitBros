using OpenTK.Input;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.HUD;
using SuperBitBros.OpenGL.OGLMath;
using System;

namespace SuperBitBros.Entities.EnityController
{
    enum MovementSection { SLIDE, WALK, FINSIHED }

    public class FlagPlayerController : AbstractNewtonEntityController
    {
        private const double SPEED_DOWN = 1.0;
        private const double SPEED_WALK = 0.75;

        private const double DOWN_CORRECTION_SPEED = 0.2;

        private MovementSection mode = MovementSection.SLIDE;
        private Vec2d flagPos;

        public FlagPlayerController(Player p, Vec2d p_flagPos)
            : base(p)
        {
            flagPos = p_flagPos;
        }

        public override void Update(KeyboardDevice keyboard)
        {
            switch (mode)
            {
                case MovementSection.SLIDE:
                    if (!ent.IsOnGround())
                    {
                        double X = flagPos.X - ent.position.X;
                        X = Math.Min(Math.Abs(X), DOWN_CORRECTION_SPEED) * Math.Sign(X);
                        MoveBy(new Vec2d(X, -SPEED_DOWN));
                        ((StandardGameHUD)owner.HUD).AddCoinParticle(StandardGameHUD.COIN_PARTICLE_COMPLETIONCOUNT / 2);
                    }
                    else
                        mode = MovementSection.WALK;
                    break;

                case MovementSection.WALK:
                    DoGravitationalMovement(new Vec2d(SPEED_WALK - movementDelta.X, 0));
                    break;
                default:
                    ent.DoCollisions();
                    break;
            }
        }

        public override bool IsActive()
        {
            return mode != MovementSection.FINSIHED;
        }

        public override void OnIllegalIntersection(Entity other)
        {
            //ignore
        }

        public override void OnHide()
        {
            //empty
        }

        public override void OnReshow()
        {
            //empty
        }
    }
}