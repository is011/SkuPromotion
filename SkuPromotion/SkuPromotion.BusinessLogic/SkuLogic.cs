using System;
using System.Collections.Generic;
using System.Linq;
using SkuPromotion.DataModel;
using SkuPromotion.DAL;

namespace SkuPromotion.BusinessLogic
{
    
    public class SkuLogic : ISkuLogic
    {
        ISkuSource _skuSource;
        public SkuLogic(ISkuSource skuSource)
        {
            _skuSource = skuSource;
        }

        public void FethSKU()
        {
            _skuSource.ProxySKUs();
        }
        public Sku GetSKU(char SkuID)
        {
            return _skuSource.GetSKU(SkuID);
        }
    }
}
