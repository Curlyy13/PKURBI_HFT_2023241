using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.WpfClient
{
    public class MainWindowViewModel
    {
        public RestCollection<RealEstate> RealEstates { get; set; }

        public MainWindowViewModel()
        {
           RealEstates = new RestCollection<RealEstate>("http://localhost:35487/", "RealEstate");
        }
    }
}
