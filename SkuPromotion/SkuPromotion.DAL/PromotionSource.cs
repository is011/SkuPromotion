using SkuPromotion.DataModel;
using System;
using System.Collections.Generic;

namespace SkuPromotion.DAL
{
    /// <summary>
    /// Promotion logic to fetch and get data instead of database
    /// </summary>
    public class PromotionSource : IPromotionSource
    {
        static List<Promotion> Promotions;
        ISkuSource _skuLogic;
        public PromotionSource(ISkuSource skuLogic)
        {
            _skuLogic = skuLogic;
        }
        /// <summary>
        /// Fetching promotion instead of taking for database
        /// </summary>
        public void ProxyPromotions()
        {
            Promotions = new List<Promotion>
            {
                new Promotion
                {
                    ID = Guid.NewGuid().ToString(),
                    OfferName = SkuPromotionConstants.JumboOffer,
                    SKUs = new List<Sku> { new Sku { ID='A', Unit=3, Price=_skuLogic.GetSKU('A').Price } },
                    FixedPrice = 130,
                    IsOfferActive = true,
                    DiscountInPercent = 0
                },
                new Promotion
                {
                    ID = Guid.NewGuid().ToString(),
                    OfferName = SkuPromotionConstants.JumboOffer,
                    SKUs = new List<Sku> { new Sku { ID='B', Unit=2, Price=_skuLogic.GetSKU('B').Price } },
                    FixedPrice = 45,
                    IsOfferActive = true,
                    DiscountInPercent = 0
                },
                new Promotion
                {
                    ID = Guid.NewGuid().ToString(),
                    OfferName = SkuPromotionConstants.ComboOffer,
                    SKUs = new List<Sku> { new Sku { ID='C', Unit=1, Price=_skuLogic.GetSKU('C').Price },
                                           new Sku { ID='D', Unit=1, Price=_skuLogic.GetSKU('D').Price } },
                    FixedPrice = 30,
                    IsOfferActive = true,
                    DiscountInPercent = 0
                }
            };
        }

        public List<Promotion> GetActiveOffers()
        {
            return Promotions.FindAll(p => p.IsOfferActive);
        }
    }
}
