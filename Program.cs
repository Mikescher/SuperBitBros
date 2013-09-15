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

            view.Start(60, 120);
        }
    }
}

//TODO Offset
//TODO Replace All Vector with own things (add VectorInt where possible)
//TODO Trigegr and PipeZones