using Nest;
using SmartApartmentData.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Domain
{
    public class OpenSearchConfiguration
	{

		private static readonly ConnectionSettings _connectionSettings;

		public static ElasticClient GetClient() => new ElasticClient(_connectionSettings);


        static OpenSearchConfiguration()
        {
			// TODO: Place connection strings and credentials in appsettings
			_connectionSettings = new ConnectionSettings(new Uri("https://search-smartapartmentdata-u6bjb6wsnmeshqrfidqjd7lp4e.us-east-2.es.amazonaws.com/"))
				.BasicAuthentication("root", "Qwertyuiop123$")
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


		// TODO: Move these to appsettings
		public static string PropertyPath => @"C:\Users\Paul Olabisi\Desktop\property-data\properties.json";
		public static string ManagementPath => @"C:\Users\Paul Olabisi\Desktop\property-data\mgmt.json";
	}
}
