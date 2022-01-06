using Nest;
using SmartApartmentData.Domain;
using SmartApartmentData.Domain.Model;
using SmartApartmentData.Persistence.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApartmentData.Persistence.Repository
{

    public class OpenSearchRepository : IOpenSearchRepository
    {

        private readonly IElasticClient _client;

        public OpenSearchRepository(IElasticClient client)
        {
            _client = client;
        }

        public async Task<ISearchResponse<object>> SearchAsync(string searchPhrase, string market, int limit)
        {

            var result = _client.Search<dynamic>(s => s
                .Index($"{Constants.PropertyIndex},{Constants.ManagementIndex}")
                .Size(limit)
                .Query(q => q
                    .Match(m => m
                        .Query(searchPhrase)
                    ))

                );

            //result.

            return result;
        }
    }
}
