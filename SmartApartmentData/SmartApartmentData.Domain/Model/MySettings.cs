using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Domain.Model
{
    public class MySettings
    {
        public OpenSearchSettings OpenSearch { get; set; }
        public string PropertyDataFilePath { get; set; }
        public string ManagementDataFilePath { get; set; }

        public class OpenSearchSettings
        {
            public string DomainUrl { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
