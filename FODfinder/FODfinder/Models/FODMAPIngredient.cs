namespace FODfinder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FODMAPIngredient
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Ingredient Name:")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Also listed as:")]
        public string Aliases { get; set; }
    }
}
