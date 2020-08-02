using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_RPG.DTOs.Characters;
using WebAPI_RPG.Models;

namespace WebAPI_RPG.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceWrapper<List<GetCharacterDTO>>> GetAllCharactersService(); 
        Task<ServiceWrapper<GetCharacterDTO>> GetCharacterByNameService(int id); 
        Task<ServiceWrapper<List<GetCharacterDTO>>> CreateCharacterService(AddCharacterDTO character); 
        Task<ServiceWrapper<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO character); 
        Task<ServiceWrapper<List<GetCharacterDTO>>> DeleteCharacterService(int id); 
    }
}