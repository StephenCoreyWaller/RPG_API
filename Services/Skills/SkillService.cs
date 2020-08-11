using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAPI_RPG.Data;
using WebAPI_RPG.DTOs.Characters;
using WebAPI_RPG.DTOs.Skill;
using WebAPI_RPG.Models;

namespace WebAPI_RPG.Services.Skills
{
    public class SkillService : ISkillService
    {
        //Add in constructor - Add dbcontext, _mapper, Httpaccessor
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        int GetUserId() => int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)); 
        public SkillService(DataContext context, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _context = context;
        }
        public async Task<ServiceWrapper<GetCharacterDTO>> AddSkill(AddSkillDTO skill)
        {
            ServiceWrapper<GetCharacterDTO> response = new ServiceWrapper<GetCharacterDTO>(); 
            
            try{

                Character character = await _context.Characters.Include(w => w.Weapon)
                    .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                    .FirstOrDefaultAsync(c => c.Id == skill.CharacterId && c.User.id == GetUserId());
                    
                if(character == null){

                    response.DidSend = false; 
                    response.Message = "Character not found.";
                    return response; 
                }
                
                Skill skillToAdd = await _context.Abilities.FirstOrDefaultAsync(s => s.Id == skill.SkillId); 

                CharacterSkill CharSkill = new CharacterSkill{
                    Skill = skillToAdd,
                    Character = character, 
                };
        
                await _context.CharactersSkills.AddAsync(CharSkill);
                await _context.SaveChangesAsync(); 

                response.Data = _mapper.Map<GetCharacterDTO>(character);

            }catch(Exception ex){

                response.DidSend = false; 
                response.Message = ex.Message; 
            }
            return response; 
        }
    }
}