using Nest;
using SmartApartmentData.Domain;
using SmartApartmentData.Domain.Data;
using SmartApartmentData.Domain.Model;
using System;

namespace SmartAprtmentData.Indexer
{
    class Program
    {

        public static string PropertyIndex => Constants.PropertyIndex;

        public static string ManagementIndex => Constants.ManagementIndex;

        // private static ElasticClient PropertyClient { get; set; }
        private static DataReader<PropertySchema> PropertyDataReader { get; set; }

        // private static ElasticClient ManagementClient { get; set; }
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
                        .Map<PropertyModel>(map => map
                            .AutoMap()
                            .Properties(p => p
                                .Text(c => c
                                    .Name(n => n.Name)
                                    .Analyzer("standard"))
                                .Text(c => c
                                    .Name(n => n.FormerName)
                                    .Analyzer("standard"))
                            )));

                }

                if (index == ManagementIndex)
                {

                    client.Indices.Create(index, c => c
                        .Map<ManagementModel>(map => map
                            .AutoMap()
                            .Properties(p => p
                                .Text(c => c
                                    .Name(n => n.Name)
                                    .Analyzer("standard"))
                                .Text(c => c
                                    .Name(n => n.Market)
                                    .Analyzer("standard"))
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


    }
}
