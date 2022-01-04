using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApartmentData.Core.Services.Interfaces
{
    public interface ISearchService
    {
        Task<string> SearchAsync(string searchPhrase, string market, int limit);
    }
}
