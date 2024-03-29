﻿using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reactivities.Domain.Models;

namespace Infrastructure
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityAttendee> ActivityAttendees { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<UserFollowing> UserFollowings { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<ActivityAttendee>(a => a.HasKey(x => new
            {
                x.UserId,
                x.ActivityId
            }));

            builder.Entity<ActivityAttendee>()
                   .HasOne(a => a.User)
                   .WithMany(a => a.Activities)
                   .HasForeignKey(a => a.UserId);

            builder.Entity<ActivityAttendee>()
                  .HasOne(a => a.Activity)
                  .WithMany(a => a.Attendees)
                  .HasForeignKey(a => a.ActivityId);

            builder.Entity<Comment>()
                   .HasOne(a => a.Activity)
                   .WithMany(a => a.Comments)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFollowing>(b => {
                b.HasKey(k => new { k.ObserverId, k.TargetId });

                b.HasOne(o => o.Observer)
                    .WithMany(f => f.Followings)
                    .HasForeignKey(o => o.ObserverId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                b.HasOne(o => o.Target)
                   .WithMany(f => f.Followers)
                   .HasForeignKey(o => o.TargetId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
