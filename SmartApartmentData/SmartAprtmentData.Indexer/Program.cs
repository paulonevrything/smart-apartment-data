using Nest;
using SmartApartmentData.Domain;
using SmartApartmentData.Domain.Data;
using SmartApartmentData.Domain.Model;
using System;

namespace SmartAprtmentData.Indexer
{
    class Program
    {
        private const string SMART_ANALYZER_KEY = "smart-analyzer";

        public static string PropertyIndex => Constants.PropertyIndex;

        public static string ManagementIndex => Constants.ManagementIndex;

        private static DataReader<PropertySchema> PropertyDataReader { get; set; }

        private static DataReader<ManagementSchema> ManagementReader { get; set; }

        static void Main(string[] args)
        {

            ElasticClient elasticClient = OpenSearchConfiguration.GetClient();

            CreateIndex(elasticClient);

            PropertyDataReader = new DataReader<PropertySchema>(OpenSearchConfiguration.PropertyPath);

            ManagementReader = new DataReader<ManagementSchema>(OpenSearchConfiguration.ManagementPath);

            IndexData(elasticClient);

            elasticClient.Indices.Refresh(($"{Constants.PropertyIndex},{Constants.ManagementIndex}"));


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void CreateIndex(ElasticClient client)
        {

            foreach (var index in new[] { PropertyIndex, ManagementIndex })
            {
                if (client.Indices.Exists(index).Exists)
                    client.Indices.Delete(index);

                if (index == PropertyIndex)
                {

                    client.Indices.Create(index, c => c
                        .Settings(c => c
                        .Analysis(Analysis))
                        .Map<PropertyModel>(map => map
                            .AutoMap()
                            .Properties(p => p
                                .Text(c => c
                                    .Name(n => n.Name)
                                    .Analyzer(SMART_ANALYZER_KEY))
                                .Text(c => c
                                    .Name(n => n.FormerName)
                                    .Analyzer(SMART_ANALYZER_KEY))
                                .Text(c => c
                                    .Name(n => n.City)
                                    .Analyzer(SMART_ANALYZER_KEY))
                                .Text(c => c
                                    .Name(n => n.Market)
                                    .Analyzer(SMART_ANALYZER_KEY))
                                .Text(c => c
                                    .Name(n => n.State)
                                    .Analyzer(SMART_ANALYZER_KEY))
                                .Text(c => c
                                    .Name(n => n.StreetAddress)
                                    .Analyzer(SMART_ANALYZER_KEY))
                            )));

                }

                if (index == ManagementIndex)
                {

                    client.Indices.Create(index, c => c
                        .Settings(c => c
                        .Analysis(Analysis))
                        .Map<ManagementModel>(map => map
                            .AutoMap()
                            .Properties(p => p
                                .Text(c => c
                                    .Name(n => n.Name)
                                    .Analyzer(SMART_ANALYZER_KEY))
                                .Text(c => c
                                    .Name(n => n.Market)
                                    .Analyzer(SMART_ANALYZER_KEY))
                                .Text(c => c
                                    .Name(n => n.State)
                                    .Analyzer(SMART_ANALYZER_KEY))
                            )));

                }
            }

        }

        private static void IndexData(ElasticClient client)
        {
            Console.WriteLine("Reading Property data from file ...");
            var propertyData = PropertyDataReader.GetData();


            Console.WriteLine("Indexing Property documents into Elasticsearch...");
            client.IndexMany(propertyData, PropertyIndex);

            Console.WriteLine("***************************************");

            Console.WriteLine("Reading Management data from file ...");
            var mamnagementData = ManagementReader.GetData();


            Console.WriteLine("Indexing Management documents into Elasticsearch...");
            client.IndexMany(mamnagementData, ManagementIndex);

        }

        private static AnalysisDescriptor Analysis(AnalysisDescriptor analysis) => analysis

                        // Define Edge n-gram tokenizer for autocomplete
                        .Tokenizers(tok => tok
                            .EdgeNGram("autocomplete-search", e => e
                                .MinGram(3)
                                .MaxGram(5)
                                .TokenChars(TokenChar.Letter, TokenChar.Digit)
                            )
                        )

                        // Setup Stop Token Filter to remove stop words
                        .TokenFilters(tokenfilters => tokenfilters
                            .Stop("stop-words", w => w
                                .StopWords("_english_ ")
                            )
                        )

                        // Bring it all together in a custom analyzer
                        .Analyzers(analyzers => analyzers
                            .Custom(SMART_ANALYZER_KEY, c => c
                                .Tokenizer("autocomplete-search")
                                .Filters("stop-words")
                            )
                        );


    }
}
