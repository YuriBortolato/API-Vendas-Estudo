namespace umfgcloud.programcaoiii.vendas.api.DTO
{
        public sealed class VendedorRequest
        {
            public string Nome { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string? Telefone { get; set; }
        }
}
