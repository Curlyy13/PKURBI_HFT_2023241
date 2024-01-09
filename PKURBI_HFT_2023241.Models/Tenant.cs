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

        public int Phone {  get; set; }

        [NotMapped]
        [JsonIgnore]
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
    //Non-Crud metódus miatt ki kell emelni a logic osztályból hogy használni tudja a ConsoleMenu
    public class Tenants
    {
        public int EstateCount { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            Tenants b = obj as Tenants;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Name == b.Name && this.EstateCount == b.EstateCount;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.EstateCount);
        }
    }
}
