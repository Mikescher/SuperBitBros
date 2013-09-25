using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.Triggers;
using System.Drawing;

namespace SuperBitBros.Entities.Blocks
{
    public class BeanStalkBoxBlock : BoxBlock
    {
        public static Color color = Color.FromArgb(128, 64, 16);

        public static Color GetColor()
        {
            return color;
        }

        public override void OnActivate(Player p)
        {
            int i = 0;

            for (int yy = blockPos.Y + 1; yy < owner.mapRealHeight; yy++)
            {
                bool found = false;
                foreach (Trigger t in owner.getReliableTriggerList(blockPos.X, yy))
                {
                    if (t is BeanStalkSpawnZone)
                    {
                        found = true;

                        owner.AddDelayedAction(i * 20, () => (t as BeanStalkSpawnZone).DoSpawn());

                        i++;
                    }
                }
                if (!found)
                    break;
            }

            Deactivate();
        }

        public override Color GetBlockColor()
        {
            return color;
        }
    }
}