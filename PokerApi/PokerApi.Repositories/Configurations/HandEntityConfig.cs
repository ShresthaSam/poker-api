using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerApi.Repositories.Entities;

namespace PokerApi.Repositories.Configurations
{
    public class HandEntityConfig : IEntityTypeConfiguration<Hand>
    {
        public void Configure(EntityTypeBuilder<Hand> builder)
        {
            builder.HasKey(p => p.HandId);
            builder.Property(b => b.HandId).HasColumnName("hand_id");
            builder.Property(b => b.CardId1).HasColumnName("card_id_1");
            builder.Property(b => b.CardId2).HasColumnName("card_id_2");
            builder.Property(b => b.CardId3).HasColumnName("card_id_3");
            builder.Property(b => b.CardId4).HasColumnName("card_id_4");
            builder.Property(b => b.CardId5).HasColumnName("card_id_5");
            builder.Property(b => b.PlayerUserId).HasColumnName("player_user_id");
            builder.ToTable("card_hand");
        }
    }
}
