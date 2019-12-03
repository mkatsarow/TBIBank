using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.UnitTesting.Service.EmailServiceTest
{
    [TestClass]
    public class EmailService_Should
    {

        [TestMethod]
        public async Task CreateAsync_ShouldCreateEmail()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldCreateEmail));

            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null,
                null, null, null, null, null, null).Object;

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>();
            var mockEmail = new Mock<Email>().Object;
            var mockEmailDTO = new Mock<EmailDTO>().Object;
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmailDTO)).Returns(mockEmail);


            var expectedResult = 1;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext,
                    mockEmailDTOMapper.Object, mockDecodeService, 
                    mockLogger, null, mockEncryptService) ;

                var sut = await emailService.CreateAsync(mockEmailDTO);

                Assert.AreEqual(expectedResult, assertContext.Emails.Count());
                
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturn_EmailDTO()
        {

            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldReturn_EmailDTO));

            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>();
            var mockEmail = new Mock<Email>().Object;
            var mockEmailDTO = new Mock<EmailDTO>().Object;
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmailDTO)).Returns(mockEmail);
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmail)).Returns(mockEmailDTO);

            using (var assertContext = new TBIAppDbContext(options))
            {
                var mockEmailService = new EmailService(assertContext, mockEmailDTOMapper.Object, 
                    mockDecodeService, mockLogger,mockUserManager,mockEncryptService);

                var sut = await mockEmailService.CreateAsync(mockEmailDTO);

                Assert.IsInstanceOfType(sut, typeof(EmailDTO));
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldBeCalledOnce()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldBeCalledOnce));

            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>();
            var mockEmail = new Mock<Email>().Object;
            var mockEmailDTO = new Mock<EmailDTO>().Object;
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmailDTO)).Returns(mockEmail);
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmail)).Returns(mockEmailDTO);

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.CreateAsync(mockEmailDTO)).ReturnsAsync(mockEmailDTO);

            await mockEmailService.Object.CreateAsync(mockEmailDTO);

            mockEmailService.Verify(x=> x.CreateAsync(mockEmailDTO),Times.Once);
            
        }

        [TestMethod]
        public async Task CreateAsync_ShouldCreateWithValidParams()
        {

            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldCreateWithValidParams));

            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockAttachment = new Mock<Attachment>().Object;
            var mockAttachmentDTO = new Mock<AttachmentDTO>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;



            var testDate = DateTime.Now;

            var mockEmail = new Mock<Email>().Object;
            mockEmail.Id = "TestId";
            mockEmail.IsOpne = true;
            mockEmail.LastOpen = testDate;
            mockEmail.LastStatusUpdate = testDate;
            mockEmail.RegisteredInDataBase = testDate;
            mockEmail.Sender = "TestSender";

            var mockEmailDTO = new Mock<EmailDTO>().Object;
            mockEmailDTO.Id = "TestId";
            mockEmailDTO.IsOpne = true;
            mockEmailDTO.LastOpen = testDate;
            mockEmailDTO.LastStatusUpdate = testDate;
            mockEmailDTO.RegisteredInDataBase = testDate;
            mockEmailDTO.Sender = "TestSender";

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>();
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmailDTO)).Returns(mockEmail);
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmail)).Returns(mockEmailDTO);

            using (var assertContext = new TBIAppDbContext(options))
            {

                var mockEmailService = new EmailService(assertContext, mockEmailDTOMapper.Object, 
                    mockDecodeService, mockLogger,mockUserManager, mockEncryptService);

                await mockEmailService.CreateAsync(mockEmailDTO);

                var sut = assertContext.Emails.FirstOrDefault();

                Assert.AreEqual(mockEmail.Id, sut.Id);
                Assert.AreEqual(mockEmail.IsOpne, sut.IsOpne);
                Assert.AreEqual(mockEmail.LastOpen, sut.LastOpen);
                Assert.AreEqual(mockEmail.LastStatusUpdate, sut.LastStatusUpdate);
                Assert.AreEqual(mockEmail.RegisteredInDataBase, sut.RegisteredInDataBase);
                Assert.AreEqual(mockEmail.Sender, sut.Sender);
               
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateAsync_ShouldThrow_ArgumentNullEx()
        {

            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldThrow_ArgumentNullEx));

            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;


            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>();
            var mockEmail = new Mock<Email>().Object;
            var mockEmailDTO = new Mock<EmailDTO>().Object;
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmailDTO)).Returns(mockEmail);
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmail)).Returns(mockEmailDTO);

            using (var assertContext = new TBIAppDbContext(options))
            {
                var mockEmailService = new EmailService(assertContext, mockEmailDTOMapper.Object,
                    mockDecodeService, mockLogger,mockUserManager, mockEncryptService);

                var sut = await mockEmailService.CreateAsync(null);
            }
        }

        [TestMethod]
        public async Task IsOpenAsync_ShouldGet_True()
        {
            var options = TestUtilities.GetOptions(nameof(IsOpenAsync_ShouldGet_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;

         


            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "testId";
            mockEmail.Id = testId;
            mockEmail.IsOpne = true;

            var expectedResult = true;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper, 
                    mockDecodeService, mockLogger,null, mockEncryptService);

                var sut = await emailService.IsOpenAsync(testId);

                Assert.AreEqual(expectedResult, sut);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task IsOpenAsync_ShouldThrowArgumentNullEx()
        {
            var options = TestUtilities.GetOptions(nameof(IsOpenAsync_ShouldThrowArgumentNullEx));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = string.Empty;
            mockEmail.Id = "correctTestId";
            mockEmail.IsOpne = true;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper,
                    mockDecodeService, mockLogger,mockUserManager, mockEncryptService);

                var sut = await emailService.IsOpenAsync(testId);
            }
        }

        [TestMethod]
        public async Task IsOpenAsync_ShouldBeCalledOnce()
        {
            var options = TestUtilities.GetOptions(nameof(IsOpenAsync_ShouldBeCalledOnce));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testInput = "testId";
            var testResult = true;

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.IsOpenAsync(testInput)).ReturnsAsync(testResult);

            await mockEmailService.Object.IsOpenAsync(testInput);

            mockEmailService.Verify(x => x.IsOpenAsync(testInput), Times.Once);

        }


        [TestMethod]
        public async Task IsOpenAsync_ShouldReturnTypeOfBool()
        {
            var options = TestUtilities.GetOptions(nameof(IsOpenAsync_ShouldReturnTypeOfBool));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testInput = "testId";
            var testResult = true;

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.IsOpenAsync(testInput)).ReturnsAsync(testResult);

            var sut = await mockEmailService.Object.IsOpenAsync(testInput);

            Assert.IsInstanceOfType(sut, typeof(bool));

        }

        [TestMethod]
        public async Task IsOpenAsync_ShouldGet_False()
        {
            var options = TestUtilities.GetOptions(nameof(IsOpenAsync_ShouldGet_False));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;



            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "testId";
            mockEmail.Id = testId;
            mockEmail.IsOpne = false;

            var expectedResult = false;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper,
                    mockDecodeService, mockLogger,mockUserManager, mockEncryptService);

                var sut = await emailService.IsOpenAsync(testId);

                Assert.AreEqual(expectedResult, sut);
            }
        }

        [TestMethod]
        public async Task LockButtonAsync_ShouldSetIsOpenToTrue()
        {
            var options = TestUtilities.GetOptions(nameof(LockButtonAsync_ShouldSetIsOpenToTrue));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "testId";
            mockEmail.Id = testId;
            mockEmail.IsOpne = false;

            var expectedResult = true;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper,
                    mockDecodeService, mockLogger, mockUserManager, mockEncryptService);

                await emailService.LockButtonAsync(testId);

                var sut = assertContext.Emails.FirstOrDefault();

                Assert.AreEqual(expectedResult, sut.IsOpne);
            }
        }

        [TestMethod]
        public async Task LockButtonAsync_ShouldReturnTypeOfBool()
        {
            var options = TestUtilities.GetOptions(nameof(LockButtonAsync_ShouldReturnTypeOfBool));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "testId";
            mockEmail.Id = testId;
            mockEmail.IsOpne = false;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper,
                    mockDecodeService, mockLogger,mockUserManager, mockEncryptService);

                await emailService.LockButtonAsync(testId);

                var email = assertContext.Emails.FirstOrDefault();

                var sut = email.IsOpne;

                Assert.IsInstanceOfType(sut, typeof(bool));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LockButtonAsync_ShouldThrow_ArgumentNullEx()
        {
            var options = TestUtilities.GetOptions(nameof(LockButtonAsync_ShouldThrow_ArgumentNullEx));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var testId = "TestId";

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper,
                    mockDecodeService, mockLogger,mockUserManager, mockEncryptService);

                await emailService.LockButtonAsync(testId);
            }
        }

        [TestMethod]
        public async Task LockButtonAsync_ShouldBeCalledOnce()
        {
            var options = TestUtilities.GetOptions(nameof(LockButtonAsync_ShouldBeCalledOnce));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "TestId";
            mockEmail.Id = testId;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var mockEmailService = new Mock<IEmailService>();
                mockEmailService.Setup(x => x.LockButtonAsync(testId));

                await mockEmailService.Object.LockButtonAsync(testId);

                mockEmailService.Verify(x => x.LockButtonAsync(testId),Times.Once);
            }
        }

        [TestMethod]
        public async Task UnLockButtonAsync_ShouldSetIsOpenToFalse()
        {
            var options = TestUtilities.GetOptions(nameof(UnLockButtonAsync_ShouldSetIsOpenToFalse));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "testId";
            mockEmail.Id = testId;
            mockEmail.IsOpne = true;

            var expectedResult = false;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper,
                    mockDecodeService, mockLogger, mockUserManager,mockEncryptService);

                await emailService.UnLockButtonAsync(testId);

                var sut = assertContext.Emails.FirstOrDefault();

                Assert.AreEqual(expectedResult, sut.IsOpne);
            }
        }

        [TestMethod]
        public async Task UnLockButtonAsync_ShouldReturnTypeOfBool()
        {
            var options = TestUtilities.GetOptions(nameof(UnLockButtonAsync_ShouldReturnTypeOfBool));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;

            var userStoreMock = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null).Object;

            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "testId";
            mockEmail.Id = testId;
            mockEmail.IsOpne = true;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper,
                    mockDecodeService, mockLogger, mockUserManager, mockEncryptService);

                await emailService.UnLockButtonAsync(testId);

                var email = assertContext.Emails.FirstOrDefault();

                var sut = email.IsOpne;

                Assert.IsInstanceOfType(sut, typeof(bool));
            }
        }

        [TestMethod]
        public async Task UnLockButtonAsync_ShouldBeCalledOnce()
        {
            var options = TestUtilities.GetOptions(nameof(UnLockButtonAsync_ShouldBeCalledOnce));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "testId";
            mockEmail.Id = testId;
            mockEmail.IsOpne = true;


            using (var assertContext = new TBIAppDbContext(options))
            {
                var mockEmailService = new Mock<IEmailService>();
                mockEmailService.Setup(x => x.UnLockButtonAsync(testId));

                await mockEmailService.Object.UnLockButtonAsync(testId);

                mockEmailService.Verify(x=> x.UnLockButtonAsync(testId),Times.Once);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UnLockButtonAsync_ShouldThrow_ArgumentNullEx()
        {
            var options = TestUtilities.GetOptions(nameof(UnLockButtonAsync_ShouldThrow_ArgumentNullEx));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "testId";
            mockEmail.Id = testId;
            mockEmail.IsOpne = true;


            using (var assertContext = new TBIAppDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailDTOMapper, mockDecodeService,
                    mockLogger,mockUserManager, mockEncryptService);

                await sut.UnLockButtonAsync(testId);
            }
        }

        [TestMethod]
        public async Task ChangeStatusAsync_ShouldChangeStatus()
        {
            var options = TestUtilities.GetOptions(nameof(ChangeStatusAsync_ShouldChangeStatus));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;

            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEmail = new Mock<Email>().Object;

            var testId = "testId";
            var testStatus = EmailStatusesEnum.New;
            mockEmail.Id = testId;
            mockEmail.Status = testStatus;

            var mockUser = new Mock<User>().Object;
            var newEmailStatus = EmailStatusesEnum.Open;

            var expectedResult = newEmailStatus;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper, 
                    mockDecodeService, mockLogger,mockUserManager, mockEncryptService);

                await emailService.ChangeStatusAsync(testId, newEmailStatus,mockUser);

                var sut = assertContext.Emails.FirstOrDefault();

                Assert.AreEqual(expectedResult, sut.Status);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ChangeStatusAsync_ShouldThrow_ArgumentNullExs()
        {
            var options = TestUtilities.GetOptions(nameof(ChangeStatusAsync_ShouldThrow_ArgumentNullExs));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var testId = "testId";
            var mockUser = new Mock<User>().Object;
            var newEmailStatus = EmailStatusesEnum.Open;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailDTOMapper, 
                    mockDecodeService, mockLogger, null,mockEncryptService);

                await sut.ChangeStatusAsync(testId, newEmailStatus, mockUser);
            }
        }

        [TestMethod]
        public async Task ChangeStatusAsync_ShouldBeCalledOnce()
        {
            var options = TestUtilities.GetOptions(nameof(ChangeStatusAsync_ShouldBeCalledOnce));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockUser = new Mock<User>().Object;

            var testId = "testId";
            var testStatus = EmailStatusesEnum.New;

            var mockEmail = new Mock<Email>().Object;
            mockEmail.Id = testId;
            mockEmail.Status = testStatus;

            var emailService = new Mock<IEmailService>();
            emailService.Setup(x => x.ChangeStatusAsync(testId, testStatus, mockUser));

            await emailService.Object.ChangeStatusAsync(testId, testStatus, mockUser);

            emailService.Verify(x => x.ChangeStatusAsync(testId, testStatus, mockUser), Times.Once);

        }

        [TestMethod]
        public async Task GetEmailsPagesByTypeAsync_ShouldGet_True()
        {
            var options = TestUtilities.GetOptions(nameof(GetEmailsPagesByTypeAsync_ShouldGet_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;

            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;

            var status = 1;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(
                    new Email
                    {
                        Status = (EmailStatusesEnum)status
                    });

                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailDTOMapper, 
                    decodeService, mockLogger, mockUserManager, mockEncryptService);

                var result = await sut.GetEmailsPagesByTypeAsync((EmailStatusesEnum)status);

                Assert.AreEqual(1, result);
            };
        }

        [TestMethod]
        public async Task GetEmailsPagesByTypeAsync_ShouldBeCalledOnce()
        {
            var options = TestUtilities.GetOptions(nameof(GetEmailsPagesByTypeAsync_ShouldBeCalledOnce));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;

            var testStatus = EmailStatusesEnum.InvalidApplication;
            var testReturn = 1;

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.GetEmailsPagesByTypeAsync(testStatus)).ReturnsAsync(testReturn);

            await mockEmailService.Object.GetEmailsPagesByTypeAsync(testStatus);

            mockEmailService.Verify(x => x.GetEmailsPagesByTypeAsync(testStatus), Times.Once);

        }

        [TestMethod]
        public async Task GetEmailsPagesByTypeAsync_ShouldReturnTypeOfInt()
        {
            var options = TestUtilities.GetOptions(nameof(GetEmailsPagesByTypeAsync_ShouldReturnTypeOfInt));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;

            var testStatus = EmailStatusesEnum.InvalidApplication;
            var testReturn = 1;

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.GetEmailsPagesByTypeAsync(testStatus)).ReturnsAsync(testReturn);

            var sut = await mockEmailService.Object.GetEmailsPagesByTypeAsync(testStatus);

            Assert.IsInstanceOfType(sut, typeof(int));
        }

        [TestMethod]
        public async Task GetCurrentPageEmailsAsync_ThrowEx_True()
        {
            var options = TestUtilities.GetOptions(nameof(GetCurrentPageEmailsAsync_ThrowEx_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUser = new Mock<User>().Object;

            var statusOfEmail = EmailStatusesEnum.NotReviewed;
            var page = 1;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailDTOMapper, 
                    decodeService, mockLogger,mockUserManager, mockEncryptService);

                var result = await sut.GetCurrentPageEmailsAsync(page, statusOfEmail,mockUser);

                Assert.IsNull(result);
            };
        }

        [TestMethod]
        public async Task GetAllEmailsPagesAsync_ShouldGet_True()
        {
            var options = TestUtilities.GetOptions(nameof(GetAllEmailsPagesAsync_ShouldGet_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null).Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var expectedCount = 1;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(new Email());
                await actContext.Emails.AddAsync(new Email());

                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailDTOMapper, 
                    mockDecodeService, mockLogger,null, mockEncryptService);

                var result = await sut.GetAllEmailsPagesAsync();

                Assert.AreEqual(expectedCount, result);
            }
        }

        [TestMethod]
        public async Task GetAllEmailsPagesAsync_ShouldBeCalledOnce()
        {
            var options = TestUtilities.GetOptions(nameof(GetAllEmailsPagesAsync_ShouldBeCalledOnce));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var testResult = 1;

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.GetAllEmailsPagesAsync()).ReturnsAsync(testResult);

            await mockEmailService.Object.GetAllEmailsPagesAsync();

            mockEmailService.Verify(x => x.GetAllEmailsPagesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAllEmailsPagesAsync_ShouldReturn_TypeOf()
        {
            var options = TestUtilities.GetOptions(nameof(GetAllEmailsPagesAsync_ShouldBeCalledOnce));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var testResult = 1;

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.GetAllEmailsPagesAsync()).ReturnsAsync(testResult);

            var sut  =await mockEmailService.Object.GetAllEmailsPagesAsync();

            Assert.IsInstanceOfType(sut, typeof(int));
        }

    }
}
