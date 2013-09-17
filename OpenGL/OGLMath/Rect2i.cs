namespace SuperBitBros.OpenGL.OGLMath
{
    public class Rect2i
    {
        private Vec2i position; // bottomLeft
        private int width;
        private int height;

        public Vec2i tl { get { return new Vec2i(position.X, position.Y + height); } }

        public Vec2i bl { get { return new Vec2i(position.X, position.Y); } }

        public Vec2i br { get { return new Vec2i(position.X + width, position.Y); } }

        public Vec2i tr { get { return new Vec2i(position.X + width, position.Y + height); } }

        public Rect2i(int bl_x, int bl_y, int pwidth, int pheight)
        {
            position = new Vec2i(bl_x, bl_y);
            width = pwidth;
            height = pheight;
        }

        public Rect2i(Vec2i bottomleft, int pwidth, int pheight)
        {
            position = new Vec2i(bottomleft);
            width = pwidth;
            height = pheight;
        }

        public Rect2i(Vec2i bottomleft, int psize)
        {
            position = new Vec2i(bottomleft);
            width = psize;
            height = psize;
        }

        public Rect2i(Vec2i bottomleft, Vec2i topRight)
        {
            position = new Vec2i(bottomleft);
            width = topRight.X - bottomleft.X;
            height = topRight.Y - bottomleft.Y;
        }

        public Rect2i(Rect2i r)
        {
            position = new Vec2i(r.position);
            width = r.width;
            height = r.height;
        }

        public Vec2d GetMiddle()
        {
            return new Vec2d(position.X + width / 2.0, position.Y + height / 2.0);
        }

        public bool IsColldingWith(Rect2i rect)
        {
            return !(this.tl.X >= rect.br.X || this.br.X <= rect.tl.X || this.tl.Y <= rect.br.Y || this.br.Y >= rect.tl.Y);
        }

        public bool IsTouching(Rect2i rect)
        {
            return (this.tl.X == rect.br.X && this.tl.X > rect.tl.X) ||
                   (this.br.X == rect.tl.X && this.br.X < rect.br.X) ||
                   (this.tl.Y == rect.br.Y && this.tl.Y < rect.tl.Y) ||
                   (this.br.Y == rect.tl.Y && this.br.Y > rect.br.Y);
        }

        public Vec2i GetDistanceTo(Rect2i rect)
        {
            int vecX;
            int vecY;

            if (rect.br.X < this.tl.X)
                vecX = rect.br.X - this.tl.X;
            else if (rect.tl.X > this.br.X)
                vecX = rect.tl.X - this.br.X;
            else
                vecX = 0;

            if (rect.br.Y > this.tl.Y)
                vecY = rect.br.Y - this.tl.Y;
            else if (rect.tl.Y < this.br.Y)
                vecY = rect.tl.Y - this.br.Y;
            else
                vecY = 0;

            return new Vec2i(vecX, vecY);
        }

        public void TrimNorth(int len)
        {
            height -= len;
        }

        public void TrimEast(int len)
        {
            width -= len;
        }

        public void TrimSouth(int len)
        {
            position.Y += len;
            height -= len;
        }

        public void TrimWest(int len)
        {
            position.X += len;
            height -= len;
        }

        public bool Includes(Vec2i vec)
        {
            return (vec.X > position.X && vec.Y > position.Y && vec.X < tl.X && vec.Y < tl.Y);
        }

        public Vec2i GetDistanceTo(Vec2i vec)
        {
            Vec2i result = Vec2i.Zero;

            if (vec.X < position.X)
            {
                result.X = vec.X - position.X;
            }
            else if (vec.X > tl.X)
            {
                result.X = vec.X - tl.X;
            }

            if (vec.Y < position.Y)
            {
                result.Y = vec.Y - position.Y;
            }
            else if (vec.Y > tl.Y)
            {
                result.Y = vec.Y - tl.Y;
            }

            return result;
        }
    }
}