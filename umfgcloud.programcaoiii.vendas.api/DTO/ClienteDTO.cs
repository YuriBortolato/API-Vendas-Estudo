namespace umfgcloud.programcaoiii.vendas.api.DTO
{
        public sealed class ClienteRequest
        {
            public string Nome { get; set; } = string.Empty;
            public string CPF { get; set; } = string.Empty;
            public string Endereco { get; set; } = string.Empty;
            public string Telefone { get; set; } = string.Empty;
        }
}
