using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace SuperBitBros.OpenGL {
    class OpenGLGameView : OpenGLView {
        public OpenGLGameView(GameModel model)
            : base(model) {
            //
        }

        protected override void OnRender(object sender, EventArgs e) {
            GL.ClearColor(Color.FromArgb(92, 148, 252));
            StartRender();

            Vector2d offset = model.GetOffset(window.Width, window.Height);
            GL.Translate(-offset.X, -offset.Y, 0);
            //GL.Translate(10, 10, 0);

            foreach (Entity entity in model.entityList) {
                if (entity.RenderBackgroundAir())
                    RenderRectangle(entity.GetPosition(), Textures.texture_air, entity.distance + 1);

                RenderRectangle(entity.GetPosition(), entity.GetCurrentTexture(), entity.distance);
            }

            EndRender();
        }
    }
}