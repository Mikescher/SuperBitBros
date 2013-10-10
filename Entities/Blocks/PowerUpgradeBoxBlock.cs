using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.MarioPower;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class PowerUpgradeBoxBlock : BoxBlock
    {
        public static Color color = Color.FromArgb(0, 128, 128);

        public override void OnActivate(Player p)
        {
            if (p.power is StandardMarioPower)
                owner.AddEntity(new MushroomEntity(), GetTopLeft().X, GetTopLeft().Y);
            else
                owner.AddEntity(new FlowerEntity(), GetTopLeft().X, GetTopLeft().Y);
            Deactivate();
            KillMobsAboveBlock();
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