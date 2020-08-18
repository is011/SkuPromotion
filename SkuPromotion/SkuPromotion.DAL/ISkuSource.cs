using System;
using System.Collections.Generic;
using SkuPromotion.DataModel;

namespace SkuPromotion.DAL
{
    /// <summary>
    /// Contract sku logic to fetch and get skus
    /// </summary>
    interface ISkuSource
    {
        // <summary>
        /// Proxy sku will fetch data instead of database
        /// </summary>
        void ProxySKUs();
        /// <summary>
        /// Get SKU by characters treated as ID like A, B, C
        /// </summary>
        /// <returns></returns>
        Sku GetSKU(char id);
    }
}
