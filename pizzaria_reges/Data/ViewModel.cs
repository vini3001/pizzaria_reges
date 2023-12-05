using Microsoft.AspNetCore.Mvc;
using pizzaria_reges.Models;

namespace pizzaria_reges.Data
{
    public class ViewModel
    {
        public Cliente cliente { get; set; }
        public Endereco_cli endereco { get; set; }
        public CarrinhoCompra carrinho { get; set; }
        public Produto produto { get; set; }
        public Pedido pedido { get; set; }
    }
}
