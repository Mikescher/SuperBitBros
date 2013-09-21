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

        private static void Main(string[] args)
        {


            Console.Out.WriteLine("START");
            GameModel world = new GameWorld(1, 1);
            OpenGLView view = new OpenGLGameView(world);

            Textures.Load();

            world.Init();

            view.Start(48, 60);
        }
    }
}

//TODO Add Mushroom / Star
//Dont walk out of X-Level bounds