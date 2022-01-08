using Microsoft.Extensions.Logging;
using SmartApartmentData.Core.Services.Interfaces;
using SmartApartmentData.Persistence.Repository.Interfaces;
using System;

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

        public string Search(string searchPhrase, string[] markets, int limit)
        {
            try
            {
                // TODO: Do autocomplete

                var result = _openSearchRepository.Search(searchPhrase, markets, limit);

                return result;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
