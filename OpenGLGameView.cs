using SuperBitBros.Entities;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Triggers;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Collections.Generic;
using System.Drawing;
using SuperBitBros.Triggers.PipeZones;

namespace SuperBitBros {
    class OpenGLGameView : OpenGLView {


        public OpenGLGameView(GameModel model)
            : base(model) {
            //
        }

        protected override void OnRender(object sender, EventArgs e) {
            base.OnRender(sender, e);

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

            if (Program.IS_DEBUG) {
                RenderDebug(offset);
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

        private void RenderDebug(Vec2i offset) {
            GL.Disable(EnableCap.Texture2D);

            //######################
            // RENDER TRIGGER
            //######################

            for (int x = 0; x < model.mapBlockWidth; x++) {
                for (int y = 0; y < model.mapBlockHeight; y++) {
                    List<Trigger> tlist = model.getTriggerList(x, y);

                    if (tlist != null) {
                        foreach (Trigger t in tlist) {
                            RenderColoredRectangle(t.GetPosition(), 0.15, Color.FromArgb(128, t.GetTriggerColor()));

                            if (t is PipeZone)
                            {
                                Vec2d start = t.GetPosition().GetMiddle();
                                Vec2d arr = Vec2d.Zero;
                                if (t is MoveNorthPipeZone) {
                                    start.Y -= Block.BLOCK_HEIGHT / 4.0;
                                    arr = new Vec2d(0, Block.BLOCK_HEIGHT / 2.0);
                                } else if (t is MoveEastPipeZone) {
                                    start.X -= Block.BLOCK_HEIGHT / 4.0;
                                    arr = new Vec2d(Block.BLOCK_WIDTH / 2.0, 0);
                                } else if (t is MoveSouthPipeZone) {
                                    start.Y += Block.BLOCK_WIDTH / 4.0;
                                    arr = new Vec2d(0, -Block.BLOCK_HEIGHT / 2.0);
                                } else if (t is MoveWestPipeZone) {
                                    start.X += Block.BLOCK_WIDTH / 4.0;
                                    arr = new Vec2d(-Block.BLOCK_WIDTH / 2.0, 0);
                                }

                                RenderColoredBox(t.GetPosition(), 0.1, t.GetTriggerColor());
                                RenderArrow(start, arr, 0.1, t.GetTriggerColor());
                            }
                        }
                    }
                }
            }

            //############################################
            // RENDER COLLSIIONBOXES && MOVEMNT VECTORS
            //############################################

            foreach (DynamicEntity e in model.entityList) {
                RenderColoredBox(e.GetPosition(), 0.1, Color.FromArgb(200, 0, 0, 255));

                RenderArrow(e.GetMiddle(), e.GetMovement() * 8, 0.2, Color.FromArgb(200, 0, 0, 255));
            }

            //######################
            // RENDER OFFSET BOX
            //######################

            RenderColoredBox(((GameWorld)model).GetOffsetBox(window.Width, window.Height), 0.1, Color.FromArgb(200, 255, 0, 0));

            //######################
            // RENDER MINIMAP
            //######################

            RenderMinimap(offset);

            //######################
            // RENDER DEBUG TEXTS
            //######################

            int foy = 0;
            Color4 col = Color.FromArgb(0, 0, 0);
            RenderFont(new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("FPS: {0} / {1}", (int)fps_counter.frequency, window.TargetRenderFrequency), col);
            RenderFont(new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("UPS: {0}", (int)ups_counter.frequency), col);
            RenderFont(new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("Entities: {0}", model.entityList.Count), col);
            RenderFont(new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("Player: [int] {0}", (Vec2i)((GameWorld)model).player.position), col);
            RenderFont(new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("Offset: [int] {0}", (Vec2i)offset), col);

            GL.Enable(EnableCap.Texture2D);
        }

        private void RenderMinimap(Vec2i offset) {
            Rect2d mapRect = new Rect2d(offset.X + window.Width - 5 - model.mapBlockWidth, offset.Y + window.Height - 5 - model.mapBlockHeight, model.mapBlockWidth, model.mapBlockHeight);

            for (int x = 0; x < model.mapBlockWidth; x++) {
                for (int y = 0; y < model.mapBlockHeight; y++) {
                    RenderPixel(mapRect.bl + new Vec2d(x, y), 0.3, model.GetBlockColor(x, y));
                }
            }

            RenderColoredBox(new Rect2d(offset / Block.BLOCK_SIZE + mapRect.bl, window.Width * 1.0 / Block.BLOCK_WIDTH, window.Height * 1.0 / Block.BLOCK_HEIGHT), 0.25, Color.FromArgb(255, 255, 0, 0));
        }
    }
}