using System;
using System.Collections.Generic;
using OpenTK.Input;

namespace SuperBitBros.OpenGL
{
    public enum KeyTriggerMode { ON_DOWN, ON_UP, WHILE_DOWN, WHILE_UP, FLICKER_DOWN, FLICKER_UP, NEVER, INSTANT }

    public class BooleanKeySwitch
    {
        private static List<BooleanKeySwitch> switchList = new List<BooleanKeySwitch>();


        private bool _Value;

        public bool Value
        {
            get
            {
                return _Value;
            }
            private set
            {
                if (_Value ^ value && value)
                    if (TurnOnEvent != null) TurnOnEvent();
                if (_Value ^ value && !value)
                    if (TurnOnEvent != null) TurnOffEvent();
                _Value = value;
            }
        }

        private bool lastState = false;

        private Key key;
        private KeyTriggerMode mode;

        public delegate void TurnEventDelegate();

        public event TurnEventDelegate TurnOnEvent;

        public event TurnEventDelegate TurnOffEvent;

        public BooleanKeySwitch(bool initial, Key actkey, KeyTriggerMode tmode)
        {
            _Value = initial;
            key = actkey;

            mode = tmode;

            Console.Out.WriteLine("Register Boolean KeySwitch [{0}] on {1}", actkey, tmode);
            switchList.Add(this);
        }

        public BooleanKeySwitch(bool initial, Key actkey)
            : this(initial, actkey, KeyTriggerMode.ON_DOWN)
        {
        }

        public BooleanKeySwitch(Key actkey)
            : this(false, actkey, KeyTriggerMode.ON_DOWN)
        {
        }

        public void Switch()
        {
            Value = !Value;
        }

        public void Turn(bool state)
        {
            Value = state;
        }

        public static void UpdateAll(KeyboardDevice keyboard) {
            switchList.ForEach( x => x.Update(keyboard));
        }

        private void Update(KeyboardDevice keyboard)
        {
            bool newstate = keyboard[key];

            switch (mode)
            {
                case KeyTriggerMode.ON_DOWN:
                    if (!lastState && newstate) Switch();
                    break;

                case KeyTriggerMode.ON_UP:
                    if (lastState && !newstate) Switch();
                    break;

                case KeyTriggerMode.WHILE_DOWN:
                    Turn(newstate);
                    break;

                case KeyTriggerMode.WHILE_UP:
                    Turn(!newstate);
                    break;

                case KeyTriggerMode.FLICKER_DOWN:
                    Turn(!lastState && newstate);
                    break;

                case KeyTriggerMode.FLICKER_UP:
                    Turn(lastState && !newstate);
                    break;

                case KeyTriggerMode.NEVER:
                    //never
                    break;

                case KeyTriggerMode.INSTANT:
                    if (newstate) Switch();
                    break;
            }

            lastState = newstate;
        }
    }
}