using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace SuperBitBros.OpenGL
{
    class Rectangle2d
    {
        public Vector2d tl { get; private set; }
        public Vector2d bl { get; private set; }
        public Vector2d br { get; private set; }
        public Vector2d tr { get; private set; }

        public Rectangle2d(Vector2d bottomleft, double width, double height)
        {
            tl = new Vector2d(bottomleft.X, bottomleft.Y + height);
            bl = new Vector2d(bottomleft.X, bottomleft.Y);
            br = new Vector2d(bottomleft.X + width, bottomleft.Y);
            tr = new Vector2d(bottomleft.X + width, bottomleft.Y + height);
        }

        public Rectangle2d(Vector2d bottomleft, double size)
        {
            tl = new Vector2d(bottomleft.X, bottomleft.Y + size);
            bl = new Vector2d(bottomleft.X, bottomleft.Y);
            br = new Vector2d(bottomleft.X + size, bottomleft.Y);
            tr = new Vector2d(bottomleft.X + size, bottomleft.Y + size);
        }

        public Rectangle2d(Vector2d bottomleft, Vector2d topRight)
        {
            tl = new Vector2d(bottomleft.X, topRight.Y);
            bl = new Vector2d(bottomleft.X, bottomleft.Y);
            br = new Vector2d(topRight.X,   bottomleft.Y);
            tr = new Vector2d(topRight.X,   topRight.Y);
        }

        public Vector2d GetMiddle()
        {
            return new Vector2d((bl.X + tr.X) / 2.0, (bl.Y + tr.Y) / 2.0);
        }

        public bool isColldingWith(Rectangle2d rect)
        {
            return !(this.tl.X >= rect.br.X || this.br.X <= rect.tl.X || this.tl.Y <= rect.br.Y || this.br.Y >= rect.tl.Y);
        }

        public Vector2d getDistanceTo(Rectangle2d rect)
        {
            Vector2d result = new Vector2d();

            if (rect.br.X < this.tl.X)
                result.X = rect.br.X - this.tl.X;
            else if (rect.tl.X > this.br.X)
                result.X = rect.tl.X - this.br.X;
            else
                result.X = 0;

            if (rect.br.Y > this.tl.Y)
                result.Y = rect.br.Y - this.tl.Y;
            else if (rect.tl.Y < this.br.Y)
                result.Y = rect.tl.Y - this.br.Y;
            else
                result.Y = 0;

            return result;
        }
    }
}
