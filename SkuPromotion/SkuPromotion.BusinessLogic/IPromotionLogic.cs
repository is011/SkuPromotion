using System;
using System.Collections.Generic;
using System.Text;
using SkuPromotion.DataModel;

namespace SkuPromotion.BusinessLogic
{
    /// <summary>
    /// Business logic contract to connect with source and process logic
    /// </summary>
    public interface IPromotionLogic
    {
        /// <summary>
        /// connect with source to transfer data
        /// </summary>
        void FetchPromotions();
        /// <summary>
        /// Process logic of calulations
        /// </summary>
        /// <param name="listSkuIds"></param>
        /// <returns></returns>
        int CalculateFinalPrice(List<char> listSkuIds);
    }
}
