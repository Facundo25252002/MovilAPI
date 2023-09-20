using MovilAPI.Models.Data;

namespace MovilAPI.Models.Repository
{
    public interface IProductoRepository
    {
        Task<Productos>CreateProductosAsync(Productos productos);
        Task<bool>DeleteProductosAsync(Productos productos);
        Productos GetProductosById(int id);
        IEnumerable<Productos> GetProductos();
        Task<bool>UpdateProductosAsync(Productos productos);

    }
}
