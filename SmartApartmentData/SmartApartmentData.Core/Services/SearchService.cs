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
        private readonly IOpenSearchRepository _openSearchRepository;
        readonly ILogger<SearchService> _logger;
        
        public SearchService(IOpenSearchRepository openSearchRepository, ILogger<SearchService> logger)
        {
            _openSearchRepository = openSearchRepository;
            _logger = logger;
        }

        public async Task<string> SearchAsync(string searchPhrase, string market, int limit)
        {
            try
            {

                var result = await _openSearchRepository.SearchAsync(searchPhrase, market, limit);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }

            return "done";
        }
    }
}
