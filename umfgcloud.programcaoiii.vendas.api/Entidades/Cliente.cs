namespace umfgcloud.programcaoiii.vendas.api.Entidades
{
    public sealed class Cliente : AbstractEntidade
    {
        public string Nome { get; private set; } = string.Empty; // ou "";
        public string CPF { get; private set; } = string.Empty;
        public string Endereco { get; private set; } = string.Empty;
        public string Telefone { get; private set; } = string.Empty;

        /// <summary>
        /// Para o entity framework conseguir mapear as propriedades 
        /// para os atributos da tabela no banco de dados
        /// </summary>
        private Cliente() { }

        public Cliente(string nome, string cpf, string endereco, string telefone)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(nome);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(cpf);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(endereco);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(telefone);

            Nome = nome.ToUpper();
            CPF = cpf;
            Endereco = endereco.ToUpper();
            Telefone = telefone;
        }
        public void Atualizar(string nome, string cpf, string endereco, string telefone)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(nome);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(cpf);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(endereco);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(telefone);

            Nome = nome.ToUpper();
            CPF = cpf;
            Endereco = endereco.ToUpper();
            Telefone = telefone;

            AtualizarDataAtualizacao();
        }
    }
}
