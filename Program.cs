using OpenTK.Input;
using SuperBitBros.OpenGL;
using System;

namespace SuperBitBros
{

    public class Program
    {
        public static BooleanKeySwitch debugViewSwitch = new BooleanKeySwitch(true, Key.F10, KeyTriggerMode.ON_DOWN);
        public static BooleanKeySwitch vectorDebugViewSwitch = new BooleanKeySwitch(true, Key.F9, KeyTriggerMode.ON_DOWN);
        public static BooleanKeySwitch minimapViewSwitch = new BooleanKeySwitch(false, Key.F8, KeyTriggerMode.ON_DOWN);

        public static BooleanKeySwitch debugFlySwitch = new BooleanKeySwitch(false, Key.LControl, KeyTriggerMode.WHILE_DOWN);
        public static BooleanKeySwitch debugFlyoverrideSwitch = new BooleanKeySwitch(false, Key.ShiftLeft, KeyTriggerMode.WHILE_DOWN);

        public static BooleanKeySwitch debugPlayerUpgrade = new BooleanKeySwitch(false, Key.F3, KeyTriggerMode.FLICKER_DOWN);
        public static BooleanKeySwitch debugCoinCheatSwitch = new BooleanKeySwitch(false, Key.F4, KeyTriggerMode.FLICKER_DOWN);
        public static BooleanKeySwitch debugExplosionSwitch = new BooleanKeySwitch(false, Key.F5, KeyTriggerMode.FLICKER_DOWN);
        public static BooleanKeySwitch debugMapExplosionSwitch = new BooleanKeySwitch(false, Key.F6, KeyTriggerMode.COOLDOWN_DOWN);
        public static BooleanKeySwitch debugParticleSwitch = new BooleanKeySwitch(false, Key.F7, KeyTriggerMode.ON_DOWN);

        private static void Main(string[] args)
        {
            debugFlySwitch.TurnOnEvent += delegate { Console.Out.WriteLine("DebugFlyMode enabled"); };
            debugFlySwitch.TurnOffEvent += delegate { Console.Out.WriteLine("DebugFlyMode disabled"); };

            debugFlyoverrideSwitch.TurnOnEvent += delegate { if (debugFlySwitch.Value) Console.Out.WriteLine("DebugFlyMode++ enabled"); };
            debugFlyoverrideSwitch.TurnOffEvent += delegate { if (debugFlySwitch.Value) Console.Out.WriteLine("DebugFlyMode++ disabled"); };


            Console.Out.WriteLine("START");
            GameModel world = new GameWorld(4, 2);
            OpenGLView view = new OpenGLGameView(world);

            Textures.Load();

            world.Init(null);

            view.Start(48, 60);
        }
    }
}

//TODO Add BigMario Crouching