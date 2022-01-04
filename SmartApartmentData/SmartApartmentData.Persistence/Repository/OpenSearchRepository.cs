using SmartApartmentData.Persistence.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApartmentData.Persistence.Repository
{
    public class OpenSearchRepository : IOpenSearchRepository
    {
        public Task<string> SearchAsync()
        {
            throw new NotImplementedException();
        }
    }
}
