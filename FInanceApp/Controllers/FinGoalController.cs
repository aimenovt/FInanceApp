using FInanceApp.Dtos.FinGoal;
using FInanceApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace FInanceApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FinGoalController : ControllerBase
    {
        private readonly IFinGoalRepository _finGoalRepository;

        public FinGoalController(IFinGoalRepository finGoalRepository)
        {
            _finGoalRepository = finGoalRepository;
        }

        [HttpPost("add-fingoal")]
        public async Task<IActionResult> AddFinGoal(AddFinGoalDto newFinancialGoal)
        {
            return Ok(await _finGoalRepository.AddFinGoal(newFinancialGoal));
        }

        [HttpGet("get-fingoal")]
        public async Task<IActionResult> GetFinGoal(int fingoalId)
        {
            return Ok(await _finGoalRepository.GetFinGoal(fingoalId));
        }

        [HttpGet("get-fingoals")]
        public async Task<IActionResult> GetFinGoals()
        {
            return Ok(await _finGoalRepository.GetFinGoals());  
        }

        [HttpPut("update-fingoal")]
        public async Task<IActionResult> UpdateFinGoal(UpdateFinGoalDto updatedFinancialGoal)
        {
            return Ok(await _finGoalRepository.UpdateFinGoal(updatedFinancialGoal));
        }

        [HttpDelete("delete-fingoal")]
        public async Task<IActionResult> DeleteFinGoal(int fingoalId)
        {
            return Ok(await _finGoalRepository.DeleteFinGoal(fingoalId));
        }
    }
}
