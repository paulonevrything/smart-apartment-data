using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartApartmentData.Api.Controllers;
using SmartApartmentData.Core.Services.Interfaces;
using SmartApartmentData.Persistence.Repository.Interfaces;

namespace SmartApartmentData.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTests
    {
        Mock<ISearchService> _searchService;
        Mock<IOpenSearchRepository> _openSearchService;

        [TestMethod]
        public void Search_ShouldReturnEmptyArray()
        {
            // Arrange

            var expectedResult = JsonConvert.SerializeObject(new object[0]);

            IActionResult actionResult = new OkObjectResult(expectedResult);

            _searchService = new Mock<ISearchService>();
            _openSearchService = new Mock<IOpenSearchRepository>();

            _searchService.Setup(p => p.Search("and", null, 10)).Returns(expectedResult);
            _openSearchService.Setup(p => p.Search("and", null, 10)).Returns(expectedResult);

            // Act
            var controller = new SearchController(_searchService.Object);

            var result = controller.Search("and", null, 0) as OkObjectResult;

            // Assert
            NUnit.Framework.Assert.AreEqual(actionResult, result);
            //result.Value;
        }

    }
}
