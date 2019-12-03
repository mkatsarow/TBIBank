using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.UnitTesting.Service.ApplicationServiceTests
{
    [TestClass]
    public class ApplicationService_Should
    {
        [TestMethod]
        public async Task CreateAsync_ShouldCreate()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldCreate));
            var mockApplication = new Mock<LoanApplication>().Object;
            var mockApplicationDTO = new Mock<LoanApplicationDTO>().Object;
            var mockLoanApplicationDTOMapper = new Mock<ILoanApplicationDTOMapper>();

            mockLoanApplicationDTOMapper.Setup(am => am.MapFrom(mockApplicationDTO)).Returns(mockApplication);

            var expected = 1;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var applicationService = new ApplicationService(assertContext, mockLoanApplicationDTOMapper.Object);

                var sut = await applicationService.CreateAsync(mockApplicationDTO);

                var count = assertContext.LoanApplications.Count();

                Assert.AreEqual(expected, count);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnType_LoanApplicationDTO()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldReturnType_LoanApplicationDTO));

            var mockApplication = new Mock<LoanApplication>().Object;
            var mockApplicationDTO = new Mock<LoanApplicationDTO>().Object;
            var mockLoanApplicationDTOMapper = new Mock<ILoanApplicationDTOMapper>();

            mockLoanApplicationDTOMapper.Setup(am => am.MapFrom(mockApplicationDTO)).Returns(mockApplication);

            using (var assertContext = new TBIAppDbContext(options))
            {
                var applicationService = new ApplicationService(assertContext, mockLoanApplicationDTOMapper.Object);

                var sut = await applicationService.CreateAsync(mockApplicationDTO);

                Assert.IsInstanceOfType(sut, typeof(LoanApplicationDTO));

            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldBeCalledOnce()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldBeCalledOnce));

            var mockApplication = new Mock<LoanApplication>().Object;
            var mockApplicationDTO = new Mock<LoanApplicationDTO>().Object;
            var mockLoanApplicationDTOMapper = new Mock<ILoanApplicationDTOMapper>();

            mockLoanApplicationDTOMapper.Setup(am => am.MapFrom(mockApplicationDTO)).Returns(mockApplication);

            var applicationService = new Mock<IApplicationService>();

            applicationService.Setup(x => x.CreateAsync(mockApplicationDTO)).ReturnsAsync(mockApplicationDTO);

            await applicationService.Object.CreateAsync(mockApplicationDTO);

            applicationService.Verify(x => x.CreateAsync(mockApplicationDTO), Times.Once);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldCreate_WithValidParams()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldCreate_WithValidParams));


            var mockApplicationDTO = new Mock<LoanApplicationDTO>().Object;

            mockApplicationDTO.FirstName = "Michail";
            mockApplicationDTO.LastName = "Ivanov";
            mockApplicationDTO.EGN = "8809277777";
            mockApplicationDTO.PhoneNumber = "123151232";
            mockApplicationDTO.Status = LoanApplicationStatus.NotReviewed;
            mockApplicationDTO.CardId = "876123123";

            var mockApplication = new Mock<LoanApplication>().Object;

            mockApplication.FirstName = "Michail";
            mockApplication.LastName = "Ivanov";
            mockApplication.EGN = "8809277777";
            mockApplication.PhoneNumber = "123151232";
            mockApplication.Status = LoanApplicationStatus.NotReviewed;
            mockApplication.CardId = "876123123";

            var mockLoanApplicationDTOMapper = new Mock<ILoanApplicationDTOMapper>();

            mockLoanApplicationDTOMapper.Setup(am => am.MapFrom(mockApplicationDTO)).Returns(mockApplication);

            using (var assertContext = new TBIAppDbContext(options))
            {
                var applicationService = new ApplicationService(assertContext, mockLoanApplicationDTOMapper.Object);

                await applicationService.CreateAsync(mockApplicationDTO);

                var sut = await assertContext.LoanApplications.FirstOrDefaultAsync();

                Assert.AreEqual(mockApplication.FirstName, sut.FirstName);
                Assert.AreEqual(mockApplication.LastName, sut.LastName);
                Assert.AreEqual(mockApplication.EGN, sut.EGN);
                Assert.AreEqual(mockApplication.PhoneNumber, sut.PhoneNumber);
                Assert.AreEqual(mockApplication.Status, sut.Status);
                Assert.AreEqual(mockApplication.CardId, sut.CardId);


            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateAsync_ShouldThrow_ArgumentNullEx()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldThrow_ArgumentNullEx));

            var mockApplication = new Mock<LoanApplication>().Object;
            var mockApplicationDTO = new Mock<LoanApplicationDTO>().Object;
            var mockLoanApplicationDTOMapper = new Mock<ILoanApplicationDTOMapper>();

            mockLoanApplicationDTOMapper.Setup(am => am.MapFrom(mockApplicationDTO)).Returns(mockApplication);

            using (var assertContext = new TBIAppDbContext(options))
            {
                var applicationService = new ApplicationService(assertContext, mockLoanApplicationDTOMapper.Object);

                var sut = await applicationService.CreateAsync(null);

            }
        }
    }
}
