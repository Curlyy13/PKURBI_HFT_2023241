using System;
using System.Collections;
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
    //Non-Crud metódus miatt ki kell emelni a logic osztályból hogy használni tudja a ConsoleMenu
    public class BasicInfo
    {
        public string Location { get; set; }
        public double Value { get; set; }
        public double Area { get; set; }
        public string Salesperson { get; set; }
        public string Tenant { get; set; }
        public int TenantContact { get; set; }

        public override bool Equals(object obj)
        {
            BasicInfo b = obj as BasicInfo;
            if (b == null) { return false; }
            else
            {
                return this.Location == b.Location && this.Value == b.Value && this.Area == b.Area && this.Salesperson == b.Salesperson && this.Tenant == b.Tenant && this.TenantContact == b.TenantContact;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Location, this.Value, this.Area, this.Salesperson, this.Tenant, this.TenantContact);
        }
    }
    //Non-Crud metódus miatt ki kell emelni a logic osztályból hogy használni tudja a ConsoleMenu
    public class AvgPrices
    {
        public string City { get; set; }
        public double AvgPrice { get; set; }

        public override bool Equals(object obj)
        {
            AvgPrices b = obj as AvgPrices;
            if (b == null) { return false; }
            else { return this.City == b.City && this.AvgPrice == b.AvgPrice; }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.City, this.AvgPrice);
        }
    }

}
