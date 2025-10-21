namespace umfgcloud.programcaoiii.vendas.api.DTO
{
        public sealed class ProdutoRequest
        {
            public string EAN { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public decimal PrecoCompra { get; set; } = decimal.Zero;
            public decimal PrecoVenda { get; set; } = decimal.Zero;
            public decimal Estoque { get; set; } = decimal.Zero;
        }
}
