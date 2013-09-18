using System;
using OpenTK.Input;
using SuperBitBros.OpenGL;

namespace SuperBitBros
{
    public class Program
    {
        public static BooleanKeySwitch debugViewSwitch = new BooleanKeySwitch(true, Key.F10, KeyTriggerMode.ON_DOWN);
        public static BooleanKeySwitch minimapViewSwitch = new BooleanKeySwitch(true, Key.F9, KeyTriggerMode.ON_DOWN);

        private static void Main(string[] args)
        {
            Console.Out.WriteLine("START");
            GameModel world = new GameWorld();
            OpenGLView view = new OpenGLGameView(world);

            Textures.Load();

            world.Init();

            view.Start(60, 60);
        }
    }
}

//TODO Performance 4 Fullscreen (Coll Chunks)