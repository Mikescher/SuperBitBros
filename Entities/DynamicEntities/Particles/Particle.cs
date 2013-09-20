﻿using OpenTK.Input;
using SuperBitBros.OpenGL;

namespace SuperBitBros.Entities.DynamicEntities.Particles
{
    public abstract class Particle : DynamicEntity
    {
        private static BooleanKeySwitch debugParticleSwitch = new BooleanKeySwitch(false, Key.F7, KeyTriggerMode.ON_DOWN);

        public static int MAX_PARTICLE_COUNT 
        { 
            get 
            {
                return debugParticleSwitch.Value ? 0 : 100;
            } 
        }

        protected static int GlobalParticleCount = 0;

        private double lifetime = -1;
        private int fadetime_max = 0;
        private double fadetime = 0;

        private double transparency = 1.0;

        private bool doInstantKill = false;

        public Particle()
            : base()
        {
            distance = DISTANCE_PARTICLES;

            if (IsPureOptical())
            {
                GlobalParticleCount++;
                if (GlobalParticleCount > MAX_PARTICLE_COUNT)
                    doInstantKill = true;
            }
        }

        protected void SetLifetime(int t)
        {
            lifetime = t;
        }

        protected void SetFadetime(int t)
        {
            fadetime = t;
            fadetime_max = t;
        }

        public override void OnAdd(GameModel model)
        {
            base.OnAdd(model);

            if (doInstantKill)
                KillLater();
        }

        public override void OnRemove()
        {
            base.OnRemove();

            if (IsPureOptical())
            {
                GlobalParticleCount--;
            }
        }

        public static int GetGlobalParticleCount()
        {
            return GlobalParticleCount;
        }

        public override void Update(KeyboardDevice keyboard, double ucorrection)
        {
            base.Update(keyboard, ucorrection);

            if (lifetime > 0)
            {
                lifetime -= ucorrection;
            }
            else if (lifetime == 0)
            {
                if (fadetime > 0)
                {
                    fadetime -= ucorrection;
                    transparency = fadetime / fadetime_max;
                }
                else
                {
                    KillLater();
                }
            }
        }

        protected override bool IsBlockingOther(Entity sender)
        {
            return false;
        }

        public override double GetTransparency()
        {
            return transparency;
        }

        protected virtual bool IsPureOptical() // true := Wird bei zuviel Partikeln nicht angezeigt
        {
            return true;
        }
    }
}
