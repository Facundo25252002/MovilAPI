using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using MovilAPI.Models;


namespace MovilAPI.Models.Data
{
    public class MovilContext:DbContext
    {
        public MovilContext(DbContextOptions<MovilContext>options): base (options) 
        { 
        
        } 
        
        public DbSet<Productos>Productos { get; set; }  
        public DbSet<Usuarios>Usuarios { get; set; }
    }
}
