using System;
using AutoMapper;
using WebAPI_RPG.DTOs.User;
using WebAPI_RPG.DTOs.Characters;
using WebAPI_RPG.Models;
using WebAPI_RPG.DTOs.Weapon_;

namespace WebAPI_RPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>(); 
            CreateMap<UpdateCharacterDTO, Character>(); 
            CreateMap<Weapon, GetWeaponDTO>(); 
        }
    }
}