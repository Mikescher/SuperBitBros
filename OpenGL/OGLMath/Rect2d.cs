namespace SuperBitBros.OpenGL.OGLMath {

    public class Rect2d {
        private Vec2d position; // bottomLeft
        private double width;
        private double height;

        public Vec2d tl { get { return new Vec2d(position.X, position.Y + height); } }

        public Vec2d bl { get { return new Vec2d(position.X, position.Y); } }

        public Vec2d br { get { return new Vec2d(position.X + width, position.Y); } }

        public Vec2d tr { get { return new Vec2d(position.X + width, position.Y + height); } }

        public Rect2d(double bl_x, double bl_y, double pwidth, double pheight) {
            position = new Vec2d(bl_x, bl_y);
            width = pwidth;
            height = pheight;
        }

        public Rect2d(Vec2d bottomleft, double pwidth, double pheight) {
            position = new Vec2d(bottomleft);
            width = pwidth;
            height = pheight;
        }

        public Rect2d(Vec2d bottomleft, double psize) {
            position = new Vec2d(bottomleft);
            width = psize;
            height = psize;
        }

        public Rect2d(Vec2d bottomleft, Vec2d topRight) {
            position = new Vec2d(bottomleft);
            width = topRight.X - bottomleft.X;
            height = topRight.Y - bottomleft.Y;
        }

        public Rect2d(Rect2d r) {
            position = new Vec2d(r.position);
            width = r.width;
            height = r.height;
        }

        public Vec2d GetMiddle() {
            return new Vec2d(position.X + width / 2.0, position.Y + height / 2.0);
        }

        public bool IsColldingWith(Rect2d rect) {
            return !(this.tl.X >= rect.br.X || this.br.X <= rect.tl.X || this.tl.Y <= rect.br.Y || this.br.Y >= rect.tl.Y);
        }

        public bool IsTouching(Rect2d rect) {
            return (this.tl.X == rect.br.X && this.tl.X > rect.tl.X) ||
                   (this.br.X == rect.tl.X && this.br.X < rect.br.X) ||
                   (this.tl.Y == rect.br.Y && this.tl.Y < rect.tl.Y) ||
                   (this.br.Y == rect.tl.Y && this.br.Y > rect.br.Y);
        }

        public Vec2d GetDistanceTo(Rect2d rect) {
            double vecX;
            double vecY;

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

            return new Vec2d(vecX, vecY);
        }

        public void TrimNorth(double len) {
            height -= len;
        }

        public void TrimEast(double len) {
            width -= len;
        }

        public void TrimSouth(double len) {
            position.Y += len;
            height -= len;
        }

        public void TrimWest(double len) {
            position.X += len;
            width -= len;
        }

        public bool Includes(Vec2d vec) {
            return (vec.X > position.X && vec.Y > position.Y && vec.X < tl.X && vec.Y < tl.Y);
        }

        public Vec2d GetDistanceTo(Vec2d vec) {
            Vec2d result = Vec2d.Zero;

            if (vec.X < position.X) {
                result.X = vec.X - position.X;
            } else if (vec.X > tr.X) {
                result.X = vec.X - tr.X;
            }

            if (vec.Y < position.Y) {
                result.Y = vec.Y - position.Y;
            } else if (vec.Y > tr.Y) {
                result.Y = vec.Y - tr.Y;
            }

            return result;
        }
    }
}