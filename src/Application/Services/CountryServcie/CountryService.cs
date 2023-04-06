
using AutoMapper;
using Tawala.Application.Common.Exceptions;
using Tawala.Application.Common.Utility;
using Tawala.Application.Models;
using Tawala.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Entities.Settings;
using Tawala.Infrastructure;
using Tawala.Domain.Entities.Country;
using Tawala.Infrastructure.Repository;
using Tawala.Application.Models.CountryDTO;

namespace Takid.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepositoryBase<Country> repository;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Country> _repository;
        private readonly IMapper _mapper;


        public CountryService(IRepositoryBase<Country> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<CountryDTO> AddCountry(CountryDTO countryDTO)
        {
            if (!CheckCountryPrefixCode(countryDTO.PrefixCode))
            {
                var country = _mapper.Map<Country>(countryDTO);
                var countryresult = await _repository.Add(country);
                return _mapper.Map<CountryDTO>(countryresult);
            }
            else
            {
                throw new CantAddException("Country is Already Exists");
            }
        }

        public bool CheckCountryPrefixCode(string PrefixCode)
        {
            return _repository.Find(x => x.PrefixCode == PrefixCode).Count() > 0;
        }

        public async Task<bool> Delete(string countryId)
        {

            var country = await _repository.Delete(countryId);
            return true;

        }
        public async Task<List<CountryDTO>> GetAllCountry()
        {

            var countryList = await _repository.GetAll();
            return _mapper.Map<List<CountryDTO>>(countryList);

        }

        public Task<Country> GetCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public async Task<CountryDTO> GetCountryById(string Id)
        {
            var country = await _repository.Get(Id);
            return _mapper.Map<CountryDTO>(country);

        }
        public async Task<CountryDTO> Update(CountryDTO countryDTO)
        {
            //var result = _repository.Find(x => x.Id != countryDTO.Id && x.PrefixCode == countryDTO.PrefixCode).ToList().Count > 0;
            //if ( result)
            //{
                var country = await _repository.Update(_mapper.Map<Country>(countryDTO));
                return _mapper.Map<CountryDTO>(country);
            //}
            //else
            //{
            //    throw new CantUpdateException("Country With Same Name Is Exists");
            //}
        }
        public bool UpdateCheck(CountryDTO countryDTO)
        {
            var result = _repository.Find(x => x.Id != countryDTO.Id && x.PrefixCode == countryDTO.PrefixCode).ToList().Count > 0;
            return !result; 
        }
    }
}
