using System;
using System.Collections.Generic;
using SkuPromotion.DataModel;
using SkuPromotion.DAL;
using System.Linq;

namespace SkuPromotion.BusinessLogic
{    
    public class PromotionLogic : IPromotionLogic
    {
        IPromotionSource _promotionSource;
        ISkuLogic _skuLogic;
        List<char> ProcessedSKUs;
        public PromotionLogic(IPromotionSource promotionSource, ISkuLogic skuLogic)
        {
            _promotionSource = promotionSource;
            _skuLogic = skuLogic;
        }
        /// <summary>
        /// Fetch data from data source
        /// </summary>
        public void FetchPromotions()
        {
            _promotionSource.ProxyPromotions();
        }
        /// <summary>
        /// Get Offers from data source
        /// </summary>
        /// <returns></returns>
        private List<Promotion> GetActivePromotions()
        {
            return _promotionSource.GetActiveOffers();
        }

        /// <summary>
        /// Calculate final price based on all active promotions
        /// </summary>
        /// <param name="listSkuIds">Input list of items</param>
        /// <returns>total price</returns>
        public int CalculateFinalPrice(List<char> listSkuIds)
        {
            try
            {
                List<Promotion> listPromotions = GetActivePromotions();
                var groupSkuIds = listSkuIds.GroupBy(i => i.ToString());
                int totalAmount = 0;
                ProcessedSKUs = new List<char>();

                foreach (var skuIds in groupSkuIds)
                {
                    List<Promotion> offerList = listPromotions.Where(p => p.SKUs.Any(s => s.ID.ToString() == skuIds.Key)).ToList();

                    int selectedSKUCount = skuIds.Count();
                    bool isPriceAdded = ProcessedSKUs.Any(s => s == skuIds.Key.ToCharArray()[0]);
                    if (offerList.Count() > 0 && !isPriceAdded)
                    {
                        foreach (Promotion objPromotion in offerList)
                        {
                            if (objPromotion.SKUs.Any(s => s.ID.ToString() == skuIds.Key))
                            {
                                int fixedPrice = objPromotion.FixedPrice;
                                if (objPromotion.OfferName == SkuPromotionConstants.JumboOffer)
                                {
                                    totalAmount += CalculatePriceForMultipleItem(objPromotion, selectedSKUCount);
                                    ProcessedSKUs.Add(objPromotion.SKUs[0].ID);
                                    break;
                                }
                                else if (objPromotion.OfferName == SkuPromotionConstants.ComboOffer)
                                {
                                    totalAmount += CalculatePriceForComboItem(objPromotion, groupSkuIds);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!isPriceAdded)
                        {
                            Sku skuItem = _skuLogic.GetSKU(skuIds.Key.ToCharArray()[0]);
                            totalAmount += skuItem.Price;
                            ProcessedSKUs.Add(skuIds.Key.ToCharArray()[0]);
                        }
                    }

                }

                return totalAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calculate total price based on all active promotions or offers for multiple items of same type
        /// </summary>
        /// <param name="objPromotion">Promotion object</param>
        /// <param name="selectedSKUCount">Total count of selected items</param>
        /// <returns>total price for multi item of same type</returns>
        public int CalculatePriceForMultipleItem(Promotion objPromotion, int selectedSKUCount)
        {
            try
            {
                int total = 0;
                int promoQunatity = objPromotion.SKUs[0].Unit;
                if (selectedSKUCount >= promoQunatity)
                {
                    int promoApplicableCount = selectedSKUCount / promoQunatity;
                    int withoutPromoCount = selectedSKUCount % promoQunatity;
                    if (promoApplicableCount > 0)
                    {
                        total += (promoApplicableCount * objPromotion.DiscountInPercent) + (withoutPromoCount * objPromotion.SKUs[0].Price);
                    }
                }
                else
                {
                    total += (selectedSKUCount * objPromotion.SKUs[0].Price);
                }
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calculate total price based on all active promotions for combo items
        /// </summary>
        /// <param name="objPromotion">Promotion object</param>
        /// /// <param name="groupSkuIds">Grouped selected items</param>
        /// <returns>total price for combo type promotion </returns>
        public int CalculatePriceForComboItem(Promotion objPromotion, IEnumerable<IGrouping<string, char>> groupSkuIds)
        {
            try
            {
                int total = 0;
                Dictionary<char, int> skuCount = new Dictionary<char, int>();
                foreach (Sku sku in objPromotion.SKUs)
                {
                    int count = groupSkuIds.Where(s => s.Key.ToString() == sku.ID.ToString()).Count();
                    skuCount.Add(sku.ID, count);
                }

                int itemCount1 = skuCount.ElementAt(0).Value;
                int itemCount2 = skuCount.ElementAt(1).Value;

                if (itemCount1 != 0 && itemCount2 != 0)
                {
                    if (itemCount1 < itemCount2)
                    {
                        Sku skuItem = _skuLogic.GetSKU(skuCount.ElementAt(1).Key);
                        total += (itemCount1 * objPromotion.FixedPrice) + (itemCount2 - itemCount1) * skuItem.Price;
                    }
                    else
                    {
                        Sku skuItem = _skuLogic.GetSKU(skuCount.ElementAt(0).Key);
                        total += (itemCount2 * objPromotion.FixedPrice) + (itemCount1 - itemCount2) * skuItem.Price;
                    }
                    ProcessedSKUs.Add(skuCount.ElementAt(0).Key);
                    ProcessedSKUs.Add(skuCount.ElementAt(1).Key);
                }
                else
                {
                    if (itemCount1 > 0)
                    {
                        Sku skuItem = _skuLogic.GetSKU(skuCount.ElementAt(0).Key);
                        total += itemCount1 * skuItem.Price;
                    }
                    else
                    {
                        Sku skuItem = _skuLogic.GetSKU(skuCount.ElementAt(1).Key);
                        total += itemCount2 * skuItem.Price;
                    }
                }
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
