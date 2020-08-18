using System.Collections.Generic;

namespace SkuPromotion.DataModel
{
    /// <summary>
    /// Promotion entity transfer data for promotions like Jumbo or Combo
    /// </summary>
    public class Promotion
    {
        /// <summary>
        /// Identification of offer or promotion
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// List of SKU information one promotion handles
        /// </summary>
        public List<Sku> SKUs { get; set; }
        /// <summary>
        /// Initializing list of SKUs in constructor itself
        /// </summary>
        public Promotion()
        {
            SKUs = new List<Sku>();
        }
        /// <summary>
        /// Name of offer for this promotion
        /// </summary>
        public string OfferName { get; set; }
        /// <summary>
        /// Fixed price as per offer
        /// </summary>
        public int FixedPrice { get; set; }
        /// <summary>
        /// Discount in percentage for future promotions
        /// </summary>
        public int DiscountInPercent { get; set; }
        /// <summary>
        /// Check for active offer, it can help to switch on or off promotions
        /// </summary>
        public bool IsOfferActive { get; set; }
    }
}
