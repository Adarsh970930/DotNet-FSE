using NUnit.Framework;
using Moq;
using CustomerCommLib;

namespace CustomerComm.Tests
{
    [TestFixture]
    public class CustomerCommTests
    {
        private Mock<IMailSender> _mailSenderMock;
        private CustomerComm _customerComm;

        [OneTimeSetUp]
        public void Init()
        {
            _mailSenderMock = new Mock<IMailSender>();
            _mailSenderMock
                .Setup(sender => sender.SendMail(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _customerComm = new CustomerComm(_mailSenderMock.Object);
        }

        [TestCase]
        public void SendMailToCustomer_ReturnsTrue()
        {
            var result = _customerComm.SendMailToCustomer();

            Assert.IsTrue(result);
            _mailSenderMock.Verify(
                sender => sender.SendMail(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}