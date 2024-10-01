using EntityFrameworkTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkTest.Data
{
    internal class SimpleDbContext : DbContext
    {
        public SimpleDbContext(DbContextOptions<SimpleDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Host=localhost;Port=5432;Database=Simple;User ID=germes;Password=q1w2e3r4Z;Pooling=true;").LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .HasMany(l => l.Members)
                .WithOne(k => k.Document)
                .HasForeignKey(k => k.DocumentId)
                .HasPrincipalKey(l => l.DocumentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Document>()
                .HasMany(l => l.Messages)
                .WithOne(k => k.Document)
                .HasForeignKey(l => l.DocumentId)
                .HasPrincipalKey(l => l.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message>()
                .HasMany(l => l.Members)
                .WithOne(k => k.Message)
                .HasForeignKey(l => l.MessageId)
                .HasPrincipalKey(l => l.MessageId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Message>()
                .ToTable("Message", l => l.ExcludeFromMigrations());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Member> Members { get; set; }
    }
}
