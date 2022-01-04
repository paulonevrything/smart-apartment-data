using Microsoft.Extensions.Logging;
using SmartApartmentData.Core.Services.Interfaces;
using SmartApartmentData.Persistence.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApartmentData.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOpenSearchRepository _openSearch;
        readonly ILogger<SearchService> _logger;
        
        public SearchService(IOpenSearchRepository openSearch, ILogger<SearchService> logger)
        {
            _openSearch = openSearch;
            _logger = logger;
        }

        public Task<string> SearchAsync(string searchPhrase, string market, int limit)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }

            throw new NotImplementedException();
        }
    }
}
