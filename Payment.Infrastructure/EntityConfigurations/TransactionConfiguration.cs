using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Model.Transaction;

namespace Payment.Infrastructure.EntityConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration(string schema)
        {
            Schema = schema;
        }

        // comment
        public string Schema { get; }

        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("tblTransactions", Schema);

            // Base Entity Properties
            builder.Property(x => x.Id).HasColumnName("ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Uid).HasColumnName("UID").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.DeletedOn).HasColumnName("DeletedOn").HasColumnType("datetime").IsRequired(false);

            // Entity Properties
            builder.Property(x => x.Amount).HasColumnName("Amount").HasColumnType("decimal").IsRequired();
            builder.Property(x => x.Currency).HasColumnName("Currency").HasColumnType("nvarchar(10)").IsRequired();
            builder.Property(x => x.CardholderNumber).HasColumnName("CardholderNumber").HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.HolderName).HasColumnName("HolderName").HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.ExpirationMonth).HasColumnName("ExpirationMonth").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.ExpirationYear).HasColumnName("ExpirationYear").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.CVV).HasColumnName("CVV").HasColumnType("int").IsRequired();
            builder.Property(x => x.OrderReference).HasColumnName("OrderReference").HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.Status).HasColumnName("TransactionStatus").HasColumnType("tinyint").IsRequired();

        }
    }
}
