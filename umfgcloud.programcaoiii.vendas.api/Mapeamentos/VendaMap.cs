using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using umfgcloud.programcaoiii.vendas.api.Entidades;

namespace umfgcloud.programcaoiii.vendas.api.Mapeamentos
{
    public sealed class VendaMap : AbstractEntidadeMap<Venda>
    {
        public override void Configure(EntityTypeBuilder<Venda> builder)
        {
            base.Configure(builder);

            builder.ToTable("VENDA");

            //exemplo de propriedade calculada em tempo de execução
            //e ignorada no banco de dados
            builder.Ignore(x => x.Total);
            // relacionamento com cliente
            builder
                .HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey("ID_CLIENTE")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // relacionamento com vendedor 
            builder
                .HasOne(x => x.Vendedor)
                .WithMany()
                .HasForeignKey("ID_VENDEDOR")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            // relacionamento com itens de venda 
            builder
                .HasMany(x => x.Itens)
                .WithOne()
                .HasForeignKey("ID_VENDA")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
