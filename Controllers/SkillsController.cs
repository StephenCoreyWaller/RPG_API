using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_RPG.DTOs.Characters;
using WebAPI_RPG.DTOs.Skill;
using WebAPI_RPG.Models;
using WebAPI_RPG.Services.Skills;

namespace WebAPI_RPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        [HttpPost]
        public async Task<ActionResult> AddSkills(AddSkillDTO addSkillDTO){

            ServiceWrapper<GetCharacterDTO> response = await _skillService.AddSkill(addSkillDTO); 

            // if(response.Data == null){
                
            //     return NotFound(response); 
            // }
            return Ok(response); 
        }
    }
}