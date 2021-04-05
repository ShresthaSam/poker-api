using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerApi.Repositories.Entities;

namespace PokerApi.Repositories.Configurations
{
    public class CardEntityConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(p => p.CardId);
            builder.Property(p => p.CardId).IsRequired();
            builder.Property(b => b.CardId).HasColumnName("card_id");
            builder.Property(b => b.Suit).HasColumnName("suit");
            builder.Property(b => b.Number).HasColumnName("number");
            builder.ToTable("card");
        }
    }
}
