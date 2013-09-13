using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace SuperBitBros.OpenGL {
    abstract class OpenGLView {
        private const int RESOLUTION_WIDTH = 540;
        private const int RESOLUTION_HEIGHT = 280;

        protected GameWindow window;
        protected GameModel model;

        public OpenGLView(GameModel model) {
            window = new GameWindow(RESOLUTION_WIDTH, RESOLUTION_HEIGHT, GraphicsMode.Default, "title");
            this.model = model;

            window.RenderFrame += new EventHandler<FrameEventArgs>(OnRender);
            window.UpdateFrame += new EventHandler<FrameEventArgs>(OnUpdate);

            OnInit();
        }

        public virtual void Start(int fps, int ups) {
            window.Run(fps, ups);
        }

        private void OnInit() {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.Enable(EnableCap.Texture2D);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.ClearColor(Color4.Black);
        }

        protected abstract void OnRender(object sender, EventArgs e);

        protected virtual void OnUpdate(object sender, EventArgs e) {
            model.Update(window.Keyboard);
        }

        protected virtual void StartRender() {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.LoadIdentity();
            GL.Ortho(0, RESOLUTION_WIDTH, 0, RESOLUTION_HEIGHT, 0, -100);
        }

        protected virtual void EndRender() {
            window.SwapBuffers();
        }

        protected virtual void RenderRectangle(Vector2d tl, Vector2d bl, Vector2d br, Vector2d tr, double distance) {
            RenderRectangle(new Rectangle2d(bl, tr), distance);
        }

        protected virtual void RenderRectangle(Rectangle2d rect, double distance) {
            GL.Begin(BeginMode.Polygon);
            GL.Vertex3(rect.tl.X, rect.tl.Y, distance);
            GL.Vertex3(rect.bl.X, rect.bl.Y, distance);
            GL.Vertex3(rect.br.X, rect.br.Y, distance);
            GL.Vertex3(rect.tr.X, rect.tr.Y, distance);
            GL.End();
        }

        protected virtual void RenderRectangle(Rectangle2d rect, OGLTexture texture, double distance) {
            Rectangle2d coords = texture.GetCoordinates();
            texture.bind();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat);

            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(coords.bl);
            GL.Vertex3(rect.tl.X, rect.tl.Y, distance);
            GL.TexCoord2(coords.tl);
            GL.Vertex3(rect.bl.X, rect.bl.Y, distance);
            GL.TexCoord2(coords.tr);
            GL.Vertex3(rect.br.X, rect.br.Y, distance);
            GL.TexCoord2(coords.br);
            GL.Vertex3(rect.tr.X, rect.tr.Y, distance);
            GL.End();
        }
    }
}