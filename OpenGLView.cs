using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using QuickFont;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros
{
    public abstract class OpenGLView
    {
        protected const int INIT_RESOLUTION_WIDTH = 540;
        protected const int INIT_RESOLUTION_HEIGHT = 280;

        protected MyGameWindow window;
        protected GameModel model;

        protected FrequencyCounter fps_counter = new FrequencyCounter();
        protected FrequencyCounter ups_counter = new FrequencyCounter();

        protected AvgDurationWatch render_watch = new AvgDurationWatch();
        protected AvgDurationWatch update_watch = new AvgDurationWatch();

        protected QFont DebugFont;

        public OpenGLView(GameModel model)
        {
            window = new MyGameWindow(INIT_RESOLUTION_WIDTH, INIT_RESOLUTION_HEIGHT);
            this.model = model;

            window.RenderFrame += new EventHandler<FrameEventArgs>(OnRender);
            window.UpdateFrame += new EventHandler<FrameEventArgs>(OnUpdate);

            window.Load += new EventHandler<EventArgs>(OnLoad);

            OnInit();
        }

        public virtual void Start(int fps, int ups)
        {
            window.Run(ups, fps);
        }

        protected void OnLoad(object sender, EventArgs e)
        {
            OnInit();
        }

        private void OnInit()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.Enable(EnableCap.Texture2D);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.ClearColor(Color4.Black);

            QFontBuilderConfiguration builderConfig = new QFontBuilderConfiguration(true);
            builderConfig.ShadowConfig.blurRadius = 1; //reduce blur radius because font is very small
            builderConfig.TextGenerationRenderHint = TextGenerationRenderHint.ClearTypeGridFit; //best render hint for this font
            DebugFont = new QFont(new Font("Arial", 8));
            DebugFont.Options.DropShadowActive = true;
        }

        protected virtual void OnRender(object sender, EventArgs e)
        {
            fps_counter.Inc();
        }

        protected virtual void OnUpdate(object sender, EventArgs e)
        {
            ups_counter.Inc();
            update_watch.Start();

            model.Update(window.Keyboard, window.Width, window.Height);

            Program.debugViewSwitch.Update(window.Keyboard);
            Program.minimapViewSwitch.Update(window.Keyboard);

            update_watch.Stop();
        }

        protected virtual void StartRender()
        {
            render_watch.Start();

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.LoadIdentity();
            GL.Ortho(0, window.Width, 0, window.Height, 0, -100);
        }

        protected virtual void EndRender()
        {
            window.SwapBuffers();

            render_watch.Stop();
        }

        protected virtual void RenderRectangle(Vec2d tl, Vec2d bl, Vec2d br, Vec2d tr, double distance)
        {
            RenderRectangle(new Rect2d(bl, tr), distance);
        }

        protected virtual void RenderRectangle(Rect2d rect, double distance, Color4 col)
        {
            GL.Begin(BeginMode.Quads);
            GL.Color4(col);
            GL.Vertex3(rect.tl.X, rect.tl.Y, distance);
            GL.Vertex3(rect.bl.X, rect.bl.Y, distance);
            GL.Vertex3(rect.br.X, rect.br.Y, distance);
            GL.Vertex3(rect.tr.X, rect.tr.Y, distance);
            GL.Color3(1.0, 1.0, 1.0);
            GL.End();
        }

        protected virtual void RenderRectangle(Rect2d rect, double distance)
        {
            GL.Begin(BeginMode.Quads);
            GL.Vertex3(rect.tl.X, rect.tl.Y, distance);
            GL.Vertex3(rect.bl.X, rect.bl.Y, distance);
            GL.Vertex3(rect.br.X, rect.br.Y, distance);
            GL.Vertex3(rect.tr.X, rect.tr.Y, distance);
            GL.End();
        }

        protected virtual void RenderRectangle(Rect2d rect, OGLTexture texture, double distance)
        {
            Rect2d coords = texture.GetCoordinates();
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

        protected virtual void RenderColoredRectangle(Rect2d rect, double distance, Color4 col)
        {
            GL.Begin(BeginMode.Quads);
            GL.Color4(col);
            GL.Vertex3(rect.tl.X, rect.tl.Y, distance);
            GL.Vertex3(rect.bl.X, rect.bl.Y, distance);
            GL.Vertex3(rect.br.X, rect.br.Y, distance);
            GL.Vertex3(rect.tr.X, rect.tr.Y, distance);
            GL.Color3(1.0, 1.0, 1.0);
            GL.End();
        }

        protected virtual void RenderColoredBox(Rect2d rect, double distance, Color4 col)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(col);
            GL.Vertex3(rect.tl.X, rect.tl.Y, distance);
            GL.Vertex3(rect.bl.X, rect.bl.Y, distance);
            GL.Vertex3(rect.br.X, rect.br.Y, distance);
            GL.Vertex3(rect.tr.X, rect.tr.Y, distance);
            GL.Color3(1.0, 1.0, 1.0);
            GL.End();
        }

        protected virtual void RenderArrow(Vec2d pos, Vec2d arrow, double distance, double arrlen, Color4 col)
        {
            Vec2d end = pos + arrow;

            Vec2d smallArr = new Vec2d(arrow);
            smallArr.SetLength(smallArr.GetLength() - arrlen);

            Vec2d rot1 = pos + smallArr;

            Vec2d rot2 = pos + smallArr;

            rot1.rotateAround(end, Math.PI / 6);
            rot2.rotateAround(end, -Math.PI / 6);

            GL.Begin(BeginMode.LineStrip);
            GL.Color4(col);
            GL.Vertex3(pos.X, pos.Y, distance);
            GL.Vertex3(end.X, end.Y, distance);
            GL.Vertex3(rot1.X, rot1.Y, distance);
            GL.Vertex3(end.X, end.Y, distance);
            GL.Vertex3(rot2.X, rot2.Y, distance);
            GL.Color3(1.0, 1.0, 1.0);
            GL.End();
        }

        public void RenderFont(Vec2d off, Vec2d pos, QFont font, string text, Color4 col)
        {
            Vec2d offset = new Vec2d(off);
            offset.Y += window.Height;
            float w = font.Measure(text).Width;
            float h = font.Measure(text).Height;
            offset.Y -= h;
            offset.X += pos.X;
            offset.Y -= pos.Y;
            RenderColoredRectangle(new Rect2d(offset, w, h), 1, Color.FromArgb(128, 255, 255, 255));


            font.Options.Colour = col;
            font.Options.UseDefaultBlendFunction = false;

            QFont.Begin();
            GL.PushMatrix();

            GL.Translate(0, 0, 1);
            font.Print(text, new Vector2((float)pos.X, (float)pos.Y));

            GL.PopMatrix();
            QFont.End();

            GL.Disable(EnableCap.Texture2D);
            GL.Color3(1.0, 1.0, 1.0);
        }

        public void RenderPixel(Vec2d pos, double distance, Color4 col)
        {
            GL.Begin(BeginMode.Points);
            GL.Color4(col);
            GL.Vertex3(pos.X, pos.Y, distance);
            GL.Color3(1.0, 1.0, 1.0);
            GL.End();
        }

        public void RenderLine(Vec2d start, Vec2d end, double distance, Color4 col)
        {
            GL.Begin(BeginMode.Lines);
            GL.Color4(col);
            GL.Vertex3(start.X, start.Y, distance);
            GL.Vertex3(end.X, end.Y, distance);
            GL.Color3(1.0, 1.0, 1.0);
            GL.End();
        }
    }
}