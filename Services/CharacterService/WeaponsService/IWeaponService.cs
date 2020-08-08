using System.Threading.Tasks;
using WebAPI_RPG.DTOs.Characters;
using WebAPI_RPG.DTOs.Weapon_;
using WebAPI_RPG.Models;

namespace WebAPI_RPG.Services.CharacterService.WeaponsService
{
    public interface IWeaponService
    {
        Task<ServiceWrapper<GetCharacterDTO>> AddWeapon(AddWeaponDTO weapon);
    }
}