using FInanceApp.Dtos.Country;
using FInanceApp.Dtos.User;
using FInanceApp.Models;

namespace FInanceApp.Interfaces
{
    public interface ICountryRepository
    {
        Task<ServiceResponse<List<GetCountryDto>>> GetCountries();
        Task<ServiceResponse<GetCountryDto>> GetCountry(int countryId);
        Task<ServiceResponse<GetCountryDto>> GetCountryOfUser();
        Task<bool> CountryExists(int countryId);
        Task<ServiceResponse<List<GetCountryDto>>> CreateCountry(AddCountryDto newCountry);
        Task<ServiceResponse<GetCountryDto>> UpdateCountry(UpdateCountryDto updatedCountry);
        Task<ServiceResponse<List<GetCountryDto>>> DeleteCountry(int id);
        Task<ServiceResponse<string>> ChangeCountryOfUser(int newCountryId);
    }
}
