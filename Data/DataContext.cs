using Microsoft.EntityFrameworkCore;
using WebAPI_RPG.Models;

namespace WebAPI_RPG.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapon { get; set; }
        public DbSet<Skill> Abilities { get; set; }
        public DbSet<CharacterSkill> CharactersSkills { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder){
        
            modelBuilder.Entity<CharacterSkill>()
            .HasKey(cs => new {cs.CharacterId, cs.SkillId});
        }
    
    }
}