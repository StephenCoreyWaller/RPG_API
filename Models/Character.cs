using System.Reflection;
namespace WebAPI_RPG.Models
{
    public class Character
    {
        public string Name { get; set; } = "Stephen"; 
        public int Id { get; set; }
        public int Health { get; set; } = 10; 
        public int Magic { get; set; } = 20; 
        public int Strength { get; set; } = 6; 
        public int Dexterity { get; set; } = 15; 
        public int Spirit { get; set; } = 20; 
        public RPG_Class Class { get; set; } = RPG_Class.Mage; 
        public User User { get; set; }

    }
}