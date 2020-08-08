using WebAPI_RPG.DTOs.Weapon_;
using WebAPI_RPG.Models;

namespace WebAPI_RPG.DTOs.Characters
{
    public class GetCharacterDTO
    {
        public string Name { get; set; } 
        public int Id { get; set; }
        public int Health { get; set; } 
        public int Magic { get; set; } 
        public int Strength { get; set; } 
        public int Dexterity { get; set; } 
        public int Spirit { get; set; } 
        public RPG_Class Class { get; set; } 
        public GetWeaponDTO Weapon { get; set; }
    }
}