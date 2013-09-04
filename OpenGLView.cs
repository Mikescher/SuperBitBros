using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace SuperBitBros
{
    abstract class OpenGLView
    {
        private const int RESOLUTION_WIDTH = 640;
        private const int RESOLUTION_HEIGHT = 480;

        protected GameWindow window;
        protected GameModel model;

        public OpenGLView(GameModel model)
        {
            window = new GameWindow(RESOLUTION_WIDTH, RESOLUTION_HEIGHT, GraphicsMode.Default, "title");
            this.model = model;

            window.RenderFrame += new EventHandler<FrameEventArgs>(OnRender);

            OnInit(); 
        }

        public virtual void Start()
        {
            window.Run();
        }

        private void OnInit() {
            GL.Ortho(0, RESOLUTION_WIDTH, 0, RESOLUTION_HEIGHT, 0, -100);
        }

        protected abstract void OnRender(object sender, EventArgs e);

        protected virtual void StartRender()
        {
            GL.ClearColor(Color4.DarkBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        protected virtual void EndRender()
        {
            window.SwapBuffers();
        }

        protected virtual void renderTriangle(Vector2d a, Vector2d b, Vector2d c, double distance)
        {
            GL.Begin(BeginMode.Triangles);
            GL.Vertex3(a.X, a.X, distance);
            GL.Vertex3(b.X, b.X, distance);
            GL.Vertex3(c.X, c.X, distance);
            GL.End();
        }

        protected virtual void renderRectangle(Vector2d a, Vector2d b, Vector2d c, Vector2d d, double distance)
        {
            GL.Begin(BeginMode.Polygon);
            GL.Vertex3(a.X, a.Y, distance);
            GL.Vertex3(b.X, b.Y, distance);
            GL.Vertex3(c.X, c.Y, distance);
            GL.Vertex3(d.X, d.Y, distance);
            GL.End();
        }
    }
}