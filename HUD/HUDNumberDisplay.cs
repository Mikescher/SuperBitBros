using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace SuperBitBros.HUD
{
    public class HUDNumberDisplay : HUDElement
    {
        public int Layer = 0;
        private int _Value = 0;
        public int Value 
        { 
            get 
            {
                return _Value;
            } 
            set 
            {
                if (_Value != value)
                {
                    _Value = value;
                    UpdateTexture();
                }
            } 
        }

        public HUDNumberDisplay(int layer, int w, int h)
        {
            Layer = layer;
            width = w;
            height = h;

            UpdateTexture();
        }

        private void UpdateTexture()
        {
            texture = Textures.number_sheet.GetTextureWrapper(Value, Layer);
        }

        public override void Update(KeyboardDevice keyboard, double ucorrection)
        {

        }
    }
}
