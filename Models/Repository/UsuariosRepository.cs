using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovilAPI.Models.Data;
using MovilAPI.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovilAPI.Models.Repository


{
    public class UsuariosRepository : IUsuariosRepository
    {
        protected readonly MovilContext _context;
        public UsuariosRepository(MovilContext context) => _context = context;

        public IEnumerable<Usuarios> GetUsuarios()
        {
            return _context.Usuarios.ToList();
        }
        public async Task<IEnumerable<Usuarios>> GetUsuariosAsync()
        {
            // Utiliza async/await para consultar la base de datos de manera asincrónica
            
            return await _context.Usuarios.ToListAsync();
        }

        public Usuarios GetUsuariosById(int id) => _context.Usuarios.Find(id);

        public async Task<Usuarios> CreateUsuariosAsync(Usuarios usuarios)
        {
            await _context.Set<Usuarios>().AddAsync(usuarios);
            await _context.SaveChangesAsync();
            return usuarios;
        }


        public async Task<bool>UpdateUsuariosAsync(Usuarios usuarios)
        {
            _context.Entry(usuarios).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool>DeleteUsuariosAsync(Usuarios usuarios)
        {
            if (usuarios == null)
            {

                return false;

            }
            _context.Set<Usuarios>().Remove(usuarios);
            await _context.SaveChangesAsync();

            return true;
        }
        

    }
}
