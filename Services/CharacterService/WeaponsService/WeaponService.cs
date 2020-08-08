using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAPI_RPG.Data;
using WebAPI_RPG.DTOs.Characters;
using WebAPI_RPG.DTOs.Weapon_;
using WebAPI_RPG.Models;

namespace WebAPI_RPG.Services.CharacterService.WeaponsService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        int GetUserId() => int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)); 
        public WeaponService(DataContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceWrapper<GetCharacterDTO>> AddWeapon(AddWeaponDTO weapon)
        {
            ServiceWrapper<GetCharacterDTO> wrapper = new ServiceWrapper<GetCharacterDTO>(); 

            try{
                Character character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == weapon.CharacterId && c.User.id == GetUserId());
                if(character == null){

                    wrapper.DidSend = false; 
                    wrapper.Message = "Character not found."; 
                    return wrapper; 
                }
                Weapon newWeapon = new Weapon{
                    Name = weapon.Name, 
                    Damage = weapon.Damage, 
                    Character = character
                };
                await _context.Weapon.AddAsync(newWeapon); 
                await _context.SaveChangesAsync();  
                wrapper.Data = _mapper.Map<GetCharacterDTO>(character); 

            }catch(Exception ex){

                wrapper.DidSend = false; 
                wrapper.Message = ex.Message; 
            }
            return wrapper; 
        }
    }
}