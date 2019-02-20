using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.Server.Model.Attachment.Abst.Interfaces;
using odec.Server.Model.Attachment.Extended;

namespace odec.Server.Model.Attachment.Contexts
{
    public class AttachmentExtendedContext:DbContext,
        IAttachmentContextExt<Attachment,AttachmentType,Extension,AttachmentTypeExtension>
    {
        public AttachmentExtendedContext(DbContextOptions<AttachmentExtendedContext> options) : base(options)
        {
            
        }
        private string AttachmentScheme = "attach";
        public DbSet<Extension> Extensions { get; set; }
        public DbSet<AttachmentTypeExtension> AttachmentTypeExtentions { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AttachmentType> AttachmentTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>().ToTable("Attachments", AttachmentScheme);
            modelBuilder.Entity<AttachmentType>().ToTable("AttachmentTypes", AttachmentScheme);
            modelBuilder.Entity<Extension>().ToTable("Extensions", AttachmentScheme);
            modelBuilder.Entity<AttachmentTypeExtension>().ToTable("AttachmentTypeExtentions", AttachmentScheme)
                .HasKey(it => new { it.AttachmentTypeId, it.ExtensionId, });
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }
}
