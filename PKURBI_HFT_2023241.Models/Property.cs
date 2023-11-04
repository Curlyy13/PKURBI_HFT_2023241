using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PKURBI_HFT_2023241.Models
{
    [Table("Properties")]
    public class Property
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropId { get; set; }

        [Required]
        [StringLength(100)] 
        public string PropCity { get; set; }

        [Required]
        public double PropValue { get; set; } // $-ban
        [Required]
        public double BasicArea { get; set; } //m2-ben

        public int SalesId { get; set; }
        public virtual Salesperson Salesperson { get; set; }

        public int TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }

        public Property()
        {
            
        }

        public Property(string line)
        {
            string[] split = line.Split('#');
            PropId = int.Parse(split[0]);
            PropCity = split[1];
            PropValue = double.Parse(split[2]);
            BasicArea = double.Parse(split[3]);
            SalesId = int.Parse(split[4]);
            TenantId = int.Parse(split[5]);
        }
    }
}
