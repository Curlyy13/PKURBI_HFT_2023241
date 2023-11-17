using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PKURBI_HFT_2023241.Models
{
    [Table("Properties")]
    public class RealEstate
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RealEstateId { get; set; }

        [Required]
        [StringLength(100)] 
        public string RealEstateCity { get; set; }

        [Required]
        public double RealEstateValue { get; set; } // $-ban
        [Required]
        public double BasicArea { get; set; } //m2-ben

        public int SalesId { get; set; }
        [NotMapped]
        public virtual Salesperson Salesperson { get; set; }

        public int TenantId { get; set; }
        [NotMapped]
        public virtual Tenant Tenant { get; set; }

        public RealEstate()
        {
            
        }

        public RealEstate(string line)
        {
            string[] split = line.Split('#');
            RealEstateId = int.Parse(split[0]);
            RealEstateCity = split[1];
            RealEstateValue = double.Parse(split[2]);
            BasicArea = double.Parse(split[3]);
            SalesId = int.Parse(split[4]);
            TenantId = int.Parse(split[5]);
        }
    }
}
