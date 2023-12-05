using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria_reges.Models
{
    public class CarrinhoCompra
    {
        [Key]
        public long? Id { get; set; }

        [ForeignKey("Pedido")]
        public long? fk_PedidoID { get; set; }

        public Pedido? Pedido{ get; set; }

        [ForeignKey("Produto")]
        public long? fk_ProdutoID { get; set; }

        public Produto? Produto { get; set; }
        
    }
}
