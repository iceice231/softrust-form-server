using FormServer.Entity;
using Microsoft.EntityFrameworkCore;

namespace FormServer;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<TopicEntity> Topics { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageEntity>()
            .HasOne(m => m.TopicEntity)
            .WithMany(t => t.Messages)
            .HasForeignKey(m => m.id_topic);

        modelBuilder.Entity<MessageEntity>()
            .HasOne(m => m.ContactEntity)
            .WithMany(t => t.Messages)
            .HasForeignKey(m => m.id_contact);
    }
}