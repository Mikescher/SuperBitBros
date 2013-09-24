using SuperBitBros.Entities.DynamicEntities;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class MushroomBoxBlock : BoxBlock
    {
        public static Color color = Color.FromArgb(0, 128, 128);

        public override void OnActivate(Player p)
        {
            owner.AddEntity(new MushroomEntity(), GetTopLeft().X, GetTopLeft().Y);
            Deactivate();
        }

        public static Color GetColor()
        {
            return color;
        }

        public override Color GetBlockColor()
        {
            return color;
        }
    }
}