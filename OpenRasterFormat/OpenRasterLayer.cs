using System.Drawing;

namespace SuperBitBros.OpenRasterFormat {

    public class OpenRasterLayer {
        public readonly Bitmap Image;
        public readonly int X;
        public readonly int Y;
        public readonly string Name;

        public readonly int Width;
        public readonly int Height;

        public OpenRasterLayer(Image pimage, string pname, int px, int py) {
            this.Image = new Bitmap(pimage);
            this.Name = pname;
            this.X = px;
            this.Y = py;

            this.Width = Image.Width;
            this.Height = Image.Height;
        }

        public Color GetColor(int px_x, int px_y) {
            px_x -= X;
            px_y -= Y;

            if (px_x < 0 || px_y < 0 || px_x >= Width || px_y >= Height) {
                return Color.FromArgb(0, 0, 0, 0);
            }

            return Image.GetPixel(px_x, px_y);
        }
    }
}