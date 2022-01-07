using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartApartmentData.Domain.Data
{
    public class DataReader<T> where T : class
    {
        private readonly string _filePath;
        public DataReader(string filePath)
        {
            _filePath = filePath;
        }

        public List<T> GetData()
        {

            var jsonString = string.Empty;

            using (var str = new StreamReader(_filePath))
            {
                jsonString = str.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<T>>(jsonString);

        }
    }
}
