using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using shared.Model;

namespace Model
{
    public class PostContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
       
        

        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<User>().ToTable("Users");
       
        }
    }
}