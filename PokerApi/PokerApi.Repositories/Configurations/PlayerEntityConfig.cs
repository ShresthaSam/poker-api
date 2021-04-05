using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerApi.Repositories.Entities;

namespace PokerApi.Repositories.Configurations
{
    public class PlayerEntityConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.UserId).HasColumnName("player_user_id");
            builder.Property(p => p.UserName).HasColumnName("player_user_name");
            builder.ToTable("player");
        }
    }
}
