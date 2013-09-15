using System;

namespace SuperBitBros {
    class Program {
        public const bool IS_DEBUG = true;

        static void Main(string[] args) {
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