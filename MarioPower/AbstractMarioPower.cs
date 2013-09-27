
using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.DynamicEntities;
namespace SuperBitBros.MarioPower
{
    public abstract class AbstractMarioPower
    {
        public AbstractMarioPower()
        {

        }

        public virtual void Update(KeyboardDevice keyboard) { /**/ }

        public abstract double GetHeightMultiplier();
        public abstract double GetCrouchMultiplier();
        public abstract AbstractMarioPower GetSubPower();
        public abstract AnimatedTexture GetTexture();
        public abstract void DoAction(Player p);
    }
}
