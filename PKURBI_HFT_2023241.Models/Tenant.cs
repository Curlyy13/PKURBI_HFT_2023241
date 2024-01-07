using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Models
{
    public class Tenant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TenantId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(11)]

        public int Phone {  get; set; }

        [NotMapped]
        public virtual ICollection<RealEstate> Realestates { get; set; }

        public Tenant()
        {
                Realestates = new HashSet<RealEstate>();
        }

        public Tenant(string line)
        {
            string[] split = line.Split('#');
            TenantId = int.Parse(split[0]);
            Name = split[1];
            Phone = int.Parse(split[2]);
            Realestates = new HashSet<RealEstate>();
        }
    }
}
