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

namespace TBIApp.UnitTesting.Service.AttachmentServiceTests
{
    [TestClass]
    public class AttachmentServices_Should
    {
        [TestMethod]
        public async Task CreateAsync_ShoulCreate()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShoulCreate));

            var mockAttachmentDTO = new Mock<AttachmentDTO>().Object;
            var mockAttachment = new Mock<Attachment>().Object;
            var mockAttachmentDTOMapper = new Mock<IAttachmentDTOMapper>();
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachmentDTO)).Returns(mockAttachment);

            var expectedResult = 1;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var attachmentService = new AttachmentService(assertContext, mockAttachmentDTOMapper.Object);

                var sut = await attachmentService.CreateAsync(mockAttachmentDTO);

                Assert.AreEqual(expectedResult, assertContext.Attachments.Count());

            };
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturn_AttachmentDTO()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldReturn_AttachmentDTO));

            var mockAttachmentDTO = new Mock<AttachmentDTO>().Object;
            var mockAttachment = new Mock<Attachment>().Object;
            var mockAttachmentDTOMapper = new Mock<IAttachmentDTOMapper>();
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachmentDTO)).Returns(mockAttachment);
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachment)).Returns(mockAttachmentDTO);

         
            using (var assertContext = new TBIAppDbContext(options))
            {
                var attachmentService = new AttachmentService(assertContext, mockAttachmentDTOMapper.Object);

                var sut = await attachmentService.CreateAsync(mockAttachmentDTO);

                Assert.IsInstanceOfType(sut, typeof(AttachmentDTO));

            };
        }

        [TestMethod]
        public async Task CreateAsync_ShouldBeCalledOnce()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldBeCalledOnce));

            var mockAttachmentDTO = new Mock<AttachmentDTO>().Object;
            var mockAttachment = new Mock<Attachment>().Object;
            var mockAttachmentDTOMapper = new Mock<IAttachmentDTOMapper>();
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachmentDTO)).Returns(mockAttachment);
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachment)).Returns(mockAttachmentDTO);

            var mockAttachmentService = new Mock<IAttachmentService>();
            mockAttachmentService.Setup(x => x.CreateAsync(mockAttachmentDTO)).ReturnsAsync(mockAttachmentDTO);

            await mockAttachmentService.Object.CreateAsync(mockAttachmentDTO);

            mockAttachmentService.Verify(x=> x.CreateAsync(mockAttachmentDTO),Times.Once);

        }

        [TestMethod]
        public async Task CreateAsync_ShouldCreateWithValidParams()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldCreateWithValidParams));

            var mockAttachment = new Mock<Attachment>().Object;
            mockAttachment.FileName = "TestPictureName";
            mockAttachment.SizeKb = 100.00;
            mockAttachment.SizeMb = 200.00;

            var mockAttachmentDTO = new Mock<AttachmentDTO>().Object;
            mockAttachmentDTO.FileName = "TestPictureName";
            mockAttachmentDTO.SizeKb = 100.00;
            mockAttachmentDTO.SizeMb = 200.00;

            var mockAttachmentDTOMapper = new Mock<IAttachmentDTOMapper>();
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachmentDTO)).Returns(mockAttachment);
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachment)).Returns(mockAttachmentDTO);

            using (var assertContext = new TBIAppDbContext(options))
            {
                var attachmentService = new AttachmentService(assertContext, mockAttachmentDTOMapper.Object);

                await attachmentService.CreateAsync(mockAttachmentDTO);

                var sut = await assertContext.Attachments.FirstOrDefaultAsync();

                Assert.AreEqual(mockAttachmentDTO.FileName, sut.FileName);
                Assert.AreEqual(mockAttachmentDTO.SizeKb, sut.SizeKb);
                Assert.AreEqual(mockAttachmentDTO.SizeMb, sut.SizeMb);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateAsync_ShouldThrow_ArgumentNullEx()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldThrow_ArgumentNullEx));

            var mockAttachment = new Mock<Attachment>().Object;
            var mockAttachmentDTO = new Mock<AttachmentDTO>().Object;
            var mockAttachmentDTOMapper = new Mock<IAttachmentDTOMapper>();
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachmentDTO)).Returns(mockAttachment);
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachment)).Returns(mockAttachmentDTO);

            using (var assertContext = new TBIAppDbContext(options))
            {
                var attachmentService = new AttachmentService(assertContext, mockAttachmentDTOMapper.Object);

                await attachmentService.CreateAsync(null);
            }
        }
    }
}
