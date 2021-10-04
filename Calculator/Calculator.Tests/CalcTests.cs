using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Calc_Sum_2_and_3_results_5()
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            int result = calc.Sum(2, 3);

            //Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Calc_Sum_0_and_0_results_0()
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            int result = calc.Sum(0, 0);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(9, 2, 11)]
        [DataRow(-4, 5, 1)]
        [DataRow(-4, -7, -11)]
        public void Calc_Sum(int a, int b, int expected)
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            int result = calc.Sum(a, b);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Calc_Sum_MAX_and_1_throws_OverflowsException()
        {
            Calc calc = new Calc();

            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }


        [TestMethod]
        public void Calc_IsWeekend()
        {
            var calc = new Calc();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 4); //mo
                Assert.IsFalse(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 5); //di
                Assert.IsFalse(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 6); //mi
                Assert.IsFalse(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 7); //do
                Assert.IsFalse(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 8); //fr
                Assert.IsFalse(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 9); //sa
                Assert.IsTrue(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 10); //so
                Assert.IsTrue(calc.IsWeekend());
            }
        }
    }
}