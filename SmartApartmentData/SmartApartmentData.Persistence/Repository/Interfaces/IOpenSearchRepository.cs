using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace SmartApartmentData.Persistence.Repository.Interfaces
{
    public interface IOpenSearchRepository
    {
        Task<ISearchResponse<dynamic>> SearchAsync(string searchPhrase, string market, int limit);
    }
}
