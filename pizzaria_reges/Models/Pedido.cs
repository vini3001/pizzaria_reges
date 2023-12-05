using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria_reges.Models
{
    public class Pedido
    {
        [Key]
        public long? Id { get; set; }
        public int Numero { get; set; }
        public int Quantidade { get; set; }
        public float? ValorTotal { get; set; }
        public DateTime? PedidoData{ get; set; }

        [ForeignKey("Cliente")]
        public long? fk_ClienteID { get; set; }

        public Cliente? Cliente { get; set; }

    }
}
