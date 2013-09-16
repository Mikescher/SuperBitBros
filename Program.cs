using System;

namespace SuperBitBros {

    public class Program {
        public static bool IS_DEBUG = true;

        private static void Main(string[] args) {
            Console.Out.WriteLine("START");
            GameWorld world = new GameWorld();
            OpenGLView view = new OpenGLGameView(world);

            Textures.Load();

            world.LoadMapFromResources();

            view.Start(60, 60);
        }
    }
}

//TODO Performance 4 Fullscreen
//TODO Trigegr and PipeZones