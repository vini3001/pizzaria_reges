using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria_reges.Models
{
    public class Produto
    {
        [Key]
        public long? Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public string descricao { get; set; }
        public string urlImg { get; set; }
        public string Categoria { get; set; }
        public DateTime dataCadastro { get; set; }
    }
}
