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
            Gameworld world = new Gameworld();
            OpenGLView view = new OpenGLGameView(world);

            view.Start();
        }
    }
}
