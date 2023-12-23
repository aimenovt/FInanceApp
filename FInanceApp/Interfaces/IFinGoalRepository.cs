using FInanceApp.Dtos.FinGoal;
using FInanceApp.Models;

namespace FInanceApp.Interfaces
{
    public interface IFinGoalRepository
    {
        public Task<ServiceResponse<List<GetFinGoalDto>>> GetFinGoals();
        public Task<ServiceResponse<GetFinGoalDto>> GetFinGoal(int fingoalId);
        public Task<ServiceResponse<List<GetFinGoalDto>>> AddFinGoal(AddFinGoalDto newFinancialGoal);
        public Task<ServiceResponse<GetFinGoalDto>> UpdateFinGoal(UpdateFinGoalDto updatedFinancialGoal);
        public Task<ServiceResponse<List<GetFinGoalDto>>> DeleteFinGoal(int fingoalId);
    }
}
