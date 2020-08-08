using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_RPG.DTOs.Characters;
using WebAPI_RPG.DTOs.Weapon_;
using WebAPI_RPG.Models;
using WebAPI_RPG.Services.CharacterService.WeaponsService;

namespace WebAPI_RPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }
        [HttpPost]
        public async Task<ActionResult> AddWeapon(AddWeaponDTO weapon){

            ServiceWrapper<GetCharacterDTO> response = await _weaponService.AddWeapon(weapon); 

            if(response.Data == null){

                return NotFound(response); 
            }
            return Ok(response);
        }
    }
}