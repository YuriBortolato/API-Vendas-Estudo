using umfgcloud.programcaoiii.vendas.api.Entidades;

namespace umfgcloud.programcaoiii.vendas.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // configuração de acesso ao banco de dados

            var app = builder.Build();

            //mapeamento dos end-points

            //TESTE

            Cliente cliente = new Cliente("TESTE", "00000000000", "RUA TESTE 123", "4436285555");
            Produto produto = new Produto("123456789", "PRODUTO TESTE", 10.99m, 79.99m, 600);
            Venda venda = new Venda(cliente);
            ItemVenda itemVenda = new ItemVenda(produto, 10);

            venda.AdicionarItem(itemVenda);

            app.Run();
        }
    }
}
