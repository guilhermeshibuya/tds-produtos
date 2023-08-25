using Products.Models;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var products = new List<Product>
{
    new Product { Id = 1, Nome = "Café Epecial Black Tucano Single Origin 250g", Preco = 24.0, Quantidade = 10 },
    new Product { Id = 2, Nome = "Mouse Ajjaz aj199", Preco = 189.90, Quantidade = 30 },
    new Product { Id = 3, Nome = "Placa de vídeo RTX 4080 Asus Rog Stix", Preco = 26.89, Quantidade = 15 }
};

builder.Services.AddSingleton(products);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// ROTA GET 
app.MapGet("/products", () =>
{
    var productService = app.Services.GetRequiredService<List<Product>>();
    return Results.Ok(productService);
});

// ROTA GET COM ID
app.MapGet("/products/{id}", (int id, HttpRequest request) =>
{
    var productService = app.Services.GetRequiredService<List<Product>>();
    var product = productService.FirstOrDefault(p => p.Id == id);

    if (product == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(product);
});

// ROTA POST
app.MapPost("/products", (Product product) =>
{
    var context = new ValidationContext(product);
    var results = new List<ValidationResult>();

    bool isValid = Validator.TryValidateObject(product, context, results);

    if (!isValid)
    {
        foreach (var validation in results)
        {
            Console.WriteLine(validation.ErrorMessage);
        }
        return Results.BadRequest();
    }
   
    var productService = app.Services.GetRequiredService<List<Product>>();
    product.Id = productService.Max(p => p.Id) + 1;
    productService.Add(product);

    return Results.Created($"/products/{product.Id}", product);
});

// ROTA PUT
app.MapPut("/products/{id}", (int id, Product product) =>
{
    var context = new ValidationContext(product);
    var results = new List<ValidationResult>();

    bool isValid = Validator.TryValidateObject(product, context, results);

    if (!isValid)
    {
        foreach (var validation in results)
        {
            Console.WriteLine(validation.ErrorMessage);
        }
        return Results.BadRequest();
    }

    var productService = app.Services.GetRequiredService<List<Product>>();
    var existingProduct = productService.FirstOrDefault(p => p.Id == id);

    if (existingProduct == null)
    {
        return Results.NotFound();
    }
    
    existingProduct.Nome = product.Nome;
    existingProduct.Preco = product.Preco;
    existingProduct.Quantidade = product.Quantidade;

    return Results.NoContent();
});

// ROTA DELETE
app.MapDelete("/products/{id}", (int id) =>
{
    var productService = app.Services.GetRequiredService<List<Product>>();
    var existingProduct = productService.FirstOrDefault(p => p.Id == id);

    if (existingProduct == null)
    {
        return Results.NotFound();
    }

    productService.Remove(existingProduct);

    return Results.NoContent();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
