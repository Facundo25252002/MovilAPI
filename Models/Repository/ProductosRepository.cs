using Microsoft.EntityFrameworkCore;
using MovilAPI.Models.Data;

namespace MovilAPI.Models.Repository
{
    public class ProductosRepository : IProductoRepository
    {
        protected readonly MovilContext _context;
        public ProductosRepository(MovilContext context) => _context= context;

        public IEnumerable<Productos> GetProductos()
        {
            return _context.Productos.ToList();
        }

        public Productos GetProductosById(int id) => _context.Productos.Find(id);

        public async Task<Productos> CreateProductosAsync(Productos productos)
        {
            await _context.Set<Productos>().AddAsync(productos);
            await _context.SaveChangesAsync();
            return productos;
        }

        public async Task<bool> UpdateProductosAsync(Productos productos)
        {
            _context.Entry(productos).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool>DeleteProductosAsync(Productos productos)
        {
            if (productos is null) {

                return false;
            
            }
            _context.Set<Productos>().Remove(productos);
            await _context.SaveChangesAsync();

            return true;
        }

       
    }
}
