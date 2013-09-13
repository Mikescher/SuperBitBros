using System;

namespace SuperBitBros.OpenGL {
    class Program {
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
