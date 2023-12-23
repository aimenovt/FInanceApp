using AutoMapper;
using FInanceApp.Data;
using FInanceApp.Dtos.Country;
using FInanceApp.Interfaces;
using FInanceApp.Models;
using RabbitMQ.Client;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FInanceApp.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CountryRepository(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<string>> ChangeCountryOfUser(int newCountryId)
        {
            _context.Users.Where(u => u.Id == GetUserId()).FirstOrDefault().Country = _context.Countries.Where(c => c.Id == newCountryId).FirstOrDefault();
            await _context.SaveChangesAsync();

            var response = new ServiceResponse<string>();
            response.Data = "Successfully changed country of user";

            return response;
        }

        public async Task<bool> CountryExists(int countryId)
        {
            return _context.Countries.Any(c => c.Id == countryId);
        }

        public async Task<ServiceResponse<List<GetCountryDto>>> CreateCountry(AddCountryDto newCountry)
        {
            //produce,send message
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                UserName = "user",
                Password = "password"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "financeapp_createcountry_queue",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    channel.ExchangeDeclare(exchange: "financeapp_exchange",
                                            type: ExchangeType.Direct,
                                            durable: true,
                                            autoDelete: false,
                                            arguments: null);

                    channel.QueueBind(queue: "financeapp_createcountry_queue",
                                      exchange: "financeapp_exchange",
                                      routingKey: "financeapp_createcountry_key",
                                      arguments: null);

                    var jsonString = JsonSerializer.Serialize(newCountry);
                    var body = Encoding.UTF8.GetBytes(jsonString).ToArray();

                    channel.BasicPublish(exchange: "financeapp_exchange",
                                         routingKey: "financeapp_createcountry_key",
                                         basicProperties: null,
                                         body: body);
                }
            }

            //main logic (adding country to database)
            _context.Countries.Add(_mapper.Map<Country>(newCountry));
            _context.SaveChanges();

            var response = new ServiceResponse<List<GetCountryDto>>();
            response.Data = _mapper.Map<List<GetCountryDto>>(_context.Countries.OrderBy(c => c.Id).ToList());

            return response;
        }

        public async Task<ServiceResponse<List<GetCountryDto>>> DeleteCountry(int id)
        {
            var countryToDelete = _context.Countries.Where(c => c.Id == id).FirstOrDefault();
            _context.Countries.Remove(countryToDelete);
            _context.SaveChanges();

            var response = new ServiceResponse<List<GetCountryDto>>();
            response.Data = _mapper.Map<List<GetCountryDto>>(_context.Countries.OrderBy(c => c.Id).ToList());

            return response;
        }

        public async Task<ServiceResponse<List<GetCountryDto>>> GetCountries()
        {
            var countries = _context.Countries.OrderBy(c => c.Id).ToList();

            var response = new ServiceResponse<List<GetCountryDto>>();
            response.Data = _mapper.Map<List<GetCountryDto>>(countries);

            return response;
        }

        public async Task<ServiceResponse<GetCountryDto>> GetCountry(int countryId)
        {
            var country = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault();

            var response = new ServiceResponse<GetCountryDto>();
            response.Data = _mapper.Map<GetCountryDto>(country);

            return response;
        }

        public async Task<ServiceResponse<GetCountryDto>> GetCountryOfUser() 
        {
            var countryOfUser = _context.Users.Where(u => u.Id == GetUserId()).Select(u => u.Country).FirstOrDefault();

            var response = new ServiceResponse<GetCountryDto>();
            response.Data = _mapper.Map<GetCountryDto>(countryOfUser);

            return response;
        }

        public async Task<ServiceResponse<GetCountryDto>> UpdateCountry(UpdateCountryDto updatedCountry)
        {
            var country = _context.Countries.Where(c => c.Id == updatedCountry.Id).FirstOrDefault();

            _mapper.Map(updatedCountry, country);

            _context.Countries.Update(country);
            _context.SaveChanges();

            var response = new ServiceResponse<GetCountryDto>();    
            response.Data = _mapper.Map<GetCountryDto>(country);

            return response;
        }
    }
}
