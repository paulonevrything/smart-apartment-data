using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApartmentData.Persistence.Repository.Interfaces
{
    public interface IOpenSearchRepository
    {
        Task<string> SearchAsync();
    }
}
