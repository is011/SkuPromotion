using System;
using System.Collections.Generic;
using SkuPromotion.DataModel;

namespace SkuPromotion.DAL
{
    /// <summary>
    /// Contract promotion logic to fetch and get promotions
    /// </summary>
    public interface IPromotionSource
    {
        /// <summary>
        /// Proxy promotion will fetch data instead of database
        /// </summary>
        void ProxyPromotions();
        /// <summary>
        /// Get all offers or promotions which is active
        /// </summary>
        /// <returns></returns>
        List<Promotion> GetActiveOffers();
    }
}
