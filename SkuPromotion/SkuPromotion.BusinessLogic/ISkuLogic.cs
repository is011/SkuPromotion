using System;
using System.Collections.Generic;
using SkuPromotion.DataModel;

namespace SkuPromotion.BusinessLogic
{
    
    public interface ISkuLogic
    {
        void FethSKU();
        Sku GetSKU(char SkuID);
    }
}
