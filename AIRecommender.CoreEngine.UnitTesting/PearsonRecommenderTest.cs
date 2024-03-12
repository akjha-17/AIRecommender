using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AIRecommender.CoreEngine.UnitTesting
{
    [TestClass]
    public class PearsonRecommenderTest
    {

        PearsonRecommender target = null;
        [TestInitialize]
        public void Init()
        {
            // Before every Method
            target = new PearsonRecommender();
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void GetCorelation_AnyElementLessThanZero_ShouldThrowException()
        {
            //Calculator target=new Calculator();
            int[] baseArr = {1,2,0};
            int[]otherArr = {1,2,-2};
            double actual = target.GetCorellation(baseArr, otherArr);
        }
        [TestMethod]
        public void GetCorelation_OtherDataGreaterThanBaseData_ShouldTrimElements()
        {
            //Calculator target=new Calculator();
            int[] baseArr = { 3,4,5};
            int[] otherArr = { 1,2,3,4,5,6 };
            double actual = target.GetCorellation(baseArr, otherArr);
            Assert.IsTrue(actual >= -1 && actual <= 1, $"Result {actual} is not in the range [-1, 1]");
        }
        [TestMethod]
        public void GetCorelation_BaseDataGreaterThanOtherData_ShouldFillElements()
        {
            //Calculator target=new Calculator();
            int[] baseArr = {1,2,3,4,5,6};
            int[] otherArr = {1,2,3};
            double actual = target.GetCorellation(baseArr, otherArr);
            Assert.IsTrue(actual >= -1 && actual <= 1, $"Result {actual} is not in the range [-1, 1]");

        }
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void GetCorelation_BaseDataANDOtherDataIsSame_ShouldFillElements()
        {
            //Calculator target=new Calculator();
            int[] baseArr = { 0,0,0 };
            int[] otherArr = { 0,0,0 };
            double actual = target.GetCorellation(baseArr, otherArr);
        }
        /*[TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetCorelation_OtherDataSizeIsZero_ShouldThrowException()
        {
            int[] baseArr = {1,2};
            int[] otherArr = {};
            double actual = target.GetCorellation(baseArr, otherArr);
        }*/

    }
}
