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

        public string Search(string searchPhrase, string[] markets, int limit)
        {
            try
            {
                // TODO: Do autocomplete

                var result = _openSearchRepository.Search(searchPhrase, markets, limit);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }

            return "done";
        }
    }
}
