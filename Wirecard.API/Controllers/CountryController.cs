using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wirecard.Core.Dtos;
using Wirecard.Core.Models;
using Wirecard.Core.Services;

namespace Wirecard.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        private readonly IGenericService<Country, CountryDto> _genericService;
        public CountryController(IGenericService<Country, CountryDto> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _genericService.GetAllAsync();
            return CommonActionResult(countries);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCountry(CountryDto countryDto)
        {
            var retVal = await _genericService.AddAsync(countryDto);
            return CommonActionResult(retVal);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry(CountryDto countryDto)
        {
            var retVal = await _genericService.UpdateAsync(countryDto, countryDto.Id);
            return CommonActionResult(retVal);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var retVal = await _genericService.RemoveAsync(id);
            return CommonActionResult(retVal);
        }
    }
}
