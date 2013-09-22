
using SuperBitBros.Entities;
namespace SuperBitBros.MarioPower
{
    public abstract class AbstractMarioPower
    {
        public AbstractMarioPower()
        {

        }

        public abstract double GetHeightMultiplier();
        public abstract AbstractMarioPower GetSubPower();
        public abstract AnimatedTexture GetTexture();
    }
}
