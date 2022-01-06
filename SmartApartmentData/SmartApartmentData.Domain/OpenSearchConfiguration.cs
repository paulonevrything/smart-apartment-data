using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Domain
{
    public class OpenSearchConfiguration<T> where T : class
	{

		private readonly ConnectionSettings _connectionSettings;

		public ElasticClient GetClient() => new ElasticClient(_connectionSettings);


        public OpenSearchConfiguration(string index)
        {
			// TODO: Place connection strings and credentials in appsettings
			_connectionSettings = new ConnectionSettings(new Uri("https://search-smartapartmentdata-u6bjb6wsnmeshqrfidqjd7lp4e.us-east-2.es.amazonaws.com/"))
				.BasicAuthentication("root", "Qwertyuiop123$")
				.DefaultIndex(index)
				.DefaultMappingFor<T>(i => i
					.IndexName(index)
				);
		}

		public static string PropertyPath => @"C:\Users\Paul Olabisi\Desktop\property-data\properties.json";
		public static string ManagementPath => @"C:\management-data";
	}
}
