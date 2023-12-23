using AutoMapper;
using FInanceApp.Dtos.Balance;
using FInanceApp.Dtos.Country;
using FInanceApp.Dtos.FinGoal;
using FInanceApp.Dtos.User;
using FInanceApp.Interfaces;
using FInanceApp.Models;
using FInanceApp.Repositories;

namespace FInanceApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //balance
            CreateMap<GetBalanceDto, Balance>();
            CreateMap<GetBalanceDto, Balance>().ReverseMap();

            CreateMap<UpdateBalanceDto, Balance>();
            CreateMap<UpdateBalanceDto, Balance>().ReverseMap();


            //country
            CreateMap<AddCountryDto, Country>();
            CreateMap<AddCountryDto, Country>().ReverseMap();

            CreateMap<GetCountryDto, Country>();
            CreateMap<GetCountryDto, Country>().ReverseMap();

            CreateMap<UpdateCountryDto, Country>();
            CreateMap<UpdateCountryDto, Country>().ReverseMap();


            //user
            CreateMap<RegisterUserDto, User>();
            CreateMap<RegisterUserDto, User>().ReverseMap();

            CreateMap<GetUserDto, User>();
            CreateMap<GetUserDto, User>().ReverseMap();

            CreateMap<LoginUserDto, User>();
            CreateMap<LoginUserDto, User>().ReverseMap();

            CreateMap<UpdateUserDto, User>();
            CreateMap<UpdateUserDto, User>().ReverseMap();


            //fingoal
            CreateMap<AddFinGoalDto, FinGoal>();
            CreateMap<AddFinGoalDto, FinGoal>().ReverseMap();

            CreateMap<GetFinGoalDto, FinGoal>();
            CreateMap<GetFinGoalDto, FinGoal>().ReverseMap();

            CreateMap<UpdateFinGoalDto, FinGoal>();
            CreateMap<UpdateFinGoalDto, FinGoal>().ReverseMap();
        }
    }
}
