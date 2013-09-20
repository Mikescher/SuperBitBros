using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.HUD
{
    public enum HUDElementAlign { HEA_TL, HEA_BL, HEA_BR, HEA_TR}

    public abstract class HUDElement
    {
        public double width;
        public double height;

        protected HUDElementAlign align = HUDElementAlign.HEA_BR;
        protected Vec2d position;

        public HUDModel owner;

        protected OGLTexture texture;

        public HUDElement()
        {
            position = Vec2d.Zero;
        }

        public virtual void OnAdd(HUDModel model)
        {
            owner = model;
        }

        public virtual Rect2d GetPosition(int window_w, int window_h)
        {
            Vec2d pos = new Vec2d(position);

            if (align == HUDElementAlign.HEA_TL || align == HUDElementAlign.HEA_TR) // TOP
                pos.Y = window_h - pos.Y - height;
            else if (align == HUDElementAlign.HEA_BR || align == HUDElementAlign.HEA_TR) // Right
                pos.X = window_w - pos.X - width;

            return new Rect2d(pos, width, height);
        }

        public void SetPosition(HUDElementAlign p_align, double x, double y)
        {
            align = p_align;
            position.Set(x, y);
        }

        public virtual Rect2d GetTexturePosition(int window_w, int window_h)
        {
            return GetPosition(window_w, window_h);
        }

        public virtual OGLTexture GetCurrentTexture()
        {
            return texture;
        }

        public virtual double GetDistance()
        {
            return Entity.DISTANCE_HUD;
        }

        public abstract void Update(KeyboardDevice keyboard, double ucorrection);
    }
}
