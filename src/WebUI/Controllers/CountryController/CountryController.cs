 
using Takid.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks; 
using System; 
using Tawala.Application.Common.SendEmail;
using Tawala.Application.Models.CountryDTO;
using Tawala.WebUI.Controllers;

namespace Takid.WebUI.Controllers
{
    //  [Authorize]
    public class CountryController : ApiControllerBase
    {
        private readonly ICountryService _countryService; 
        public CountryController(ICountryService countryService  )
        {
            this._countryService = countryService; 
           
        }

        [HttpGet]
        [Route("GetAllCountry")]
        public async Task<ActionResult<List<CountryDTO>>> GetAllCountry()
        {
            return await _countryService.GetAllCountry();
        }


        [HttpGet]
        [Route("GetCountryById/{Id}")]
        public async Task<ActionResult<CountryDTO>> GetCountryById(string Id)
        {
            var aa = await _countryService.GetCountryById(Id);
            return aa;
        }

        // [Authorize ]
        //[CustomAuthAttribute]
        [HttpPost]
        [Route("AddCountry")]
        public async Task<ActionResult<CountryDTO>> AddCountry(CountryDTO country)
        {
            var result = await _countryService.AddCountry(country);
            return result;
        }

        // [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("DeleteCountry")]
        public async Task<ActionResult<bool>> DeleteCountry(string countryId)
        {
            return await _countryService.Delete(countryId);
        } 
  
    }
}
