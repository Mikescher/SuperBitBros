using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Triggers;
using SuperBitBros.Triggers.PipeZones;

namespace SuperBitBros
{
    public class OpenGLGameView : OpenGLView
    {
        public OpenGLGameView(GameModel model)
            : base(model)
        {
            //
        }

        protected override void OnRender(object sender, EventArgs e)
        {
            base.OnRender(sender, e);

            GL.ClearColor(Color.FromArgb(0, 0, 0));
            StartRender();

            Vec2i offset = (Vec2i)model.GetOffset(); // Cast to int for ... reasons
            GL.Translate(-offset.X, -offset.Y, 0);

            Rect2i bRange = new Rect2i((int)(offset.X / Block.BLOCK_WIDTH) - 1,
                                                 (int)(offset.Y / Block.BLOCK_HEIGHT) - 1,
                                                 (int)(window.Width / Block.BLOCK_WIDTH) + 2,
                                                 (int)(window.Height / Block.BLOCK_HEIGHT) + 2);

            //Render from behind to nearest 100 = behindest

            double depthposition = 100;
            while (depthposition > 0)
            {
                depthposition = renderInDepth(depthposition, bRange);
            }

            if (Program.debugViewSwitch.Value)
            {
                RenderDebug(offset);
            }

            EndRender();
        }

        protected double renderInDepth(double depth, Rect2i blockRange)
        {
            double nextDepth = 0;

            for (int x = blockRange.bl.X; x <= blockRange.tr.X; x++)
            {
                for (int y = blockRange.bl.Y; y <= blockRange.tr.Y; y++)
                {
                    Block block = model.GetBlock(x, y);
                    if (block == null)
                        continue;

                    if (block.distance == depth)
                    {
                        if (block.RenderBackgroundAir())
                            RenderRectangle(block.GetTexturePosition(), Textures.texture_air, block.distance + 0.1);

                        RenderRectangle(block.GetPosition(), block.GetCurrentTexture(), block.distance);
                    }
                    else if (block.distance < depth && block.distance > nextDepth)
                    {
                        nextDepth = block.distance;
                    }
                }
            }

            foreach (DynamicEntity entity in model.entityList)
            {
                if (entity.distance == depth)
                {
                    RenderRectangle(entity.GetTexturePosition(), entity.GetCurrentTexture(), entity.distance);
                }
                else if (entity.distance < depth && entity.distance > nextDepth)
                {
                    nextDepth = entity.distance;
                }
            }

            return nextDepth;
        }

        protected override void OnUpdate(object sender, EventArgs e)
        {
            base.OnUpdate(sender, e);
        }

