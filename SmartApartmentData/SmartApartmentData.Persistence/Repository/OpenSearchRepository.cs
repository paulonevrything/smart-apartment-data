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


            // var boolQuery = new BoolQuery();
            // boolQuery.Must = new QueryContainer[] { new MultiMatchQuery
            //     {
            //         Query = "stone",
            //         Fields = "*"
            //     }
            // };

            // var searchResponse = elasticClient.Search<object>(s => s
            //     .Index(($"{Constants.PropertyIndex},{Constants.ManagementIndex}"))
            //     .Size(150)
            //     .Query(q => q
            //         .QueryString(m => m
            //             .Query("stone")
            //         ))
            // );

            // foreach (var document in searchResponse.Documents)
            //     Console.WriteLine($"document is a {document.GetType().Name}");


            var result = _client.Search<dynamic>(s => s
                .Index($"{Constants.PropertyIndex},{Constants.ManagementIndex}")
                .Size(limit)
                .Query(q => q
                    .QueryString(m => m
                        .Query(searchPhrase)
                    ))

                );

            //result.

            return result;
        }
    }
}
