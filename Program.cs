using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace SuperBitBros
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWorld world = new GameWorld();
            OpenGLView view = new OpenGLGameView(world);

            Textures.Load();

            world.LoadMapFromResources();

            view.Start(60, 120);
        }
    }
}
