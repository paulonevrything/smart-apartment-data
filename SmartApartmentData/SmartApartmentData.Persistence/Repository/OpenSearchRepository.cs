using Nest;
using Newtonsoft.Json;
using SmartApartmentData.Domain;
using SmartApartmentData.Persistence.Repository.Interfaces;
using System.Linq;

namespace SmartApartmentData.Persistence.Repository
{

    public class OpenSearchRepository : IOpenSearchRepository
    {

        private readonly IElasticClient _client;
        private const string PROPERTY_MARKET_KEY = "Property.Market";
        private const string MGMT_MARKET_KEY = "Mgmt.Market";
        private static readonly string[] SEARCHABLE_FIELDS = new string[] { "Mgmt.Name", "Mgmt.Market", "Mgmt.State", "Property.State", "Property.Name", "Property.FormerName",
                                                                               "Property.StreetAddress", "Property.City", "Property.Market", };

        public OpenSearchRepository(IElasticClient client)
        {
            _client = client;
        }

        public string Search(string searchPhrase, string[] markets, int limit)
        {

            var boolQuery = new BoolQuery();

            boolQuery.Must = new QueryContainer[] { new MultiMatchQuery
                {
                    Query = searchPhrase,
                    Fields = SEARCHABLE_FIELDS
                }
            };

            if(markets.Length > 0)
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


            var resultDocs = searchResponse.Documents?.ToList();

            var jsonData = JsonConvert.SerializeObject(resultDocs, Formatting.Indented);


            return jsonData;
        }
    }
}
