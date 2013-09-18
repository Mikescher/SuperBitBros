namespace SuperBitBros.Entities.DynamicEntities {

    public class PersistentCoinEntity : CoinEntity {

        public PersistentCoinEntity()
            : base() {
            lifetime = -1;
        }
    }
}