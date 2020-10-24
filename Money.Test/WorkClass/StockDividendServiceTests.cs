using Microsoft.VisualStudio.TestTools.UnitTesting;
using Money.Repository.Interface;
using Money.WebService.WorkClass;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Money.WebService.WorkClass.Tests
{
    [TestClass()]
    public class StockDividendServiceTests
    {
        [TestMethod("測試嚴格遞增")]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6 }, 0,5)]
        [DataRow(new int[] { 1, 2, 3, 4, 1, 2 }, 0,3)]
        [DataRow(new int[] { 1, 2, 3, 3, 1, 2 }, 0,2)]
        [DataRow(new int[] { 1, 2, 4, 1, 2, 3, 5, 6, 7, 6, 5, 4, 3, 3, 23 }, 3,8)]
        public void StrictlyIncreasingTest(int[] targetData, int lowPoint,int highPoint)
        {
            
               StockDividendService stockDividendService = new StockDividendService(Substitute.For<IDatabaseConnection>());
           var targetdata= stockDividendService.StrictlyIncreasing(targetData.Select(x => (decimal?)x).ToList());
            Assert.AreEqual(targetdata.lowPoint, lowPoint);
            Assert.AreEqual(targetdata.highPoint, highPoint);
        }
    }
}