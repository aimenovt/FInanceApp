using AutoMapper;
using FInanceApp.Dtos.Country;
using FInanceApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FInanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IUserRepository userRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("get-all-available-countries")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _countryRepository.GetCountries());
        }

        [HttpGet("get-country/{countryId}")]
        public async Task<IActionResult> GetCountry(int countryId)
        {
            return Ok(await _countryRepository.GetCountry(countryId));
        }

        [HttpGet("get-country-of-user")]
        public async Task<IActionResult> GetCountryOfUser()
        {
            return Ok(await _countryRepository.GetCountryOfUser());
        }

        [HttpPost("create-country")]
        public async Task<IActionResult> CreateCountry(AddCountryDto newCountry)
        {
            return Ok(await _countryRepository.CreateCountry(newCountry));
        }

        [HttpPut("update-country")]
        public async Task<IActionResult> UpdateCountry(UpdateCountryDto countryToUpdate)
        {
            return Ok(await _countryRepository.UpdateCountry(countryToUpdate));
        }

        [HttpDelete("delete-country/{countryId}")]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            return Ok(await _countryRepository.DeleteCountry(countryId));
        }

        [HttpPut("change-country-of-user/{newCountryId}")]
        public async Task<IActionResult> ChangeCountryOfUser(int newCountryId)
        {
            return Ok(await _countryRepository.ChangeCountryOfUser(newCountryId));
        }
    }
}
