using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace SuperBitBros {
    class MyGameWindow : GameWindow {
        public MyGameWindow(int resX, int resY)
            : base(resX, resY, GraphicsMode.Default, "tite") {

        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
        }
    }
}
