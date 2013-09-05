using System;
using System.Drawing;
using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SuperBitBros
{
    class OpenGLGameView : OpenGLView
    {
        public OpenGLGameView(GameModel model) : base(model) 
        {
            //
        }

        protected override void OnRender(object sender, EventArgs e)
        {
            GL.ClearColor(Color.FromArgb(92, 148, 252));
            StartRender();

            Vector2d offset = model.GetOffset(window.Width, window.Height);
            GL.Translate(-offset.X, -offset.Y, 0);
            //GL.Translate(10, 10, 0);

            foreach (Entity entity in model.entityList)
            {
                RenderRectangle(entity.GetPosition(), entity.GetCurrentTexture(), entity.distance);
            }

            EndRender();
        }
    }
}