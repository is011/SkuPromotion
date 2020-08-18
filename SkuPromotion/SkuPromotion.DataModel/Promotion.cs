using System;
using System.Collections.Generic;
using System.Text;

namespace SkuPromotion.DataModel
{
    /// <summary>
    /// Promotion entity transfer data for promotions like Jumbo or Combo
    /// </summary>
    class Promotion
    {
        /// <summary>
        /// List of SKU information one promotion handles
        /// </summary>
        List<Sku> SKUs { get; set; }
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
