using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PKURBI_HFT_2023241.Models
{
    [Table("Properties")]
    public class Properties : Entity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PropId", TypeName = "int")]
        public override int Id { get; set; }

        [Required]
        [StringLength(150)] 
        public string PropAdress { get; set; }

        [Required]
        public double PropValue { get; set; }
        public double BasicAresa { get; set; }
    }
}
