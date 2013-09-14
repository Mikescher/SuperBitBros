using Entities.SuperBitBros;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL;
using System;
using System.Drawing;

namespace SuperBitBros {
    class OpenGLGameView : OpenGLView {
        public OpenGLGameView(GameModel model)
            : base(model) {
            //
        }

        protected override void OnRender(object sender, EventArgs e) {
            GL.ClearColor(Color.FromArgb(0, 0, 0));
            StartRender();

            Vector2d offset = model.GetOffset(window.Width, window.Height);
            offset.X = (int)offset.X; // Cast to int for ... reasons
            offset.Y = (int)offset.Y;
            GL.Translate(-offset.X, -offset.Y, 0);

            Rectangle2d bRange = new Rectangle2d((int)(offset.X / Block.BLOCK_WIDTH) - 1,
                                                 (int)(offset.Y / Block.BLOCK_HEIGHT) - 1,
                                                 (int)(window.Width / Block.BLOCK_WIDTH) + 2,
                                                 (int)(window.Height / Block.BLOCK_HEIGHT) + 2);

            //Render from behind to nearest 100 = behindest

            double depthposition = 100;
            while (depthposition > 0) {
                depthposition = renderInDepth(depthposition, bRange);
            }

            EndRender();
        }

        protected double renderInDepth(double depth, Rectangle2d blockRange) {
            double nextDepth = 0;

            int x1 = (int)blockRange.bl.X;
            int y1 = (int)blockRange.bl.Y;
            int x2 = (int)blockRange.tr.X;
            int y2 = (int)blockRange.tr.Y;

            for (int x = x1; x <= x2; x++) {
                for (int y = y1; y <= y2; y++) {
                    Block block = model.GetBlock(x, y);
                    if (block == null)
                        continue;

                    if (block.distance == depth) {
                        if (block.RenderBackgroundAir())
                            RenderRectangle(block.GetTexturePosition(), Textures.texture_air, block.distance + 0.1);

                        RenderRectangle(block.GetPosition(), block.GetCurrentTexture(), block.distance);
                    } else if (block.distance < depth && block.distance > nextDepth) {
                        nextDepth = block.distance;
                    }
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