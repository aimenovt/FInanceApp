using AutoMapper;
using FInanceApp.Dtos.Balance;
using FInanceApp.Interfaces;
using FInanceApp.Models;
using FInanceApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FInanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : Controller
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalanceController(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        [HttpGet("get-balances")]
        public async Task<IActionResult> GetBalances()
        {
            return Ok(await _balanceRepository.GetBalances());
        }

        [HttpGet("get-balance/{balanceId}")]
        public async Task<IActionResult> GetBalance(int balanceId)
        {
            return Ok(await _balanceRepository.GetBalance(balanceId));
        }

        [HttpPut("update-balance")]
        public async Task<IActionResult> UpdateBalance(UpdateBalanceDto balanceToUpdate)
        {
            return Ok(await _balanceRepository.UpdateBalance(balanceToUpdate));
        }

        [HttpGet("get-USD-value-of-funds/{balanceId}")]
        public async Task<IActionResult> GetUSDvalueOfFunds(int balanceId)
        {
            return Ok(await _balanceRepository.GetUSDvalueOfFunds(balanceId));
        }

        [HttpGet("get-EUR-value-of-funds/{balanceId}")]
        public async Task<IActionResult> GetEURvalueOfFunds(int balanceId)
        {
            return Ok(await _balanceRepository.GetEURvalueOfFunds(balanceId));
        }

        [HttpGet("get-CAD-value-of-funds/{balanceId}")]
        public async Task<IActionResult> GetCADvalueOfFunds(int balanceId)
        {
            return Ok(await _balanceRepository.GetCADvalueOfFunds(balanceId));
        }

        [HttpGet("get-RUB-value-of-funds/{balanceId}")]
        public async Task<IActionResult> GetRUBvalueOfFunds(int balanceId)
        {
            return Ok(await _balanceRepository.GetRUBvalueOfFunds(balanceId));
        }

        [HttpGet("get-longterm-government-bond-intrate")]
        public async Task<IActionResult> GetLongTermGovernmentBondInterestRate()
        {
            return Ok(await _balanceRepository.GetLongTermGovernmentBondInterestRate());
        }

        [HttpGet("get-bitcoin-price-in-USD")]
        public async Task<IActionResult> GetBitcoinPriceInUSD()
        {
            return Ok(await _balanceRepository.GetBitcoinPriceInUSD());
        }

        [HttpGet("how-many-bitcoins-can-be-bought/{balanceId}")]
        public async Task<IActionResult> HowManyBitcoinsCanBeBought(int balanceId)
        {
            return Ok(await _balanceRepository.HowManyBitcoinsCanBeBought(balanceId));
        }

        [HttpDelete("delete-balance/{balanceId}")]
        public async Task<IActionResult> DeleteBalance(int balanceId)
        {
            return Ok(await _balanceRepository.DeleteBalance(balanceId));
        }
    }
}
