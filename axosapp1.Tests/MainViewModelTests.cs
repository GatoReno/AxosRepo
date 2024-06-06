using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axosapp1.Tests
{
      [Test]
        public async Task LoadCatsAsync_PopulatesCatCollection()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(new[]
                    {
                        new CatModel { Id = "1", Url = "https://example.com/cat1.png", Width = 200, Height = 200 },
                        new CatModel { Id = "2", Url = "https://example.com/cat2.png", Width = 200, Height = 200 }
                    }))
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var viewModel = new MainViewModel
            {
                HttpClient = httpClient // Set the HttpClient to the mock client
            };

            // Act
            await viewModel.LoadCatsAsync();

            // Assert
            Assert.IsNotEmpty(viewModel.CatCollection);
            Assert.AreEqual(2, viewModel.CatCollection.Count);
            Assert.AreEqual("1", viewModel.CatCollection[0].Id);
            Assert.AreEqual("https://example.com/cat1.png", viewModel.CatCollection[0].Url);
            Assert.AreEqual("2", viewModel.CatCollection[1].Id);
            Assert.AreEqual("https://example.com/cat2.png", viewModel.CatCollection[1].Url);
        }
}