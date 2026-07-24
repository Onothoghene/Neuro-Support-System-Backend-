using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<OrganizationUsers> OrganizationUsers { get; set; }
        public DbSet<OrganizationUsersInvite> OrganizationUsersInvite { get; set; }
        public DbSet<OrganizationRoles> OrganizationRoles { get; set; }
        public DbSet<OrganizationUserRoles> OrganizationUserRoles { get; set; }
        public DbSet<EmailChangeRequest> EmailChangeRequest { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<FileTemp> FileTemp { get; set; }
        public DbSet<Payment> Payment { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                string authUser;

                if (_authenticatedUser.UserId != null)
                {
                    authUser = _authenticatedUser.UserId.ToString();
                }
                else
                {
                    authUser = null;
                }

                if (entry.Entity.IsDeleted == true)
                {
                    entry.Entity.Deleted = _dateTime.NowUtc;
                    entry.Entity.DeletedBy = authUser;
                }
                else
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.Created = _dateTime.NowUtc;
                            entry.Entity.CreatedBy = authUser;
                            break;
                        case EntityState.Modified:
                            entry.Entity.LastModified = _dateTime.NowUtc;
                            entry.Entity.LastModifiedBy = authUser;
                            break;
                    }
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Universal filtering
            //builder.Entity<Stage>().HasQueryFilter(p => !p.IsDeleted);

            //builder.SeedAsync()

            //Fluent Navigations
            builder.Entity<UserProfile>(entity =>
            {
                //entity.HasIndex(e => e.AspUserId)
                //    .HasName("IX_User_AspNet")
                //    .IsUnique();

                //entity.Property(e => e.AspUserId)
                //    .IsRequired()
                //    .HasMaxLength(50);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Email);

            });

            //builder.Entity<Comments>(entity =>
            //{
            //    entity.HasOne(d => d.CreatedByNavigation)
            //     .WithMany()
            //     .HasForeignKey(d => d.CreatedBy)
            //     .HasConstraintName("FK_Comments_UserProfile");
            //});


            //builder.Entity<FileTemp>(entity =>
            //{
            //    entity.HasOne(d => d.MenuItem)
            //     .WithMany(r => r.Images)
            //     .HasForeignKey(d => d.MenuItemId)
            //     .HasConstraintName("FK_FileTemp_MenuItem")
            //     .OnDelete(DeleteBehavior.Cascade);
            //});

            //builder.Entity<Payment>(entity =>
            //{
            //    entity.HasOne(d => d.Order)
            //     .WithMany(p => p.Payments)
            //     .HasForeignKey(d => d.OrderId)
            //     .HasConstraintName("FK_Payment_Order");
            //});

            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            base.OnModelCreating(builder);
        }
    }
}
