using SkuPromotion.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace SkuPromotion.DAL
{
    /// <summary>
    /// Implementation of SKU logic contract
    /// </summary>
    public class SkuSource : ISkuSource
    {
        /// <summary>
        /// List of SKUs to fetch data instead of database
        /// </summary>
        static List<Sku> listSKU = new List<Sku>();
        public void ProxySKUs()
        {
            listSKU.Add(new Sku { ID = 'A', Price = 50, Unit = 1 });
            listSKU.Add(new Sku { ID = 'B', Price = 30, Unit = 1 });
            listSKU.Add(new Sku { ID = 'C', Price = 20, Unit = 1 });
            listSKU.Add(new Sku { ID = 'D', Price = 15, Unit = 1 });
            listSKU.Add(new Sku { ID = 'E', Price = 10, Unit = 1 });
        }
        /// <summary>
        /// Get SKU by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sku GetSKU(char id)
        {
            return listSKU.FirstOrDefault(sk => sk.ID == id);
        }
    }
}
