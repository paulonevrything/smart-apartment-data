using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Domain.Model
{
    public class ManagementSchema
    {
        public ManagementModel Mgmt { get; set; }
    }

    public class ManagementModel
    {
        public int MgmtID { get; set; }
        
        [Text(Analyzer = "smart-analyzer", Name = nameof(Name))]
        public string Name { get; set; }
        
        [Text(Analyzer = "smart-analyzer", Name = nameof(Market))]
        public string Market { get; set; }
        
        [Text(Analyzer = "smart-analyzer", Name = nameof(State))]
        public string State { get; set; }
    }
}
