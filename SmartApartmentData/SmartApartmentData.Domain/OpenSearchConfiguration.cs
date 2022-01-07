using Microsoft.Extensions.Configuration;
using Nest;
using SmartApartmentData.Domain.Model;
using System;
using System.Text;

namespace SmartApartmentData.Domain
{
    public class OpenSearchConfiguration
	{

		private static readonly ConnectionSettings _connectionSettings;

		public static ElasticClient GetClient() => new ElasticClient(_connectionSettings);

        private static MySettings _settings;

        static OpenSearchConfiguration()
        {

            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            var settings = configuration.GetSection("MySettings").Get<MySettings>();

            _settings = settings;

            _connectionSettings = new ConnectionSettings(new Uri(_settings.OpenSearch.DomainUrl))
				.BasicAuthentication(_settings.OpenSearch.Username, _settings.OpenSearch.Password)
				.DefaultIndex(Constants.PropertyIndex)
				.DefaultMappingFor<PropertyModel>(i => i
					.IndexName(Constants.PropertyIndex).IdProperty(f => f.PropertyID))
				.DefaultMappingFor<ManagementModel>(i => i
					.IndexName(Constants.PropertyIndex).IdProperty(f => f.MgmtID))
				.DefaultFieldNameInferrer(f => f)
                .OnRequestCompleted(callDetails =>
                {
                    if (callDetails.RequestBodyInBytes != null)
                    {
                        Console.WriteLine(
                            $"{callDetails.HttpMethod} {callDetails.Uri} \n" +
                            $"{Encoding.UTF8.GetString(callDetails.RequestBodyInBytes)}");
                    }
                    else
                    {
                        Console.WriteLine($"{callDetails.HttpMethod} {callDetails.Uri}");
                    }

                    Console.WriteLine();

                    if (callDetails.ResponseBodyInBytes != null)
                    {
                        Console.WriteLine($"Status: {callDetails.HttpStatusCode}\n" +
                                 $"{Encoding.UTF8.GetString(callDetails.ResponseBodyInBytes)}\n" +
                                 $"{new string('-', 30)}\n");
                    }
                    else
                    {
                        Console.WriteLine($"Status: {callDetails.HttpStatusCode}\n" +
                                 $"{new string('-', 30)}\n");
                    }
                });
		}

		public static string PropertyPath => _settings.PropertyDataFilePath;
		public static string ManagementPath => _settings.ManagementDataFilePath;
	}
}
