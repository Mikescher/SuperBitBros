using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace SuperBitBros.HUD
{
    public abstract class HUDModel
    {
        public List<HUDElement> elements { get; private set; }

        public HUDModel()
        {
            elements = new List<HUDElement>();

            CreateHUD();
        }

        public void Add(HUDElement el, double x, double y, HUDElementAlign align = HUDElementAlign.HEA_BL)
        {
            el.SetPosition(align, x, y);
            elements.Add(el);
        }

        public virtual void Update(KeyboardDevice keyboard)
        {
            foreach (HUDElement e in elements)
            {
                e.Update(keyboard);
            }
        }

        public abstract void CreateHUD();
    }
}
