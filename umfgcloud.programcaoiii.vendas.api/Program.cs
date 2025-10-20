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

            app.Run();
        }
    }
}
