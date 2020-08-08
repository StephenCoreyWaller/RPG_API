using System.Security.Claims;
using System;
using System.Linq;
using System.Collections.Generic;
using WebAPI_RPG.Models;
using System.Threading.Tasks;
using WebAPI_RPG.DTOs.Characters;
using AutoMapper;
using WebAPI_RPG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace WebAPI_RPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        int GetUserId() => int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)); 
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceWrapper<List<GetCharacterDTO>>> CreateCharacterService(AddCharacterDTO character)
        {
            ServiceWrapper<List<GetCharacterDTO>> _wrapper = new ServiceWrapper<List<GetCharacterDTO>>();
            Character newChar = _mapper.Map<Character>(character);
            newChar.User = await _context.Users.FirstOrDefaultAsync(u => u.id == GetUserId()); 
            await _context.Characters.AddAsync(newChar);
            await _context.SaveChangesAsync();
            List<Character> characters = await _context.Characters.Where(c => c.User.id == GetUserId()).ToListAsync();
            _wrapper.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return _wrapper;
        }
        public async Task<ServiceWrapper<List<GetCharacterDTO>>> GetAllCharactersService()
        {
            ServiceWrapper<List<GetCharacterDTO>> _wrapper = new ServiceWrapper<List<GetCharacterDTO>>();
            List<Character> characters = await _context.Characters.Where(c => c.User.id == GetUserId()).ToListAsync();
            _wrapper.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return _wrapper;
        }
        public async Task<ServiceWrapper<GetCharacterDTO>> GetCharacterByNameService(int id)
        {
            ServiceWrapper<GetCharacterDTO> _wrapper = new ServiceWrapper<GetCharacterDTO>();
            Character character = await _context.Characters.FirstAsync(c => c.Id == id);
            _wrapper.Data = _mapper.Map<GetCharacterDTO>(character);
            return _wrapper;
        }

        public async Task<ServiceWrapper<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO character)
        {
            ServiceWrapper<GetCharacterDTO> _wrapper = new ServiceWrapper<GetCharacterDTO>();
            try
            {
                Character updatedChar = await _context.Characters.FirstAsync(c => c.Id == character.Id);
                updatedChar.Name = character.Name;
                updatedChar.Health = character.Health;
                updatedChar.Magic = character.Magic;
                updatedChar.Spirit = character.Dexterity;
                updatedChar.Strength = character.Strength;
                updatedChar.Class = character.Class;
                _context.Characters.Update(updatedChar);
                await _context.SaveChangesAsync();
                _wrapper.Data = _mapper.Map<GetCharacterDTO>(await _context.Characters.FirstAsync(c => c.Id == updatedChar.Id));
            }
            catch (Exception ex)
            {

                _wrapper.DidSend = false;
                _wrapper.Message = ex.Message;
            }
            return _wrapper;
        }
        public async Task<ServiceWrapper<List<GetCharacterDTO>>> DeleteCharacterService(int id)
        {
            ServiceWrapper<List<GetCharacterDTO>> _wrapper = new ServiceWrapper<List<GetCharacterDTO>>();

            try
            {
                Character character = await _context.Characters.FirstAsync(c => c.Id == id);
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                List<Character> characters = await _context.Characters.ToListAsync();
                _wrapper.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            }
            catch (Exception ex)
            {

                _wrapper.DidSend = false;
                _wrapper.Message = ex.Message;
            }
            return _wrapper;
        }
    }
}