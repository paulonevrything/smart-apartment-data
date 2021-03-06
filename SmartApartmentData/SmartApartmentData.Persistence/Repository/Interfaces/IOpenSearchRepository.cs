using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace SmartApartmentData.Persistence.Repository.Interfaces
{
    public interface IOpenSearchRepository
    {
        string Search(string searchPhrase, string[] markets, int limit);
    }
}
