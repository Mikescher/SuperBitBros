using SuperBitBros.Entities.EnityController;

namespace SuperBitBros.Entities.DynamicEntities.Mobs
{
    public class PrisonBowser : HammerBowser
    {
        public PrisonBowser()
            : base()
        {
            (controllerStack.Peek() as BowserController).active = true;
        }
    }
}
