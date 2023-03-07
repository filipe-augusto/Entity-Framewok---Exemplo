using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Blog.Controllers
{

    [ApiController]

    public class CategoryController : ControllerBase
    {
        //async - será assincrono ou seja em uma tread separada 
        //wait agaurdar o metodo
        [HttpGet("categories")]//sempre no minusculo e no plural
        [HttpGet("categorias")]
        [HttpGet("v1/categorias")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context)
        {
            try
            {
                var categorias = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categorias));
               // return Ok(categorias);
            }
            catch 
            {

                return StatusCode(500, new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));
            }

        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
    [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    NotFound(new ResultViewModel<Category>("Couteúdo não encontrado."));
                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
            }
       
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
      [FromBody] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context)
        {

            if (!ModelState.IsValid)
                //return BadRequest(ModelState.GetErrors());
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
            try
            {

                var category = new Category
                {
                    Id = 0,
                    Posts = null,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("0XE9 - Não foi possivel incluir a categoria") );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05x10 -Falha interna no servidor"));
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return BadRequest(new ResultViewModel<Category>("Conteúdo não encontrado."));

                category.Name = model.Name;
                category.Slug = model.Slug;
                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05x11 - Não foi possivel incluir a categoria ex:" + ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05x12 - falha interna no servidor: ex:" + ex.Message));
            }
        }


    


    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
    [FromRoute] int id,
    [FromServices] BlogDataContext context)
    {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05x13 - Não foi possivel deletar a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05x14 - falha interna no servidor");

            }
        }

}
}
