using FInanceApp.Data;
using FInanceApp.Interfaces;
using FInanceApp.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using FInanceApp.Dtos.Balance;
using AutoMapper;
using Microsoft.VisualBasic;

namespace FInanceApp.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BalanceRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> BalanceExists(int balanceId)
        {
            return await _context.Balances.AnyAsync(b => b.Id == balanceId);
        }

        public async Task<ServiceResponse<List<GetBalanceDto>>> DeleteBalance(int id)
        {
            var balanceToDelete = _context.Balances.Where(b => b.Id == id).FirstOrDefault();

            _context.Balances.Remove(balanceToDelete);
            _context.SaveChanges();

            var response = new ServiceResponse<List<GetBalanceDto>>();
            response.Data = _mapper.Map<List<GetBalanceDto>>(_context.Balances.OrderBy(b => b.Id).ToList());

            return response;
        }

        public async Task<ServiceResponse<GetBalanceDto>> GetBalance(int balanceId)
        {
            var balance = await _context.Balances.Where(b => b.Id == balanceId).FirstOrDefaultAsync();

            var response = new ServiceResponse<GetBalanceDto>();
            response.Data = _mapper.Map<GetBalanceDto>(balance);

            return response;
        }

        public async Task<ServiceResponse<List<GetBalanceDto>>> GetBalances()
        {
            var balances = await _context.Balances.OrderBy(b => b.Id).ToListAsync();

            var response = new ServiceResponse<List<GetBalanceDto>>();
            response.Data = _mapper.Map<List<GetBalanceDto>>(balances);

            return response;
        }

        public async Task<ServiceResponse<decimal>> GetUSDvalueOfFunds(int balanceId)
        {
            var fundsKZT = _context.Balances.Where(b => b.Id == balanceId).FirstOrDefault().FundsKZT;

            string apiUrl = "https://v6.exchangerate-api.com/v6/432953f010e2b61fd0a8fac0/pair/USD/KZT";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        // Parse the JSON response
                        JsonDocument jsonDocument = JsonDocument.Parse(responseData);

                        // Access the specific value using the key
                        JsonElement conversionRateElement = jsonDocument.RootElement.GetProperty("conversion_rate");

                        // Extract the value as a string
                        var conversionRate = conversionRateElement.GetDecimal(); // Assuming conversion_rate is a decimal

                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = fundsKZT / conversionRate;

                        return serviceResponse;
                    }
                    else
                    {
                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = 0;

                        return serviceResponse;
                    }
                }
                catch (Exception ex)
                {
                    var serviceResponse = new ServiceResponse<decimal>();
                    serviceResponse.Data = 0;

                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<decimal>> GetEURvalueOfFunds(int balanceId)
        {
            var fundsKZT = _context.Balances.Where(b => b.Id == balanceId).FirstOrDefault().FundsKZT;

            string apiUrl = "https://v6.exchangerate-api.com/v6/432953f010e2b61fd0a8fac0/pair/EUR/KZT";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        JsonDocument jsonDocument = JsonDocument.Parse(responseData);

                        JsonElement conversionRateElement = jsonDocument.RootElement.GetProperty("conversion_rate");

                        var conversionRate = conversionRateElement.GetDecimal();

                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = fundsKZT / conversionRate;

                        return serviceResponse;
                    }

                    else
                    {
                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = 0;

                        return serviceResponse;
                    }
                }

                catch (Exception)
                {

                    var serviceResponse = new ServiceResponse<decimal>();
                    serviceResponse.Data = 0;

                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<decimal>> GetCADvalueOfFunds(int balanceId)
        {
            var fundsKZT = _context.Balances.Where(b => b.Id == balanceId).FirstOrDefault().FundsKZT;

            string apiUrl = "https://v6.exchangerate-api.com/v6/432953f010e2b61fd0a8fac0/pair/CAD/KZT";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        JsonDocument jsonDocument = JsonDocument.Parse(responseData);

                        JsonElement conversionRateElement = jsonDocument.RootElement.GetProperty("conversion_rate");

                        var conversion = conversionRateElement.GetDecimal();

                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = fundsKZT / conversion;

                        return serviceResponse;
                    }

                    else
                    {
                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = 0;

                        return serviceResponse;
                    }
                }

                catch (Exception)
                {
                    var serviceResponse = new ServiceResponse<decimal>();
                    serviceResponse.Data = 0;

                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<decimal>> GetRUBvalueOfFunds(int balanceId)
        {
            var fundsKZT = _context.Balances.Where(b => b.Id == balanceId).FirstOrDefault().FundsKZT;

            string apiUrl = "https://v6.exchangerate-api.com/v6/432953f010e2b61fd0a8fac0/pair/RUB/KZT";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string contentData = await response.Content.ReadAsStringAsync();

                        JsonDocument jsonDocument = JsonDocument.Parse(contentData);

                        JsonElement conversionRateElement = jsonDocument.RootElement.GetProperty("conversion_rate");

                        var conversion = conversionRateElement.GetDecimal();

                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = fundsKZT / conversion;

                        return serviceResponse;
                    }

                    else
                    {
                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = 0;

                        return serviceResponse;
                    }
                }
                catch (Exception)
                {

                    var serviceResponse = new ServiceResponse<decimal>();
                    serviceResponse.Data = 0;

                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<decimal>> GetLongTermGovernmentBondInterestRate()
        {
            string apiUrl = "https://api.stlouisfed.org/fred/series/observations?series_id=IRLTLT01USM156N&api_key=99d0cfa208237a4b91d59079a0402e29&file_type=json&limit=1&sort_order=desc";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        JsonDocument jsonDocument = JsonDocument.Parse(responseData);

                        JsonElement latestObservationValueElement = jsonDocument.RootElement.GetProperty("observations")[0].GetProperty("value");

                        var latestObservationValue = decimal.Parse(latestObservationValueElement.GetString());

                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = latestObservationValue;

                        return serviceResponse;
                    }

                    else
                    {
                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = 0;

                        return serviceResponse;
                    }
                }

                catch (Exception)
                {
                    var serviceResponse = new ServiceResponse<decimal>();
                    serviceResponse.Data = 0;

                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<decimal>> GetBitcoinPriceInUSD()
        {
            string apiUrl = "https://coinlib.io/api/v1/coinlist?key=be569df95e56706a&pref=USD&page=1&order=volume_desc";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        JsonDocument jsonDocument = JsonDocument.Parse(responseData);

                        JsonElement BTCpriceElement = jsonDocument.RootElement.GetProperty("coins")[1].GetProperty("price");

                        var BTCprice = decimal.Parse(BTCpriceElement.ToString());

                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = BTCprice;

                        return serviceResponse;
                    }

                    else
                    {
                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = 0;

                        return serviceResponse;
                    }
                }

                catch (Exception)
                {
                    var serviceResponse = new ServiceResponse<decimal>();
                    serviceResponse.Data = 0;

                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<decimal>> HowManyBitcoinsCanBeBought(int balanceId)
        {
            var fundsKZT = _context.Balances.Where(b => b.Id == balanceId).FirstOrDefault().FundsKZT;

            string apiBTCpriceUrl = "https://coinlib.io/api/v1/coinlist?key=be569df95e56706a&pref=USD&page=1&order=volume_desc";
            string apiUSDurl = "https://v6.exchangerate-api.com/v6/432953f010e2b61fd0a8fac0/pair/USD/KZT";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiBTCpriceUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        JsonDocument jsonDocument = JsonDocument.Parse(responseData);

                        JsonElement BTCpriceElement = jsonDocument.RootElement.GetProperty("coins")[1].GetProperty("price");

                        var BTCprice = decimal.Parse(BTCpriceElement.ToString());

                        try
                        {
                            HttpResponseMessage responseUSD = await client.GetAsync(apiUSDurl);

                            if (responseUSD.IsSuccessStatusCode)
                            {
                                string responseUSDdata = await responseUSD.Content.ReadAsStringAsync();

                                JsonDocument jsonDocumentUSD = JsonDocument.Parse(responseUSDdata);

                                JsonElement conversionRateElement = jsonDocumentUSD.RootElement.GetProperty("conversion_rate");

                                var conversion = conversionRateElement.GetDecimal();

                                var serviceResponse = new ServiceResponse<decimal>();
                                serviceResponse.Data = fundsKZT / (BTCprice * conversion);

                                return serviceResponse;
                            }

                            else
                            {
                                var serviceResponse = new ServiceResponse<decimal>();
                                serviceResponse.Data = 0;

                                return serviceResponse;
                            }
                        }

                        catch (Exception)
                        {
                            var serviceResponse = new ServiceResponse<decimal>();
                            serviceResponse.Data = 0;

                            return serviceResponse;
                        }
                    }

                    else
                    {
                        var serviceResponse = new ServiceResponse<decimal>();
                        serviceResponse.Data = 0;

                        return serviceResponse;
                    }
                }

                catch (Exception)
                {
                    var serviceResponse = new ServiceResponse<decimal>();
                    serviceResponse.Data = 0;

                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<GetBalanceDto>> UpdateBalance(UpdateBalanceDto updateBalance)
        {
            var balance = _context.Balances.Where(b => b.Id == updateBalance.Id).FirstOrDefault();
            _mapper.Map(updateBalance, balance);

            _context.Balances.Update(balance);
            _context.SaveChanges();

            var response = new ServiceResponse<GetBalanceDto>();    
            response.Data = _mapper.Map<GetBalanceDto>(balance);

            return response;
        }
    }
}
