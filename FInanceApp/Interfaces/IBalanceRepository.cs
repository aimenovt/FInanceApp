using FInanceApp.Dtos.Balance;
using FInanceApp.Dtos.User;
using FInanceApp.Models;

namespace FInanceApp.Interfaces
{
    public interface IBalanceRepository
    {
        Task<ServiceResponse<List<GetBalanceDto>>> GetBalances();
        Task<ServiceResponse<GetBalanceDto>> GetBalance(int balanceId);
        Task<bool> BalanceExists(int balanceId);
        Task<ServiceResponse<GetBalanceDto>> UpdateBalance(UpdateBalanceDto updateBalance);
        Task<ServiceResponse<List<GetBalanceDto>>> DeleteBalance(int id);
        Task<ServiceResponse<decimal>> GetUSDvalueOfFunds(int balanceId);
        Task<ServiceResponse<decimal>> GetEURvalueOfFunds(int balanceId);
        Task<ServiceResponse<decimal>> GetCADvalueOfFunds(int balanceId);
        Task<ServiceResponse<decimal>> GetRUBvalueOfFunds(int balanceId);
        Task<ServiceResponse<decimal>> GetLongTermGovernmentBondInterestRate();
        Task<ServiceResponse<decimal>> GetBitcoinPriceInUSD();
        Task<ServiceResponse<decimal>> HowManyBitcoinsCanBeBought(int balanceId);
    }
}
