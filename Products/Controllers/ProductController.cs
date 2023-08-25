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
        public List<Product> Get( 
            [FromServices] AppDbContext context
        )
        {
            return context.Products!.ToList();
        }

        [HttpGet("/{id:int}")]
        public Product GetById(
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
            return context.Products!.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost("/")]
        public Product Post( 
            [FromBody] Product product,
            [FromServices] AppDbContext context
        ) 
        {
            context.Products!.Add(product);
            context.SaveChanges();
            return product;
        }

        [HttpPut("/")]
        public Product Put(
            [FromRoute] int id,
            [FromBody] Product product,
            [FromServices] AppDbContext context
        )
        {
            var model = context.Products!.FirstOrDefault(X => X.Id == id);
            if ( model == null )
            {
                return product;
            }

            model.Nome = product.Nome;
            model.Preco = product.Preco;
            model.Quantidade = product.Quantidade;

            context.Products!.Update(model);
            context.SaveChanges();
            return model;
        }

        [HttpDelete("/")]
        public Product Delete(
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
            var model = context.Products!.FirstOrDefault(x => x.Id == id);

            context.Products!.Remove(model);
            context.SaveChanges();
            return model;
        }
    }
}