        private void RenderDebug(Vec2i offset)
        {
            GL.Disable(EnableCap.Texture2D);

            //######################
            // RENDER TRIGGER
            //######################

            for (int x = 0; x < model.mapBlockWidth; x++)
            {
                for (int y = 0; y < model.mapBlockHeight; y++)
                {
                    List<Trigger> tlist = model.getTriggerList(x, y);

                    if (tlist != null)
                    {
                        foreach (Trigger t in tlist)
                        {
                            RenderColoredRectangle(t.GetPosition(), Entity.DISTANCE_DEBUG_ZONE, Color.FromArgb(128, t.GetTriggerColor()));

                            if (t is PipeZone)
                            {
                                PipeZone z = t as PipeZone;

                                RenderColoredBox(t.GetPosition(), Entity.DISTANCE_DEBUG_ZONE, t.GetTriggerColor());

                                Vec2d start = t.GetPosition().GetMiddle();
                                Vec2d arr = Vec2d.Zero;
                                const int arrlen = 5;
                                Color4 arrcolor = z.CanEnter() ? Color.Red : Color.Black;
                                switch (z.GetRealDirection())
                                {
                                    case PipeDirection.NORTH:
                                        start.Y -= Block.BLOCK_HEIGHT / 4.0;
                                        arr = new Vec2d(0, Block.BLOCK_HEIGHT / 2.0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        break;

                                    case PipeDirection.EAST:
                                        start.X -= Block.BLOCK_HEIGHT / 4.0;
                                        arr = new Vec2d(Block.BLOCK_WIDTH / 2.0, 0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        break;

                                    case PipeDirection.SOUTH:
                                        start.Y += Block.BLOCK_WIDTH / 4.0;
                                        arr = new Vec2d(0, -Block.BLOCK_HEIGHT / 2.0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        break;

                                    case PipeDirection.WEST:
                                        start.X += Block.BLOCK_WIDTH / 4.0;
                                        arr = new Vec2d(-Block.BLOCK_WIDTH / 2.0, 0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        break;

                                    case PipeDirection.NORTHSOUTH:
                                        arr = new Vec2d(0, Block.BLOCK_HEIGHT / 4.0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        arr = new Vec2d(0, -Block.BLOCK_HEIGHT / 4.0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        break;

                                    case PipeDirection.EASTWEST:
                                        arr = new Vec2d(Block.BLOCK_WIDTH / 4.0, 0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        arr = new Vec2d(-Block.BLOCK_WIDTH / 4.0, 0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        break;

                                    case PipeDirection.ANY:
                                        arr = new Vec2d(0, Block.BLOCK_HEIGHT / 4.0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        arr = new Vec2d(0, -Block.BLOCK_HEIGHT / 4.0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        arr = new Vec2d(Block.BLOCK_WIDTH / 4.0, 0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        arr = new Vec2d(-Block.BLOCK_WIDTH / 4.0, 0);
                                        RenderArrow(start, arr, Entity.DISTANCE_DEBUG_ZONE - 1, arrlen, arrcolor);
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            //############################################
            // RENDER COLLSIIONBOXES && MOVEMNT VECTORS
            //############################################

            foreach (DynamicEntity e in model.entityList)
            {
                RenderColoredBox(e.GetPosition(), Entity.DISTANCE_DEBUG_MARKER, Color.FromArgb(200, 0, 0, 255));

                RenderArrow(e.GetMiddle(), e.GetMovement() * 8, Entity.DISTANCE_DEBUG_MARKER, 7, Color.FromArgb(200, 0, 0, 255));
            }

            //######################
            // RENDER OFFSET BOX
            //######################

            RenderColoredBox(((GameWorld)model).offset.GetOffsetBox(window.Width, window.Height), Entity.DISTANCE_DEBUG_MARKER, Color.FromArgb(200, 255, 0, 0));

            //######################
            // RENDER MINIMAP
            //######################

            if (Program.minimapViewSwitch.Value)
                RenderMinimap(offset, Entity.DISTANCE_DEBUG_MINIMAP);

            //######################
            // RENDER DEBUG TEXTS
            //######################
            int foy = 0;
            Color4 col = Color.FromArgb(0, 0, 0);
            RenderFont(offset, new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("FPS: {0} / {1}", (int)fps_counter.Frequency, window.TargetRenderFrequency), col);
            RenderFont(offset, new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("UPS: {0}", (int)ups_counter.Frequency), col);
            RenderFont(offset, new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("Entities: {0}", model.entityList.Count), col);
            RenderFont(offset, new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("Player: [int] {0}", (Vec2i)((GameWorld)model).player.position), col);
            RenderFont(offset, new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("Offset: [int] {0}", (Vec2i)offset), col);
            RenderFont(offset, new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("Player -> : [int] {0}", (Vec2i)((GameWorld)model).player.GetMovement()), col);
            RenderFont(offset, new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("Avg R-Time: {0}", ((int)(render_watch.Duration * 10)) / 10.0), col);
            RenderFont(offset, new Vec2d(5, 5 + foy++ * 12), DebugFont, String.Format("Avg U-Time: {0}", ((int)(update_watch.Duration * 10)) / 10.0), col);

            GL.Enable(EnableCap.Texture2D);
        }

        private void RenderMinimap(Vec2i offset, double distance)
        {
            Rect2d mapRect = new Rect2d(offset.X + window.Width - 5 - model.mapBlockWidth, offset.Y + window.Height - 5 - model.mapBlockHeight, model.mapBlockWidth, model.mapBlockHeight);

            for (int x = 0; x < model.mapBlockWidth; x++)
            {
                for (int y = 0; y < model.mapBlockHeight; y++)
                {
                    RenderPixel(mapRect.bl + new Vec2d(x, y), distance, model.GetBlockColor(x, y));
                }
            }

            RenderColoredBox(new Rect2d(offset / Block.BLOCK_SIZE + mapRect.bl, window.Width * 1.0 / Block.BLOCK_WIDTH, window.Height * 1.0 / Block.BLOCK_HEIGHT), distance-0.5, Color.FromArgb(255, 255, 0, 0));
        }
    }
}