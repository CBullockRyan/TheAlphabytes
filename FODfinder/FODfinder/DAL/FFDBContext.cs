namespace FODfinder.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FFDBContext : DbContext
    {
        public FFDBContext()
            : base("name=FFDBContext")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<FODMAPIngredient> FODMAPIngredients { get; set; }
        public virtual DbSet<LabelledIngredient> LabelledIngredients { get; set; }
        public virtual DbSet<SavedFood> SavedFoods { get; set; }
        public virtual DbSet<UserIngredient> UserIngredients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.SavedFoods)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.userID);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.UserIngredients)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.userID);

            modelBuilder.Entity<LabelledIngredient>()
                .HasMany(e => e.UserIngredients)
                .WithRequired(e => e.LabelledIngredient)
                .WillCascadeOnDelete(false);
        }
    }
}
