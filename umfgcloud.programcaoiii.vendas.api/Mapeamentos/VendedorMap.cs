using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using umfgcloud.programcaoiii.vendas.api.Entidades;

namespace umfgcloud.programcaoiii.vendas.api.Mapeamentos
{
    public sealed class VendedorMap : AbstractEntidadeMap<Vendedor>
    {
        public override void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            base.Configure(builder);

            builder.ToTable("VENDEDOR");

            builder
                .Property(x => x.Nome)
                .HasColumnName("NM_VENDEDOR")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasColumnName("DS_EMAIL")
                .HasMaxLength(150)
                .IsRequired();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.Telefone)
                .HasColumnName("NR_TELEFONE")
                .HasMaxLength(20)
                .IsRequired(false);
        }
    }
}
