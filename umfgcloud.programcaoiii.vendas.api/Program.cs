using Microsoft.EntityFrameworkCore;
using umfgcloud.programcaoiii.vendas.api.Contexto;
using umfgcloud.programcaoiii.vendas.api.Entidades;

namespace umfgcloud.programcaoiii.vendas.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connection = "Server=localhost;" +
                "Port=3307;" +
                "Database=umfg_vendas;" +
                "Uid=root;Pwd=root";

            var builder = WebApplication.CreateBuilder(args);

            // configuração de acesso ao banco de dados
            builder.Services.AddDbContext<ContextoVenda>(option =>
                option.UseMySQL(connection));

            var app = builder.Build();

            //mapeamento dos end-points

            app.MapGet("/clientes", (ContextoVenda contexto) => 
            { 
                return contexto.Clientes.ToList();
            });

            app.Run();
        }
    }
}
