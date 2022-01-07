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
        private const string PROPERTY_MARKET_KEY = "Property.Market";
        private const string MGMT_MARKET_KEY = "Mgmt.Market";

        public OpenSearchRepository(IElasticClient client)
        {
            _client = client;
        }

        public ISearchResponse<object> Search(string searchPhrase, string[] markets, int limit)
        {

            var boolQuery = new BoolQuery();

            boolQuery.Must = new QueryContainer[] { new MultiMatchQuery
                {
                    Query = searchPhrase,
                }
            };

            if(markets.Length > 0 && !string.IsNullOrEmpty(markets[0]))
            {
                boolQuery.Filter = new QueryContainer[] { new MultiMatchQuery
                { 
                    Fields = Infer.Field(PROPERTY_MARKET_KEY).And(MGMT_MARKET_KEY),
                    Query =  string.Join(" ", markets)
                }};
            }

            var searchResponse = _client.Search<dynamic>(s => s
                .Index(($"{Constants.PropertyIndex},{Constants.ManagementIndex}"))
                .Size(limit)
                .Query(c => boolQuery)
            );

            // TODO: Retrieve data from searchResponse

            foreach (var document in searchResponse.Documents)
                Console.WriteLine($"document is a {document.GetType().Name}");


            return searchResponse;
        }
    }
}
