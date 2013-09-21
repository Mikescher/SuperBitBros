using System;

namespace SuperBitBros
{
    public class DelayedAction
    {
        private Action action;
        private int time;

        public DelayedAction(int countdown, Action a)
        {
            time = countdown;
            action = a;
        }

        public bool DecInvoke()
        {
            time--;
            if (time <= 0)
            {
                action.Invoke();
                return true;
            }
            return false;
        }
    }
}
