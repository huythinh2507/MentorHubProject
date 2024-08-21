using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using YPP.MH.DataAccessLayer.Repositories;

#nullable enable

namespace YPP.MH.DataAccessLayer.Models
{
    public partial class MHDbContext : DbContext
    {
        
        public MHDbContext(DbContextOptions<MHDbContext> options)
         : base(options)
        {
        }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Space> Space { get; set; }
        public virtual DbSet<SpaceMember> SpaceMember { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WorkspaceMember> WorkspaceMember { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<SourceFilePath> SourceFilePath { get; set; }
        public virtual DbSet<WorkSpace> WorkSpace { get; set; }
        public virtual DbSet<Invitation> Invitation { get; set; }
        public virtual DbSet<Course> Course  { get; set; }
        public virtual DbSet<SpaceGroup> SpaceGroup { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SpaceMember>(entity =>
            {
                entity.HasKey(sm => new { sm.SpaceId, sm.MemberId });

                entity.HasOne(sm => sm.Space)
                    .WithMany(s => s.Members)
                    .HasForeignKey(sm => sm.SpaceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sm => sm.Member)
                    .WithMany(u => u.Spaces)
                    .HasForeignKey(sm => sm.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(p => p.Space)
                    .WithMany(s => s.Posts)
                    .HasForeignKey(p => p.SpaceId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.RememberMeToken)
                    .HasDefaultValue(string.Empty);

                entity.Property(u => u.ConfirmationToken)
                    .HasDefaultValue(string.Empty);
            });

            modelBuilder.Entity<Comment>()
               .HasOne(c => c.ParentComment)
               .WithMany(c => c.Replies)
               .HasForeignKey(c => c.ParentCommentId)
               .OnDelete(DeleteBehavior.Restrict); // or another delete behavior as needed
        }

        public async Task SaveUser(User user)
        {
            User.Add(user);

            await SaveChangesAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await User.FirstOrDefaultAsync(u => u.Id == userId);

            ArgumentNullException.ThrowIfNull(user);
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await User.FirstOrDefaultAsync(u => u.Email == email);

            ArgumentNullException.ThrowIfNull(user);
            return user;
        }
        public void UpdateUser(User user)
        {
            User.Update(user);
            SaveChangesAsync();
        }

        public async Task SavePost(Post post)
        {
            Post.Add(post);

            await SaveChangesAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await Post.FirstOrDefaultAsync(u => u.Id == postId);

            ArgumentNullException.ThrowIfNull(post);
            return post;
        }


    }
}
