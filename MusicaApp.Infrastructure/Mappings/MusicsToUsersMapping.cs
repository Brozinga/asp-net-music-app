using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicApp.Domain.Models;

namespace MusicApp.Infrastructure.Mappings
{
    public class MusicsToUsersMapping : IEntityTypeConfiguration<MusicsToUsers>
    {
        public void Configure(EntityTypeBuilder<MusicsToUsers> builder)
        {
            builder.ToTable("MusicsToTables");

            builder.HasKey(x => new { x.MusicId, x.UserId });

            builder.HasOne(x => x.Music)
                .WithMany(x => x.MusicsToUsers)
                .HasForeignKey(x => x.MusicId)
                .HasConstraintName("Music_To_Users_Music_FK");

            builder.HasOne(x => x.User)
                .WithMany(x => x.MusicsToUsers)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("Music_To_Users_Users_FK");


        }
    }
}