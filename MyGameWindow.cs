using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using QuickFont;
using System;

namespace SuperBitBros {

    public class MyGameWindow : GameWindow {

        public MyGameWindow(int resX, int resY)
            : base(resX, resY, GraphicsMode.Default, "tite") {
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            QFont.InvalidateViewport();
        }
    }
}