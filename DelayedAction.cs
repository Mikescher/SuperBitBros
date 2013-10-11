using System;

namespace SuperBitBros
{
    public class DelayedAction
    {
        private Action action;
        public int Delay { get; private set; }

        public DelayedAction(int countdown, Action a)
        {
            Delay = countdown;
            action = a;
        }

        public bool DecInvoke()
        {
            Delay--;
            if (Delay <= 0)
            {
                action.Invoke();
                return true;
            }
            return false;
        }
    }
}
