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

        public virtual DbSet<FODMAPIngredient> FODMAPIngredients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
