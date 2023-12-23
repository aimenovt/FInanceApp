using AutoMapper;
using FInanceApp.Data;
using FInanceApp.Dtos.FinGoal;
using FInanceApp.Interfaces;
using FInanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FInanceApp.Repositories
{
    public class FinGoalRepository : IFinGoalRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FinGoalRepository(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetFinGoalDto>>> AddFinGoal(AddFinGoalDto newFinancialGoal)
        {
            var response = new ServiceResponse<List<GetFinGoalDto>>();

            try
            {
                var user = _context.Users.Include(u => u.FinGoals).FirstOrDefault(u => u.Id == GetUserId());
                var finGoalToAdd = _mapper.Map<FinGoal>(newFinancialGoal);
                user.FinGoals.Add(finGoalToAdd);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<List<GetFinGoalDto>>(_context.FinGoals.Where(f => f.Users.Contains(user)).ToList());
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetFinGoalDto>> GetFinGoal(int fingoalId)
        {
            var response = new ServiceResponse<GetFinGoalDto>();

            try
            {
                var user = _context.Users.Include(u => u.FinGoals).FirstOrDefault(u => u.Id == GetUserId());
                var goal = user.FinGoals.FirstOrDefault(f => f.Id == fingoalId);

                if (goal == null)
                {
                    response.Success = false;
                    response.Message = "Financial foal not found";
                }

                else
                {
                    response.Data = _mapper.Map<GetFinGoalDto>(_context.FinGoals.FirstOrDefault(f => f.Id == fingoalId));
                }
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetFinGoalDto>>> GetFinGoals()
        {
            var response = new ServiceResponse<List<GetFinGoalDto>>();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == GetUserId());
                var fingoals = _context.FinGoals.Where(f => f.Users.Contains(user)).ToList();

                if (!fingoals.Any())
                {
                    response.Success = false;
                    response.Message = "Financial goals were not found";
                }

                else
                {
                    response.Data = _mapper.Map<List<GetFinGoalDto>>(fingoals);
                }
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetFinGoalDto>> UpdateFinGoal(UpdateFinGoalDto updatedFinancialGoal)
        {
            var response = new ServiceResponse<GetFinGoalDto>();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == GetUserId());

                var fingoal = _context.FinGoals.FirstOrDefault(f => f.Users.Contains(user) && f.Id == updatedFinancialGoal.Id);

                if (fingoal == null)
                {
                    response.Success = false;
                    response.Message = "Financial goal was not found";
                }

                else
                {
                    _mapper.Map(updatedFinancialGoal, fingoal); //mapped fingoal

                    _context.FinGoals.Update(fingoal);
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetFinGoalDto>(fingoal);
                }
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetFinGoalDto>>> DeleteFinGoal(int fingoalId)
        {
            var response = new ServiceResponse<List<GetFinGoalDto>>();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == GetUserId());
                var fingoal = _context.FinGoals.FirstOrDefault(f => f.Id == fingoalId && f.Users.Contains(user));

                if (fingoal == null)
                {
                    response.Success = false;
                    response.Message = "Financial goal was not found";
                }

                else
                {
                    _context.FinGoals.Remove(fingoal);
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<List<GetFinGoalDto>>(_context.FinGoals.Where(f => f.Users.Contains(user)).OrderBy(f => f.Id).ToList());
                }
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
