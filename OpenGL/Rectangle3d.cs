using OpenTK;

namespace SuperBitBros.OpenGL {
    class Rectangle3d {
        public Vector3d tl { get; private set; }
        public Vector3d bl { get; private set; }
        public Vector3d br { get; private set; }
        public Vector3d tr { get; private set; }

        public Rectangle3d(Vector3d bottomleft, double width, double height) {
            tl = new Vector3d(bottomleft.X, bottomleft.Y + height, bottomleft.Z);
            bl = new Vector3d(bottomleft.X, bottomleft.Y, bottomleft.Z);
            br = new Vector3d(bottomleft.X + width, bottomleft.Y, bottomleft.Z);
            tr = new Vector3d(bottomleft.X + width, bottomleft.Y + height, bottomleft.Z);
        }

        public Rectangle3d(Vector3d bottomleft, double size) {
            tl = new Vector3d(bottomleft.X, bottomleft.Y + size, bottomleft.Z);
            bl = new Vector3d(bottomleft.X, bottomleft.Y, bottomleft.Z);
            br = new Vector3d(bottomleft.X + size, bottomleft.Y, bottomleft.Z);
            tr = new Vector3d(bottomleft.X + size, bottomleft.Y + size, bottomleft.Z);
        }

        public Rectangle3d(Vector3d bottomleft, Vector3d topRight) {
            tl = new Vector3d(bottomleft.X, topRight.Y, bottomleft.Z);
            bl = new Vector3d(bottomleft.X, bottomleft.Y, bottomleft.Z);
            br = new Vector3d(topRight.X, bottomleft.Y, bottomleft.Z);
            tr = new Vector3d(topRight.X, topRight.Y, bottomleft.Z);
        }
    }
}
