using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.UnitTesting.Service.CheckEgnServiceTests
{
    [TestClass]
    public class CheckEgnService_Should
    {
        [TestMethod]
        public async Task IsRealAsync_ShouldReturn_True()
        {
            //Mock
            var testEgn = "8810120964";

            var checkEgnService = new CheckEgnService();

            var sut = await checkEgnService.IsRealAsync(testEgn);

            var expectedResult = true;

            Assert.AreEqual(expectedResult, sut);

        }

        [TestMethod]
        public async Task IsRealAsync_ShouldBeCalledOnce()
        {
            var testEgn = "8810120964";
            var testResult = true;

            var checkEgnService = new Mock<ICheckEgnService>();
            checkEgnService.Setup(x => x.IsRealAsync(testEgn)).ReturnsAsync(testResult);

            await checkEgnService.Object.IsRealAsync(testEgn);

            checkEgnService.Verify(x=>x.IsRealAsync(testEgn),Times.Once);

        }

        [TestMethod]
        public async Task IsRealAsync_ShouldReturnTypeOfBool()
        {
            var testEgn = "8810120964";

            var checkEgnService = new CheckEgnService();

            var sut = await checkEgnService.IsRealAsync(testEgn);

            Assert.IsInstanceOfType(sut, typeof(bool));
        }

        [TestMethod]
        public async Task IsRealAsync_ShouldReturn_False()
        {
            var testEgn = "1010120964";

            var checkEgnService = new CheckEgnService();

            var sut = await checkEgnService.IsRealAsync(testEgn);

            var expectedResult = false;

            Assert.AreEqual(expectedResult, sut);

        }

        [TestMethod]
        public async Task isValidMonthAndDate_ShouldReturn_True()
        {
            var testEgn = new int[] { 8, 8, 1, 0, 1, 2, 0, 9, 6, 4 };

            var checkEgnService = new CheckEgnService();

            var sut = checkEgnService.isValidMonthAndDate(testEgn);

            var expectedResult = true;

            Assert.AreEqual(expectedResult, sut);

        }

        [TestMethod]
        public async Task isValidMonthAndDate_ShouldReturn_False()
        {
            var testEgn = new int[] { 1,0,1,0,1,2,0,9,6,4 };

            var checkEgnService = new CheckEgnService();

            var sut = checkEgnService.isValidMonthAndDate(testEgn);

            var expectedResult = true;

            Assert.AreEqual(expectedResult, sut);

        }

        [TestMethod]
        public async Task isValidMonthAndDate_IsCalledOnce()
        {
            var testEgn = new int[] { 1, 0, 1, 0, 1, 2, 0, 9, 6, 4 };
            var expectedResult = true;

            var mockCheckEngService = new Mock<ICheckEgnService>();
            mockCheckEngService.Setup(x => x.isValidMonthAndDate(testEgn)).Returns(expectedResult);

            mockCheckEngService.Object.isValidMonthAndDate(testEgn);

            mockCheckEngService.Verify(x => x.isValidMonthAndDate(testEgn), Times.Once);

        }

        [TestMethod]
        public async Task isValidMonthAndDate_ShouldReturnTypeOf()
        {
            var testEgn = new int[] { 8, 8, 1, 0, 1, 2, 0, 9, 6, 4 };

            var checkEgnService = new CheckEgnService();

            var sut = checkEgnService.isValidMonthAndDate(testEgn);
           
            Assert.IsInstanceOfType(sut,typeof(bool));

        }
    }
}
