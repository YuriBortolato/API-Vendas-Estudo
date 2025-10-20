namespace umfgcloud.programcaoiii.vendas.api.Entidades
{
    public sealed class Vendedor : AbstractEntidade
    {
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string? Telefone { get; private set; }

        private Vendedor() { }

        public Vendedor(string nome, string email, string? telefone = null)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(nome);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(email);

            if (nome.Length < 3)
                throw new ArgumentException("O nome deve ter no mínimo 3 caracteres.");

            if (!email.Contains("@") || !email.Contains("."))
                throw new ArgumentException("E-mail inválido.");

            Nome = nome.ToUpper();
            Email = email.ToLower();
            Telefone = telefone;
        }

        public void Atualizar(string nome, string email, string? telefone = null)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(nome);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(email);

            if (nome.Length < 3)
                throw new ArgumentException("O nome deve ter no mínimo 3 caracteres.");

            if (!email.Contains("@") || !email.Contains("."))
                throw new ArgumentException("E-mail inválido.");

            Nome = nome.ToUpper();
            Email = email.ToLower();
            Telefone = telefone;

            AtualizarDataAtualizacao();
        }
    }
}
