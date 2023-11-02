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
        public string PropAdress { get; set; }

        [Required]
        public double PropValue { get; set; }
        [Required]
        public double BasicArea { get; set; }

        public int SalesId { get; set; }
        public virtual Salesperson Salesperson { get; set; }

        public int TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
