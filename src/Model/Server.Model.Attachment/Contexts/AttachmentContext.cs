using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.Server.Model.Attachment.Abst.Interfaces;

namespace odec.Server.Model.Attachment.Contexts
{
    public class AttachmentContext:DbContext,IAttachmentContext<Attachment,AttachmentType>
    {
        private string AttachmentScheme = "attach";

        public AttachmentContext(DbContextOptions<AttachmentContext> options) : base(options) { }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AttachmentType> AttachmentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>().ToTable("Attachments", AttachmentScheme);
            modelBuilder.Entity<AttachmentType>().ToTable("AttachmentTypes", AttachmentScheme);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }


}
