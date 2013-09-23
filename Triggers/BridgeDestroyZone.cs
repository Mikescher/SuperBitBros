using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Drawing;

namespace SuperBitBros.Triggers
{
    public class BridgeDestroyZone : Trigger
    {
        private bool active = true;

        public BridgeDestroyZone(Vec2i pos)
            : base(pos)
        {

        }

        public override void OnCollide(DynamicEntity collider)
        {
            Random r = new Random();

            Player p = collider as Player;
            if (p != null && active)
            {

                foreach (Trigger t in p.owner.triggerList)
                {
                    if (t is BridgeDestroyZone)
                        (t as BridgeDestroyZone).active = false;
                }

                foreach (DynamicEntity e in p.owner.GetCurrentEntityList())
                {
                    if (e is BridgeEntity)
                    {
                        p.owner.AddDelayedAction((int)(r.NextDouble() * 60), () => e.KillLater());
                    }
                }

                p.WalkToLevelEnd();
            }
        }

        public static Color color = Color.FromArgb(255, 0, 128);

        public static Color GetColor()
        {
            return color;
        }

        public override Color GetTriggerColor()
        {
            return GetColor();
        }
    }
}
