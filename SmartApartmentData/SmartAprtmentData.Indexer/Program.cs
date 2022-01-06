using Nest;
using SmartApartmentData.Domain;
using SmartApartmentData.Domain.Data;
using SmartApartmentData.Domain.Model;
using System;

namespace SmartAprtmentData.Indexer
{
    class Program
    {
		private static ElasticClient PropertyClient { get; set; }
		private static DataReader<PropertySchema> PropertyDataReader { get; set; }

		static void Main(string[] args)
		{

			// Index Properties
			OpenSearchConfiguration<PropertySchema> propertyRef = new OpenSearchConfiguration<PropertySchema>(Constants.PropertyIndex);
			PropertyClient = propertyRef.GetClient();
			var propertyDirectory = OpenSearchConfiguration<PropertySchema>.PropertyPath;

			PropertyDataReader = new DataReader<PropertySchema>(propertyDirectory);

			CreateIndex<PropertySchema>(PropertyClient, Constants.PropertyIndex);
            IndexData(PropertyClient, PropertyDataReader, Constants.PropertyIndex);

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}

		private static void CreateIndex<T>(ElasticClient client, string index) where T : class
		{
			client.Indices.Delete(index);
			client.Indices.Create(index, i => i
				.Map<T>(p => p.AutoMap())  
				// TODO: Do Proper Mapping
			);
		}

		private static void IndexData<T>(ElasticClient client, DataReader<T> dataReader, string index) where T : class
        {
			Console.WriteLine($"Reading {typeof(T)} data from file ...");
			var documentData = dataReader.GetData();

			Console.Write("Indexing documents into Elasticsearch...");
			client.IndexMany<T>(documentData, index);

		}


	}
}
