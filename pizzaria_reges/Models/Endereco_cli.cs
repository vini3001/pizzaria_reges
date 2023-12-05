using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pizzaria_reges.Models
{
    public class Endereco_cli
    {
        [Key]
        public long? Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public int Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
