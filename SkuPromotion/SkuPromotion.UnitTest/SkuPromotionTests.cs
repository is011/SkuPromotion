using NUnit.Framework;
using SkuPromotion.DAL;
using SkuPromotion.BusinessLogic;
using SkuPromotion.DataModel;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace SkuPromotion.UnitTest
{
    public class SkuPromotionTests
    {
        IPromotionLogic _promotionLogic;
        ISkuLogic _skuLogic;
        private ServiceProvider serviceProvider { get; set; }
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddTransient<IPromotionSource, PromotionSource>();
            services.AddTransient<ISkuSource, SkuSource>();
            services.AddTransient<IPromotionLogic, PromotionLogic>();
            services.AddTransient<ISkuLogic, SkuLogic>();
            serviceProvider = services.BuildServiceProvider();

            _skuLogic = serviceProvider.GetService<ISkuLogic>();
            _skuLogic.FethSKU();
            _promotionLogic = serviceProvider.GetService<IPromotionLogic>();
            _promotionLogic.FetchPromotions();
        }

        [Test]
        public void TestCase_ScenarioA()
        {

            List<char> inputs = new List<char>() { 'A', 'B', 'C' };
            int actualResult = _promotionLogic.CalculateFinalPrice(inputs);
            int expectedResult = 100;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestCase_ScenarioB()
        {
            List<char> inputs = new List<char>() { 'A', 'A', 'B', 'C', 'A', 'B', 'A', 'B', 'A', 'B', 'B' };
            int actualResult = _promotionLogic.CalculateFinalPrice(inputs);
            int expectedResult = 370;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestCase_ScenarioC()
        {
            List<char> inputs = new List<char>() { 'A', 'A', 'B', 'C', 'B', 'A', 'B', 'B', 'B', 'D' };
            int actualResult = _promotionLogic.CalculateFinalPrice(inputs);
            int expectedResult = 280;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}