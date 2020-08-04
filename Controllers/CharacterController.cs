using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI_RPG.Models;
using WebAPI_RPG.Services.CharacterService;
using System.Threading.Tasks;
using WebAPI_RPG.DTOs.Characters;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI_RPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAllCharacters")]
        public async Task<ActionResult> GetCharacters()
        {
            return Ok(await _characterService.GetAllCharactersService());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetChar(int id)
        {
            return Ok(await _characterService.GetCharacterByNameService(id));
        }
        [HttpPost]
        public async Task<ActionResult> CreateCharacter(AddCharacterDTO character)
        {
            return Ok(await _characterService.CreateCharacterService(character));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(UpdateCharacterDTO character, int id){
            
            character.Id = id; 
            ServiceWrapper<GetCharacterDTO> response = await _characterService.UpdateCharacter(character); 

            if(response.Data == null){

                return NotFound(response); 
            }
            return Ok(response); 
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id){
            
            ServiceWrapper<List<GetCharacterDTO>> response = await _characterService.DeleteCharacterService(id);

            if(response.Message == null){

                return Ok(response.Data); 
            }
            return NotFound(response);  
        }
    }
}