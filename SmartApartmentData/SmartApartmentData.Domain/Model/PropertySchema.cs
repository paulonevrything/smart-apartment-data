using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Domain.Model
{
    public class PropertySchema
    {
        public PropertyModel Property { get; set; }
    }

    public class PropertyModel
    {
        public int PropertyID { get; set; }
        public string Name { get; set; }
        public string FormerName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Market { get; set; }
        public string State { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
    }

}
