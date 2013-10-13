using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace SuperBitBros.Triggers
{
    public class SwitchZoomZone : Trigger
    {
        private int active = 0;

        public SwitchZoomZone(Vec2i pos)
            : base(pos)
        {
            //--
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (active > 0)
                active--;
        }

        public override void OnCollide(DynamicEntity collider)
        {
            Player p = collider as Player;

            if (p != null && active <= 0)
            {
                owner.ownerView.switchZoom();
                deactivate();
            }
        }

        public void deactivate()
        {
            SwitchZoomZone[] z = new SwitchZoomZone[9];

            z[0] = owner.getReliableTriggerList(position.X, position.Y).Find(x => x is SwitchZoomZone) as SwitchZoomZone;

            z[1] = owner.getReliableTriggerList(position.X + 1, position.Y).Find(x => x is SwitchZoomZone) as SwitchZoomZone;
            z[2] = owner.getReliableTriggerList(position.X, position.Y + 1).Find(x => x is SwitchZoomZone) as SwitchZoomZone;
            z[3] = owner.getReliableTriggerList(position.X - 1, position.Y).Find(x => x is SwitchZoomZone) as SwitchZoomZone;
            z[4] = owner.getReliableTriggerList(position.X, position.Y - 1).Find(x => x is SwitchZoomZone) as SwitchZoomZone;

            z[5] = owner.getReliableTriggerList(position.X + 1, position.Y + 1).Find(x => x is SwitchZoomZone) as SwitchZoomZone;
            z[6] = owner.getReliableTriggerList(position.X + 1, position.Y - 1).Find(x => x is SwitchZoomZone) as SwitchZoomZone;
            z[7] = owner.getReliableTriggerList(position.X - 1, position.Y + 1).Find(x => x is SwitchZoomZone) as SwitchZoomZone;
            z[8] = owner.getReliableTriggerList(position.X - 1, position.Y - 1).Find(x => x is SwitchZoomZone) as SwitchZoomZone;

            for (int i = 0; i < 9; i++)
            {
                if (z[i] != null)
                    z[i].active = 60;
            }
        }

        public static Color color = Color.FromArgb(0, 255, 255);

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