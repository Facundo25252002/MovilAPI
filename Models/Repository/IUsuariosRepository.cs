using MovilAPI.Models.Data;

namespace MovilAPI.Models.Repository
{
    public interface IUsuariosRepository
    {
        Task<Usuarios> CreateUsuariosAsync(Usuarios usuarios);
        Task<bool>DeleteUsuariosAsync(Usuarios usuarios);
        Usuarios GetUsuariosById(int id);
        IEnumerable<Usuarios>GetUsuarios();
        //.AsQueryable() después de .GetUsuarios() para convertir la secuencia IEnumerable<Usuarios>
        //en una consulta IQueryable<Usuarios>, para poder utilizar  .FirstOrDefaultAsync();
        //tambien agregue using Linq  [Esta solucion no  funciono ] 


        // convertir  en una lista y despues guardarlo en una variable  por ejemplo userEncontrado = usuarioList.FirstOrDefault();
        // de esa manera  me deja usar el metodo FirstOrDefault(); //
        Task<IEnumerable<Usuarios>> GetUsuariosAsync();
        Task<bool>UpdateUsuariosAsync(Usuarios usuarios);
       
    }
}
