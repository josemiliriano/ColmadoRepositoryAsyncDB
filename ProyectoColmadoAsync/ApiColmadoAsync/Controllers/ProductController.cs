using ApiColmadoAsync.Dto;
using ApiColmadoAsync.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace ApiColmadoAsync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDataContext _context;
        public ProductController(MyDataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDto dto)
        {
            var NewProduct = await _context.Products.AnyAsync(p => p.ProductName.ToLower() == dto.ProductName);
            if (NewProduct)
            {
                return BadRequest("El producto digitado exite en la base de datos");
            }
            var product = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                SalePrice = dto.SalePrice,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllProductsWhitCategory()
        {
            var ProductList = await _context.Products.Include(p => p.Category).Select(p => new ProductCompleteDto
            {
                ProductName = p.ProductName,
                Price = p.Price,
                SalePrice = p.SalePrice,
                Stock = p.Stock,
                CategoryName = p.Category.CategoryName
            }).ToListAsync();
            return Ok(ProductList);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult>GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return Ok(product);
        }
        [HttpGet("name/{name}")]
        public async Task<ActionResult>GetByName(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p=> p.ProductName== name);
            if (product == null)
            {
                return NotFound("El producto no existe en la base de datos");
            }
            return Ok(product);
        }
        [HttpGet("Senciblename/{SencibleName}")]
        public async Task<ActionResult>getProductBySencibleName(string SencibleName)
        {
            var product = await _context.Products.Where(p => p.ProductName.Contains(SencibleName)).ToListAsync();
            if (product == null)
            {
                return NotFound("El producto no se encuentra en la base de datos");
            }
            return Ok(product);
        }
        [HttpGet("NoDelete")]
        public async Task<ActionResult> GetNoDelete()
        {
            var listProduct = await _context.Products.Where(p => p.IsDelete == '0').ToListAsync();
            return Ok(listProduct);
        }
        [HttpPut]
        public async Task<ActionResult>UpdateProduct(int id, ProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"el producto con el id {id} no existe en base de datos");
            }
            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.SalePrice = dto.SalePrice;
            product.Stock = dto.Stock;
            product.CategoryId = dto.CategoryId;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"El id {id} no existe en la base de datos");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult>SoftDelete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"El id {id} no existe en la base de datos");
            }
            product.IsDelete = '1';
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok("El producto fue borrado correctamente");
        }
        [HttpGet("GetProductWithCategory")]
        public async Task<ActionResult> GetProductWithCategory()
        {
            var listProduct = await _context.Products.Include(p => p.Category).Where(p => p.IsDelete == '0').Select(p => new ProductCompleteDto
            {
                ProductName = p.ProductName,
                Price = p.Price,
                SalePrice = p.SalePrice,
                Stock = p.Stock,
                CategoryName = p.Category.CategoryName
            }).ToListAsync();
            return Ok(listProduct);
        }
            
    }
}
