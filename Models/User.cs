using System.Collections.Generic;

namespace WebAPI_RPG.Models
{
    public class User
    {
        public int id { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PsswordSalt { get; set; }
        public List<Character> Characters { get; set; }
    }
}