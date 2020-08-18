namespace SkuPromotion.DataModel
{
    /// <summary>
    /// SKU entity is going to use for transfer information about each SKU in cart
    /// </summary>
    public class Sku
    {
        /// <summary>
        /// Identification of SKU in characters like A, B, C
        /// </summary>
        public char ID { get; set; }
        /// <summary>
        /// Number of SKU or count
        /// </summary>
        public int Unit { get; set; }
        /// <summary>
        /// Price for this SKU
        /// </summary>
        public int Price { get; set; }
    }
}
