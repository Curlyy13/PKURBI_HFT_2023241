using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PKURBI_HFT_2023241.Models
{
    [Table("Salespeople")]
    public class Salesperson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [NotMapped]
        public virtual ICollection<RealEstate> Realestates { get; set; }

        public Salesperson()
        {
            Realestates = new HashSet<RealEstate>();
        }

        public Salesperson(string line)
        {
            string[] split = line.Split('#');
            SalesId = int.Parse(split[0]);
            Name = split[1];
            Age = int.Parse(split[2]);
            Realestates = new HashSet<RealEstate>();
        }
    }
}
