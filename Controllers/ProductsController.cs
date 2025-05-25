using jycbackend.Data;
using jycbackend.DTOs;
using jycbackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jycbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context) => _context = context;

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetProducts()
        {
            var products = await _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            return Ok(new ApiResponse<IEnumerable<ProductDto>>(true, products, "Productos obtenidos correctamente"));
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound(new ApiResponse<ProductDto>(false, null!, "Producto no encontrado"));

            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt
            };

            return Ok(new ApiResponse<ProductDto>(true, dto, "Producto obtenido correctamente"));
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var result = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt
            };

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new ApiResponse<ProductDto>(true, result, "Producto creado correctamente"));
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> UpdateProduct(Guid id, CreateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new ApiResponse<ProductDto>(false, null!, "Producto no encontrado"));

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;

            await _context.SaveChangesAsync();

            var result = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt
            };

            return Ok(new ApiResponse<ProductDto>(true, result, "Producto actualizado correctamente"));
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new ApiResponse<string>(false, null!, "Producto no encontrado"));

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<string>(true, "Producto eliminado correctamente", "Producto eliminado correctamente"));
        }
    }
}
