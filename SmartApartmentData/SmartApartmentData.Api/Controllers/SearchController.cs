using Microsoft.AspNetCore.Mvc;
using SmartApartmentData.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SmartApartmentData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // TODO: Write tests

        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string searchPhrase, [FromQuery] string[] markets, [FromQuery] int limit = 25)
        {
            if (String.IsNullOrEmpty(searchPhrase))
                return BadRequest();

            return Ok(_searchService.Search(searchPhrase, markets, limit));
        }

    }
}
