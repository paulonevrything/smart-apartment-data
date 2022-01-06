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
        public string Name { get; set; }
        public string Market { get; set; }
        public string State { get; set; }
    }
}
