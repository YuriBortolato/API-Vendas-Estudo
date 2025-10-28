using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using umfgcloud.programcaoiii.vendas.api.Contexto;
using umfgcloud.programcaoiii.vendas.api.DTO;
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

            // Clientes (CRUD Completo)
            
            app.MapGet("/clientes", (ContextoVenda contexto) =>
            {
                return Results.Ok(contexto.Clientes.Where(x => x.IsAtivo).ToList());
            });

            app.MapGet("/clientes/{id}", (Guid id, ContextoVenda contexto) =>
            {
                var cliente = contexto.Clientes.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                return cliente != null ? Results.Ok(cliente) : Results.NotFound("Cliente não encontrado");
            });

            app.MapPost("/clientes", ([FromBody] ClienteRequest dto, ContextoVenda contexto) =>
            {
                try
                {
                    var cliente = new Cliente(dto.Nome, dto.CPF, dto.Endereco, dto.Telefone);
                    contexto.Clientes.Add(cliente);
                    contexto.SaveChanges();
                    return Results.Created($"/clientes/{cliente.Id}", cliente);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.StatusCode(500);
                }
            });

            app.MapPut("/clientes/{id}", (Guid id, [FromBody] ClienteRequest dto, ContextoVenda contexto) =>
            {
                try
                {
                    var cliente = contexto.Clientes.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                    if (cliente == null)
                        return Results.NotFound("Cliente não encontrado");

                    cliente.Atualizar(dto.Nome, dto.CPF, dto.Endereco, dto.Telefone);
                    contexto.Clientes.Update(cliente);
                    contexto.SaveChanges();
                    return Results.Ok(cliente);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.StatusCode(500);
                }
            });

            app.MapDelete("/clientes/{id}", (Guid id, ContextoVenda contexto) =>
            {
                var cliente = contexto.Clientes.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                if (cliente == null)
                    return Results.NotFound("Cliente não encontrado");

                cliente.Inativar();
                contexto.Clientes.Update(cliente);
                contexto.SaveChanges();
                return Results.NoContent();
            });

            // Produtos (CRUD Completo)
            app.MapGet("/produtos", (ContextoVenda contexto) =>
            {
                return Results.Ok(contexto.Produtos.Where(x => x.IsAtivo).ToList());
            });

            app.MapGet("/produtos/{id}", (Guid id, ContextoVenda contexto) =>
            {
                var produto = contexto.Produtos.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                return produto != null ? Results.Ok(produto) : Results.NotFound("Produto não encontrado");
            });

            app.MapPost("/produtos", ([FromBody] ProdutoRequest dto, ContextoVenda contexto) =>
            {
                try
                {
                    var produto = new Produto(dto.EAN, dto.Descricao, dto.PrecoCompra, dto.PrecoVenda, dto.Estoque);
                    contexto.Produtos.Add(produto);
                    contexto.SaveChanges();
                    return Results.Created($"/produtos/{produto.Id}", produto);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.StatusCode(500);
                }
            });

            app.MapPut("/produtos/{id}", (Guid id, [FromBody] ProdutoRequest dto, ContextoVenda contexto) =>
            {
                try
                {
                    var produto = contexto.Produtos.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                    if (produto == null)
                        return Results.NotFound("Produto não encontrado");

                    produto.Atualizar(dto.EAN, dto.Descricao, dto.PrecoCompra, dto.PrecoVenda, dto.Estoque);
                    contexto.Produtos.Update(produto);
                    contexto.SaveChanges();
                    return Results.Ok(produto);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.StatusCode(500);
                }
            });

            app.MapDelete("/produtos/{id}", (Guid id, ContextoVenda contexto) =>
            {
                var produto = contexto.Produtos.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                if (produto == null)
                    return Results.NotFound("Produto não encontrado");

                produto.Inativar();
                contexto.Produtos.Update(produto);
                contexto.SaveChanges();
                return Results.NoContent();
            });

            // Vendedores (CRUD Completo)
            app.MapGet("/vendedores", (ContextoVenda contexto) =>
            {
                return Results.Ok(contexto.Vendedores.Where(x => x.IsAtivo).ToList());
            });

            app.MapGet("/vendedores/{id}", (Guid id, ContextoVenda contexto) =>
            {
                var vendedor = contexto.Vendedores.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                return vendedor != null ? Results.Ok(vendedor) : Results.NotFound("Vendedor não encontrado");
            });

            app.MapPost("/vendedores", ([FromBody] VendedorRequest dto, ContextoVenda contexto) =>
            {
                try
                {
                    // Validação de email único
                    if (contexto.Vendedores.Any(x => x.Email == dto.Email.ToLower() && x.IsAtivo))
                        return Results.BadRequest("O e-mail informado já está em uso.");

                    var vendedor = new Vendedor(dto.Nome, dto.Email, dto.Telefone);
                    contexto.Vendedores.Add(vendedor);
                    contexto.SaveChanges();
                    return Results.Created($"/vendedores/{vendedor.Id}", vendedor);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.StatusCode(500);
                }
            });

            app.MapPut("/vendedores/{id}", (Guid id, [FromBody] VendedorRequest dto, ContextoVenda contexto) =>
            {
                try
                {
                    var vendedor = contexto.Vendedores.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                    if (vendedor == null)
                        return Results.NotFound("Vendedor não encontrado");

                    // Validação de email único na atualização
                    if (contexto.Vendedores.Any(x => x.Email == dto.Email.ToLower() && x.Id != id && x.IsAtivo))
                        return Results.BadRequest("O e-mail informado já está em uso por outro vendedor.");

                    vendedor.Atualizar(dto.Nome, dto.Email, dto.Telefone);
                    contexto.Vendedores.Update(vendedor);
                    contexto.SaveChanges();
                    return Results.Ok(vendedor);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.StatusCode(500);
                }
            });

            app.MapDelete("/vendedores/{id}", (Guid id, ContextoVenda contexto) =>
            {
                var vendedor = contexto.Vendedores.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                if (vendedor == null)
                    return Results.NotFound("Vendedor não encontrado");

                vendedor.Inativar(); // Soft delete
                contexto.Vendedores.Update(vendedor);
                contexto.SaveChanges();
                return Results.NoContent();
            });

            // Vendas (CRUD Completo e Ajustes)
            app.MapGet("/vendas", (ContextoVenda contexto) =>
            {
                var vendas = contexto.Vendas
                    .Include(v => v.Cliente)
                    .Include(v => v.Vendedor)
                    .Where(v => v.IsAtivo)
                    .ToList();
                return Results.Ok(vendas);
            });

            app.MapGet("/vendas/{id}", (Guid id, ContextoVenda contexto) =>
            {
                var venda = contexto.Vendas
                    .Include(v => v.Cliente)
                    .Include(v => v.Vendedor)
                    .Include(v => v.Itens)
                        .ThenInclude(i => i.Produto)
                    .FirstOrDefault(v => v.Id == id && v.IsAtivo);

                return venda != null ? Results.Ok(venda) : Results.NotFound("Venda não encontrada");
            });

            app.MapPost("/vendas", ([FromBody] TransacaoDTO.TransacaoCapaRequest dto,
                ContextoVenda contexto) =>
            {
                // Validação de ClienteId
                Cliente? cliente = contexto
                    .Clientes
                    .FirstOrDefault(x => x.Id == dto.IdCliente && x.IsAtivo);

                if (cliente == null)
                    return Results.NotFound("Cliente não cadastrado ou inativo!");

                // Validação de VendedorId
                Vendedor? vendedor = contexto
                    .Vendedores
                    .FirstOrDefault(x => x.Id == dto.IdVendedor && x.IsAtivo);

                if (vendedor == null)
                    return Results.NotFound("Vendedor não cadastrado ou inativo!");

                try
                {
                    Venda venda = new Venda(cliente, vendedor);
                    contexto.Vendas.Add(venda);
                    contexto.SaveChanges();

                    return Results.Created($"vendas/{venda.Id}", venda);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            app.MapPut("/vendas/{id}", (Guid id, [FromBody] TransacaoDTO.TransacaoCapaRequest dto, ContextoVenda contexto) =>
            {
                var venda = contexto.Vendas.FirstOrDefault(x => x.Id == id && x.IsAtivo);
                if (venda == null)
                    return Results.NotFound("Venda não encontrada");

                // Validação de ClienteId
                Cliente? cliente = contexto
                    .Clientes
                    .FirstOrDefault(x => x.Id == dto.IdCliente && x.IsAtivo);

                if (cliente == null)
                    return Results.NotFound("Cliente não cadastrado ou inativo!");

                // Validação de VendedorId
                Vendedor? vendedor = contexto
                    .Vendedores
                    .FirstOrDefault(x => x.Id == dto.IdVendedor && x.IsAtivo);

                if (vendedor == null)
                    return Results.NotFound("Vendedor não cadastrado ou inativo!");

                try
                {
                    venda.AtualizarCliente(cliente);
                    venda.AtualizarVendedor(vendedor);
                    contexto.Vendas.Update(venda);
                    contexto.SaveChanges();
                    return Results.Ok(venda);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            app.MapDelete("/vendas/{id}", (Guid id, ContextoVenda contexto) =>
            {
                var venda = contexto.Vendas
                    .Include(v => v.Itens)
                    .FirstOrDefault(x => x.Id == id && x.IsAtivo);

                if (venda == null)
                    return Results.NotFound("Venda não encontrada");

                foreach (var item in venda.Itens)
                {
                    item.Inativar();
                    contexto.ItensVenda.Update(item);
                }

                venda.Inativar();
                contexto.Vendas.Update(venda);
                contexto.SaveChanges();
                return Results.NoContent();
            });


            // Itens de Venda (Ajustes de Validação)
            app.MapPost("/vendas/{idVenda}/itens", (
                Guid idVenda,
                TransacaoDTO.TransacaoItemRequest dto,
                ContextoVenda contexto) =>
            {
                // Validação Venda
                Venda? venda = contexto
                    .Vendas
                        .Include(x => x.Cliente)
                        .Include(x => x.Vendedor)
                        .Include(x => x.Itens)
                            .ThenInclude(x => x.Produto)
                        .FirstOrDefault(x => x.Id == idVenda && x.IsAtivo);

                if (venda == null)
                    return Results.NotFound("Venda não cadastrada!");

                // Validação Produto
                Produto? produto = contexto
                    .Produtos
                    .FirstOrDefault(x => x.Id == dto.IdProduto && x.IsAtivo);

                if (produto == null)
                    return Results.NotFound("Produto não cadastrado!");

                try
                {
                    // Validações de negócio
                    if (dto.Quantidade <= 0.0m)
                        throw new InvalidOperationException("Quantidade informada inválida!");

                    if (produto.Estoque - dto.Quantidade < 0.0m)
                        throw new InvalidOperationException("Não há estoque suficiente para venda!");

                    produto.AbaterEstoque(dto.Quantidade);

                    ItemVenda itemVenda = new ItemVenda(produto, dto.Quantidade);

                    venda.AdicionarItem(itemVenda);

                    contexto.ItensVenda.Add(itemVenda);
                    contexto.Produtos.Update(produto);
                    contexto.Vendas.Update(venda);

                    contexto.SaveChanges();

                    return Results.Created($"vendas/{idVenda}", venda);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            // GET: Listar todos os itens de UMA venda
            app.MapGet("/vendas/{idVenda}/itens", (Guid idVenda, ContextoVenda contexto) =>
            {
                var vendaExiste = contexto.Vendas.Any(v => v.Id == idVenda && v.IsAtivo);
                if (!vendaExiste)
                    return Results.NotFound("Venda não cadastrada!");

                var itensDaVenda = contexto.Vendas
                .Where(v => v.Id == idVenda)    
                .SelectMany(v => v.Itens)       
                .Include(i => i.Produto)        
                .Where(i => i.IsAtivo)          
                .ToList();

                return Results.Ok(itensDaVenda);
            });


            // GET: Obter UM item específico de uma venda
            app.MapGet("/vendas/{idVenda}/itens/{idItem}", (Guid idVenda, Guid idItem, ContextoVenda contexto) =>
            {
                var itemVenda = contexto.Vendas
                .Where(v => v.Id == idVenda && v.IsAtivo) 
                .SelectMany(v => v.Itens)                
                .Include(i => i.Produto)
                .FirstOrDefault(i => i.Id == idItem && i.IsAtivo);

                if (itemVenda == null)
                    return Results.NotFound("Item de venda não cadastrado ou não pertence a esta venda.");

                return Results.Ok(itemVenda);
            });

            // PUT: Atualizar a quantidade de um item
            app.MapPut("/vendas/{idVenda}/itens/{idItem}", (
                Guid idVenda,
                Guid idItem,
                [FromBody] TransacaoDTO.TransacaoItemRequest dto,
                ContextoVenda contexto) =>
            {
                // Validação de Venda
                var venda = contexto.Vendas
                    .Include(v => v.Itens.Where(i => i.Id == idItem && i.IsAtivo))
                        .ThenInclude(i => i.Produto)
                    .FirstOrDefault(v => v.Id == idVenda && v.IsAtivo);

                if (venda == null)
                    return Results.NotFound("Venda não cadastrada!");

                // Validação ItemVenda
                var itemVenda = venda.Itens.FirstOrDefault();
                if (itemVenda == null)
                    return Results.NotFound("Item de venda não cadastrado nesta venda.");

                var produto = itemVenda.Produto;
                decimal antigaQuantidade = itemVenda.Quantidade;
                decimal novaQuantidade = dto.Quantidade;

                if (novaQuantidade <= 0.0m)
                    return Results.BadRequest("Quantidade informada inválida!");

                if (produto.Estoque + antigaQuantidade < novaQuantidade)
                    return Results.BadRequest("Não há estoque suficiente para esta alteração!");

                try
                {
                    
                    produto.AdicionarEstoque(antigaQuantidade);

                    produto.AbaterEstoque(novaQuantidade);

                    itemVenda.AtualizarQuantidade(novaQuantidade);

                    contexto.Produtos.Update(produto);
                    contexto.ItensVenda.Update(itemVenda);
                    contexto.SaveChanges();

                    return Results.Ok(itemVenda);
                }
                catch (ArgumentException ex) 
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });


            app.MapDelete("/vendas/{idVenda}/itens/{idItem}", (Guid idVenda,
                Guid idItem, ContextoVenda contexto) =>
            {
                // Validação Venda
                Venda? venda = contexto
                    .Vendas
                    .Include(x => x.Cliente)
                    .Include(x => x.Vendedor)
                    .Include(x => x.Itens)
                        .ThenInclude(x => x.Produto)
                    .FirstOrDefault(x => x.Id == idVenda && x.IsAtivo);

                if (venda == null)
                    return Results.NotFound("Venda não cadastrada!");

                // Validação ItemVenda
                ItemVenda? itemVenda = venda
                    .Itens
                    .FirstOrDefault(x => x.Id == idItem && x.IsAtivo);

                if (itemVenda == null)
                    return Results.NotFound("Item de venda não cadastrado");

                try
                {
                    // Reverte o estoque
                    itemVenda.Produto.AdicionarEstoque(itemVenda.Quantidade);

                    // Soft delete do item
                    itemVenda.Inativar();

                    contexto.Produtos.Update(itemVenda.Produto);
                    contexto.ItensVenda.Update(itemVenda);
                    contexto.SaveChanges();

                    // Retorna a venda atualizada sem o item 
                    var vendaAtualizada = contexto.Vendas
                        .Include(v => v.Cliente)
                        .Include(v => v.Vendedor)
                        .Include(v => v.Itens.Where(i => i.IsAtivo)) 
                            .ThenInclude(i => i.Produto)
                        .FirstOrDefault(v => v.Id == idVenda && v.IsAtivo);

                    return Results.Ok(vendaAtualizada);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            app.Run();
        }
    }
}
