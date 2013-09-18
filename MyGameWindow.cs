using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using QuickFont;

namespace SuperBitBros
{
    public class MyGameWindow : GameWindow
    {
        private OpenGLView owner;

        public MyGameWindow(OpenGLView v, int resX, int resY)
            : base(resX, resY, GraphicsMode.Default, "tite")
        {
            owner = v;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            QFont.InvalidateViewport();

            owner.OnResize();
        }
    }
}