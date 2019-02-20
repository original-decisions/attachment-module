using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.Server.Model.Attachment;
using odec.Server.Model.Attachment.Abst.Interfaces;
using odec.Server.Model.Attachment.Extended;
using odec.Server.Model.User;
using Attach =odec.Server.Model.Attachment.Attachment;
using Usr = odec.Server.Model.User.User;
namespace odec.Server.Model.Secure.Attachment.Contexts
{
    public class SecureAttachmentContext : DbContext,
        //IdentityDbContext<Usr, Role, int, UserClaim, UserRole, UserLogin, IdentityRoleClaim<int>, UserToken>, 
        ISecureAttachmentContext<Attach, AttachmentType, Extension, AttachmentTypeExtension, AttachmentTypePermission, Permission,AttachmentPermission>
    {
        private string MembershipScheme = "AspNet";
        private string AttachmentScheme = "attach";
        private string SecurityScheme = "security";
        
        public SecureAttachmentContext(DbContextOptions<SecureAttachmentContext> options)
            : base(options)
        {

        }

        public DbSet<Usr> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<AttachmentTypePermission> AttachmentTypePermissions { get; set; }
        public DbSet<Attach> Attachments { get; set; }
        public DbSet<AttachmentType> AttachmentTypes { get; set; }
        public DbSet<Extension> Extensions { get; set; }
        public DbSet<AttachmentTypeExtension> AttachmentTypeExtentions { get; set; }
        public DbSet<AttachmentPermission> AttachmentPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //  modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Usr>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<Role>().ToTable("Roles", MembershipScheme);
         //   modelBuilder.Entity<UserRole>().ToTable("UserRoles", MembershipScheme);
            //modelBuilder.Entity<UserClaim>().ToTable("UserClaims", MembershipScheme);
            //modelBuilder.Entity<UserLogin>().ToTable("UserLogins", MembershipScheme);
            //modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", MembershipScheme);
            //modelBuilder.Entity<UserToken>().ToTable("UserTokens", MembershipScheme);

            modelBuilder.Entity<Attach>().ToTable("Attachments", AttachmentScheme);
            modelBuilder.Entity<AttachmentType>().ToTable("AttachmentTypes", AttachmentScheme);
            modelBuilder.Entity<Extension>().ToTable("Extensions", AttachmentScheme);
            modelBuilder.Entity<AttachmentTypeExtension>()
                .ToTable("AttachmentTypeExtentions", AttachmentScheme)
                .HasKey(it=> new { it.AttachmentTypeId,it.ExtensionId});
            modelBuilder.Entity<AttachmentPermission>()
                .ToTable("AttachmentPermissions", SecurityScheme)
                .HasKey(it => new { it.AttachmentId, it.PermissionId, it.UserId});

            modelBuilder.Entity<AttachmentTypePermission>()
                .ToTable("AttachmentTypePermissions", SecurityScheme)
                .HasKey(it => new { it.AttachmentTypeId, it.PermissionId,it.RoleId });
            modelBuilder.Entity<Permission>().ToTable("Permissions", SecurityScheme);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
