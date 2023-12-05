using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria_reges.Models
{
    public class Cliente
    {
        [Key]
        public long? Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public int Telefone { get; set; }
        public string email { get; set; }
        public string data_nasc { get; set; }
        public int cpf { get; set; }

        public IList<Pedido>? Pedidos { get; set; }

        [ForeignKey("Endereco")]
        public long? fk_EnderecoID { get; set; }

        public Endereco_cli? Endereco { get; set; }
    }
}
