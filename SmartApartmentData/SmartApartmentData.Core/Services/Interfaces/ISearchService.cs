using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApartmentData.Core.Services.Interfaces
{
    public interface ISearchService
    {
        string Search(string searchPhrase, string[] markets, int limit);
    }
}
