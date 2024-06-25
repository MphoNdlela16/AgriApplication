using System;
using System.ComponentModel.DataAnnotations;

namespace Agri_Energy_Connect.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        public int FarmerId { get; set; }
        public virtual Farmer Farmer { get; set; }
    }
}
