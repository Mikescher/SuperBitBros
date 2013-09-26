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

        private static void Main(string[] args)
        {
            debugFlySwitch.TurnOnEvent += delegate { Console.Out.WriteLine("DebugFlyMode enabled"); };
            debugFlySwitch.TurnOffEvent += delegate { Console.Out.WriteLine("DebugFlyMode disabled"); };

            debugFlyoverrideSwitch.TurnOnEvent += delegate { if (debugFlySwitch.Value) Console.Out.WriteLine("DebugFlyMode++ enabled"); };
            debugFlyoverrideSwitch.TurnOffEvent += delegate { if (debugFlySwitch.Value) Console.Out.WriteLine("DebugFlyMode++ disabled"); };


            Console.Out.WriteLine("START");
            GameModel world = new GameWorld(2, 2);
            OpenGLView view = new OpenGLGameView(world);

            Textures.Load();

            world.Init();

            view.Start(48, 60);
        }
    }
}