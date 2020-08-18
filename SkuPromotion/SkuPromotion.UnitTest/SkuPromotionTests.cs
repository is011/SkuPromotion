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

        #region Expected Test Cases
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
        #endregion

        #region Extra Test Cases
        [Test]
        public void TestCase_ScenarioD()
        {
            List<char> inputs = new List<char>() { 'C', 'D' };
            int actualResult = _promotionLogic.CalculateFinalPrice(inputs);
            int expectedResult = 30;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void TestCase_ScenarioE()
        {
            List<char> inputs = new List<char>() { 'A', 'C' };
            int actualResult = _promotionLogic.CalculateFinalPrice(inputs);
            int expectedResult = 70;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void TestCase_ScenarioF()
        {
            List<char> inputs = new List<char>() { 'A', 'B', 'C', 'D' };
            int actualResult = _promotionLogic.CalculateFinalPrice(inputs);
            int expectedResult = 110;
            Assert.AreEqual(expectedResult, actualResult);

        }
        [Test]
        public void TestCase_ScenarioG()
        {
            List<char> inputs = new List<char>() { 'A', 'A', 'A', 'A','A' };
            int actualResult = _promotionLogic.CalculateFinalPrice(inputs);
            int expectedResult = 230;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestCase_ScenarioH()
        {
            List<char> inputs = new List<char>() { 'B', 'B', 'B', 'B', 'B' };
            int actualResult = _promotionLogic.CalculateFinalPrice(inputs);
            int expectedResult = 120;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void TestCase_ScenarioI()
        {
            List<char> inputs = new List<char>() { 'C' };
            int actualResult = _promotionLogic.CalculateFinalPrice(inputs);
            int expectedResult = 20;
            Assert.AreEqual(expectedResult, actualResult);
        }
        #endregion
    }
}