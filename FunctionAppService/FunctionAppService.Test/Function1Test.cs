using FunctionAppService;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Net;

namespace FunctionAppService.Test
{
    public class Function1Test
    {
        // <summary>A <see cref="MemoryStream"/> used for a mocked <see cref="HttpResponseData"/> instance.</summary>
        private MemoryStream memoryStream = null!;

        /// <summary>A mock of <see cref="HttpRequestData"/>.</summary>
        private Mock<HttpRequestData> httpRequestDataMock = null!;

        private Mock<FunctionContext> functionContextMock = null!;

        [SetUp]
        public void Setup()
        {
            this.memoryStream = new MemoryStream();
            this.functionContextMock = new Mock<FunctionContext>();
            this.httpRequestDataMock = new Mock<HttpRequestData>(this.functionContextMock.Object);

            this.httpRequestDataMock.Setup(req => req.CreateResponse()).Returns(() =>
            {
                var httpResponseDataMock = new Mock<HttpResponseData>(this.functionContextMock.Object);
                httpResponseDataMock.SetupProperty(res => res.Headers, new HttpHeadersCollection());
                httpResponseDataMock.SetupProperty(res => res.StatusCode);
                var memoryStream = this.memoryStream;
                httpResponseDataMock.SetupProperty(res => res.Body, memoryStream);
                return httpResponseDataMock.Object;
            });
        }

        [Test]
        public void BadRequestTest()
        {
            // Arrange
            var function = new Function1();
            var expected = HttpStatusCode.BadRequest;

            // Act
            var response = function.Run(this.httpRequestDataMock.Object, "");

            // Assert
            Assert.AreEqual(expected, response.StatusCode);
        }

        [Test]
        public void OKTest()
        {
            // Arrange
            var function = new Function1();
            var expected = HttpStatusCode.OK;

            // Act
            var response = function.Run(this.httpRequestDataMock.Object, "Anthony");

            // Assert
            Assert.AreEqual(expected, response.StatusCode);
        }
    }
}