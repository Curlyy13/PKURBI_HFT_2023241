using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Models
{
    public class Tenant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TenantId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(11)]

        public int Phone {  get; set; }

        public int PropId { get; set; }

        public ICollection<Property> Properties { get; set; }

        public Tenant()
        {
                Properties = new HashSet<Property>();
        }

        public Tenant(string line)
        {
            string[] split = line.Split('#');
            TenantId = int.Parse(split[0]);
            Name = split[1];
            Phone = int.Parse(split[2]);
            PropId = int.Parse(split[3]);
            Properties = new HashSet<Property>();
        }
    }
}
