using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SuperBitBros.OpenGL.Entities.Blocks;
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
            offset.X = (int)offset.X; // Cast to int for ... reasons
            offset.Y = (int)offset.Y;
            GL.Translate(-offset.X, -offset.Y, 0);

            //Render from behind to nearest 100 = behindest

            double depthposition = 100;
            while (depthposition > 0) {
                depthposition = renderInDepth(depthposition);
            }

            EndRender();
        }

        protected double renderInDepth(double depth) {
            double nextDepth = 0;

            foreach (Block block in model.blockList) {
                if (block.distance == depth) {
                    if (block.RenderBackgroundAir())
                        RenderRectangle(block.GetTexturePosition(), Textures.texture_air, block.distance + 0.1);

                    RenderRectangle(block.GetPosition(), block.GetCurrentTexture(), block.distance);
                } else if (block.distance < depth && block.distance > nextDepth) {
                    nextDepth = block.distance;
                }
            }

            foreach (DynamicEntity entity in model.entityList) {
                if (entity.distance == depth) {
                    RenderRectangle(entity.GetTexturePosition(), entity.GetCurrentTexture(), entity.distance);
                } else if (entity.distance < depth && entity.distance > nextDepth) {
                    nextDepth = entity.distance;
                }
            }

            return nextDepth;
        }
    }
}