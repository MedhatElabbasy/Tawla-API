
using Tawala.Application.Models;
using Tawala.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Models.CountryDTO;

namespace Takid.Application.Services
{
    public interface ICountryService
    {
        Task<List<CountryDTO>> GetAllCountry();
        Task<CountryDTO> GetCountryById(string Id);
        Task<CountryDTO> AddCountry(CountryDTO countryDTO);
        Task<bool> Delete(string countryId);
        bool CheckCountryPrefixCode(string PrefixCode);
        Task<CountryDTO> Update(CountryDTO countryDTO);
        bool UpdateCheck(CountryDTO countryDTO);

    }
}
