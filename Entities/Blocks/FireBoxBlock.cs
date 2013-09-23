
using SuperBitBros.Entities.DynamicEntities;
using System.Drawing;
namespace SuperBitBros.Entities.Blocks
{
    public class FireBoxBlock : Block
    {
        public static Color color = Color.FromArgb(255, 64, 0);

        public FireBoxBlock()
            : base()
        {
            texture = Textures.texture_coinblock_empty;
        }

        public override void OnAdd(GameModel mod, int bx, int by)
        {
            base.OnAdd(mod, bx, by);

            for (int i = 0; i < 6; i++)
            {
                owner.AddEntity(new FireballEntity(this, 20 + i * 10), position.X, position.Y);
            }
        }

        public static Color GetColor()
        {
            return color;
        }

        public override Color GetBlockColor()
        {
            return color;
        }

        public override EntityRenderType GetRenderType()
        {
            return EntityRenderType.BRT_BLOCKTEXTURES;
        }
    }
}
