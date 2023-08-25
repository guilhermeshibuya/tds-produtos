using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="O nome do produto não pode ser vazio")]
        public string Nome { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "O valor não pode ser menor que zero")]
        public double Preco { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser menor que zero")]
        public int Quantidade { get; set; }

        public Product(int id, string nome, double preco, int quantidade)
        {
            Id = id;
            Nome = nome;    
            Preco = preco;
            Quantidade = quantidade;
        }
    }
}
