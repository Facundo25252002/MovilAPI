
using MovilAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MovilAPI.Models.Repository;
using MovilAPI.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MovilAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
            private IProductoRepository _productosRepository;

            public ProductosController(IProductoRepository productosRepository)
            {
                _productosRepository = productosRepository;
            }

        [HttpGet]
        [ActionName(nameof(GetProductosAsync))]
        public IEnumerable<Productos> GetProductosAsync()
        {
            return _productosRepository.GetProductos();
        }


        [HttpGet("{id}")]
        [ActionName(nameof(GetProductosById))]
        public ActionResult<Productos> GetProductosById(int id)
        {
            var productosByID = _productosRepository.GetProductosById(id);
            if (productosByID == null)
            {
                return NotFound();
            }
            return Ok(productosByID);
        }



        [HttpPost]
        [ActionName(nameof(CreateProductosAsync))]
        public async Task<ActionResult<Productos>> CreateProductosAsync(Productos productos)
        {
            await _productosRepository.CreateProductosAsync(productos);
            return CreatedAtAction(nameof(GetProductosById), new { id = productos.id }, productos);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdateProductos))]
        public async Task<ActionResult> UpdateProductos(int id, Productos productos)
        {
            if (id != productos.id)
            {
                return BadRequest();
            }

            var update = await _productosRepository.UpdateProductosAsync(productos);
            if (!update) {

                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteProductos))]
        public async Task<IActionResult> DeleteProductos(int id)
        {
            var productos = _productosRepository.GetProductosById(id);
            if (productos == null)
            {
                return NotFound(); //404 server Error
            }

            var deleted = await _productosRepository.DeleteProductosAsync(productos);
            if (!deleted) {

                return StatusCode(500);// internal Server Error 500
            
            }
            return NoContent(); // 204 no content
        }
        


    }   
}
