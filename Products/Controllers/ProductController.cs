using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Models;

namespace Products.Controllers
{
    [ApiController] // Indicar que a classe é uma API Controller
    [Route("api/[controller]")] // Será acessível pela rota api/product
    public class ProductController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get( 
            [FromServices] AppDbContext context
        )
        {
            return Ok(context.Products!.ToList());
        }

        [HttpGet("/{id:int}")]
        public IActionResult GetById(
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
            var model = context.Products!.FirstOrDefault(x => x.Id == id);
        
            if (model == null) 
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPost("/")]
        public IActionResult Post( 
            [FromBody] Product product,
            [FromServices] AppDbContext context
        ) 
        {
            context.Products!.Add(product);
            context.SaveChanges();
            return Created($"/{product.Id}", product);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] Product product,
            [FromServices] AppDbContext context
        )
        {
            var model = context.Products!.FirstOrDefault(x => x.Id == id);
            if ( model == null )
            {
                return NotFound();
            }

            model.Nome = product.Nome;
            model.Preco = product.Preco;
            model.Quantidade = product.Quantidade;

            context.Products!.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete(
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
            var model = context.Products!.FirstOrDefault(x => x.Id == id);

            if ( model == null )
            {
                return NotFound();
            }
            context.Products!.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}
