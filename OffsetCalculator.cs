using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Collections.Generic;

namespace SuperBitBros
{
    public class OffsetCalculator
    {
        public const double MAX_CORRECTION_SPEED = 15;

        public Vec2d Value { get; private set; }

        public List<Rect2d> visionBoxes = new List<Rect2d>();

        public OffsetCalculator()
        {
            Value = Vec2d.Zero;
        }

        public void Change(Vec2d off)
        {
            Value = new Vec2d(off);
        }

        public void Calculate(Rect2d target, int window_width, int window_height, double mapW, double mapH, bool capCorrection = true)
        {
            Rect2d cameraBox = GetOffsetBox(window_width, window_height);
            Vec2d playerPos = target.GetMiddle();

            // Wenn man aus der Mittleren Box rausläuft - Offset Verschieben
            //###############################################################################

            Vec2d moveCorrection = cameraBox.GetDistanceTo(playerPos);

            // Offset NICHT verschieben wenn amn damit die Grenzen der aktuellen Zone verletzten Würde
            //###############################################################################

            Vec2d zoneCorrection = GetVisionZoneCorrection(Value + moveCorrection, playerPos, window_width, window_height);
            moveCorrection += zoneCorrection; // Korrigierte MoveCorrection (durch die Zone)

            if (capCorrection)
                moveCorrection.DoMaxLength(MAX_CORRECTION_SPEED);

            Value += moveCorrection;
        }

        private Vec2d GetVisionZoneCorrection(Vec2d offset, Vec2d playerPos, int window_width, int window_height)
        {
            Rect2d windowBox = new Rect2d(offset, window_width, window_height);
            foreach (Rect2d vrect in visionBoxes)
            {
                if (vrect.Includes(playerPos))
                {
                    double corrY_bottom = Math.Max(vrect.bl.Y - windowBox.bl.Y, 0);
                    double corrY_top = Math.Min(vrect.tr.Y - windowBox.tr.Y, 0);

                    double corrX_left = Math.Max(vrect.bl.X - windowBox.bl.X, 0);
                    double corrX_right = Math.Min(vrect.tr.X - windowBox.tr.X, 0);

                    Vec2d correction = Vec2d.Zero;

                    if (!(corrY_top != 0 && corrY_bottom != 0))
                    {
                        if (corrY_top != 0)
                            correction.Y = corrY_top;
                        else
                            correction.Y = corrY_bottom;
                    }

                    if (!(corrX_left != 0 && corrX_right != 0))
                    {
                        if (corrX_left != 0)
                            correction.X = corrX_left;
                        else
                            correction.X = corrX_right;
                    }

                    return correction;
                }
            }

            return Vec2d.Zero;
        }

        public Rect2d GetOffsetBox(int window_width, int window_height)
        {
            Rect2d result = new Rect2d(Value, window_width, window_height);

            result.TrimNorth(Block.BLOCK_HEIGHT * 2);
            result.TrimEast(Block.BLOCK_WIDTH * 8);
            result.TrimSouth(Block.BLOCK_HEIGHT * 2 + Player.PLAYER_HEIGHT / 2.0);
            result.TrimWest(Block.BLOCK_WIDTH * 8);

            return result;
        }

        public void AddVisionBoxes(List<Rect2d> boxes)
        {
            visionBoxes.AddRange(boxes);
        }
    }
}
