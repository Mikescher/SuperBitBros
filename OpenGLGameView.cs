using Entities.SuperBitBros;
using OpenTK.Graphics.OpenGL;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL.OGLMath;
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

            Vec2i offset = (Vec2i)model.GetOffset(window.Width, window.Height); // Cast to int for ... reasons
            GL.Translate(-offset.X, -offset.Y, 0);

            Rect2i bRange = new Rect2i((int)(offset.X / Block.BLOCK_WIDTH) - 1,
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

        protected double renderInDepth(double depth, Rect2i blockRange) {
            double nextDepth = 0;

            for (int x = blockRange.bl.X; x <= blockRange.tr.X; x++) {
                for (int y = blockRange.bl.Y; y <= blockRange.tr.Y; y++) {
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