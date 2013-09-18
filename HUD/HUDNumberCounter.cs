using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.HUD
{
    class HUDNumberCounter
    {
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
                    UpdateCounter();
                }
            } 
        }
        private List<HUDNumberDisplay> counterList = new List<HUDNumberDisplay>();

        public HUDNumberCounter()
            : base()
        {
            
        }

        public void AddCounter(HUDNumberDisplay c)
        {
            counterList.Add(c);

            UpdateCounter();
        }

        private void UpdateCounter() 
        {
            for (int i = 0; i < counterList.Count; i++)
            {
                int p = counterList.Count - i - 1;
                counterList[p].Value = (Value % (int)Math.Pow(10, i + 1)) / (int)Math.Pow(10, i);
            }
        }
    }
}
