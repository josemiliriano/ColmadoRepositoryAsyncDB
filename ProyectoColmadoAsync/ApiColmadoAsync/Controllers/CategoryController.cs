using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiColmadoAsync;
using ApiColmadoAsync.Dto;
using ApiColmadoAsync.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiColmadoAsync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDataContext _context;
        public CategoryController(MyDataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryDto dto)
        {
            var exit = await _context.Categories.AnyAsync(p => p.CategoryName.ToLower() == dto.CategoryName);
            if (exit)
            {
                return BadRequest("El producto digitado ya existe en la base de datos");
            }
            var newCategory = new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return Ok(newCategory);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCategory()
        {
            var CategoryList = await _context.Categories.ToListAsync();
            return Ok(CategoryList);
        }
        [HttpGet ("{id:int}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var Category = await _context.Categories.FindAsync(id);
            if (Category == null)
            {
                return NotFound($"El id {id} no existe en la base de datos");
            }
            return Ok(Category);
        }
        [HttpGet("name/{name}")]
        public async Task<ActionResult>GetCategoryByName(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c=> c.CategoryName == name);
            if (category == null)
            {
                return NotFound(new { message = "La categoria no exite en la base de datos" });
            }
            return Ok(category);
        }
        [HttpGet("Senciblename/{SencibleName}")]
        public async Task<ActionResult> GetCategoryByNameSencible(string SencibleName)
        {
            var category = await _context.Categories.Where(c => c.CategoryName.Contains(SencibleName)).ToListAsync();           
            if (category == null)
            {
                return NotFound(new { message = "La categoria no existe en la base de datos" });
            }
            return Ok(category);
        }       
        [HttpGet("NoDelete")]
        public async Task<ActionResult> GetCategoryNoDelete()
        {
            var CategoryList = await _context.Categories.Where(c => c.Isdelete == '0').ToListAsync();
            if(CategoryList == null)
            {
                return NotFound("Todos las categorias estan borradas consulte su administrador de Base de Datos");
            }
            return Ok(CategoryList);
        }
        [HttpPut]
        public async Task<ActionResult>UpdateCategory(int id, CategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = $"El Id {id} no existe en la base de datos" });
            }
            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = $"El Id {id} no existe en la base de datos" });
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult>SoftDelete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = $"El id {id} no existe en la base de datos" });
            }
            category.Isdelete = '1';
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return Ok(new { message = "El producto fue borrado correctamente" });
        }
        
    }
}
