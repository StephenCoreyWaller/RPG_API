using System;
using AutoMapper;
using WebAPI_RPG.DTOs.User;
using WebAPI_RPG.DTOs.Characters;
using WebAPI_RPG.Models;

namespace WebAPI_RPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>(); 
            CreateMap<UpdateCharacterDTO, Character>(); 
        }
    }
}