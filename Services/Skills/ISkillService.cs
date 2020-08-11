using System.Threading.Tasks;
using WebAPI_RPG.DTOs.Characters;
using WebAPI_RPG.DTOs.Skill;
using WebAPI_RPG.Models;

namespace WebAPI_RPG.Services.Skills
{
    public interface ISkillService
    {
        Task<ServiceWrapper<GetCharacterDTO>> AddSkill(AddSkillDTO skill); 
    }
}