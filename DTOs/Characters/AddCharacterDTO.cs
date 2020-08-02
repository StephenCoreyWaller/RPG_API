using WebAPI_RPG.Models;

namespace WebAPI_RPG.DTOs.Characters
{
    public class AddCharacterDTO
    {
        public string Name { get; set; } 
        public int Health { get; set; } 
        public int Magic { get; set; } 
        public int Strength { get; set; } 
        public int Dexterity { get; set; } 
        public int Spirit { get; set; }  
        public RPG_Class Class { get; set; } 
    }
}