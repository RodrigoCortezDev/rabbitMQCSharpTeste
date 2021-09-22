using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;

namespace wpfRabbitMQ.Postgres.DB
{
    [DisplayName("Usuários")]
    public class tbUser
    {
        public long user_pk { get; set; } // PK
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_senha { get; set; }
    }

    public class staFiscalCstConfiguration : IEntityTypeConfiguration<tbUser>
    {
        public void Configure(EntityTypeBuilder<tbUser> builder)
        {
            builder.HasKey(x => x.user_pk);
            builder.Property(x => x.user_pk).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.user_name).IsUnicode(false).HasMaxLength(300);
            builder.Property(x => x.user_email).IsUnicode(false).HasMaxLength(500);
            builder.Property(x => x.user_senha).IsUnicode(false).HasMaxLength(500);
        }
    }
}
