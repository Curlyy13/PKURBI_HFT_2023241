using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PKURBI_HFT_2023241.WpfClient.WindowViewModels
{
    public class NonCrudsViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<RealEstate> RealEstates { get; set; }
        public RestCollection<Salesperson> Salespeople { get; set; }
        public RestCollection<Tenant> Tenants { get; set; }

        public ICommand AvgPriceBySalespersonCommand { get; set; }

        private double? avgPrice;
        public double? AvgPrice
        {
            get { return avgPrice; }
            set { SetProperty(ref avgPrice, Math.Round((double)value,2)); }
        }

        private int salesId;
        public int SalesId
        {
            get {return salesId;}
            set {SetProperty(ref salesId, value);}
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public NonCrudsViewModel()
        {
            if (!IsInDesignMode)
            {
                RealEstates = new RestCollection<RealEstate>("http://localhost:35487/", "RealEstate", "hub");
                Salespeople = new RestCollection<Salesperson>("http://localhost:35487/", "Salesperson", "hub");
                Tenants = new RestCollection<Tenant>("http://localhost:35487/", "Tenant", "hub");
                var rest = new RestService("http://localhost:35487/");
                AvgPriceBySalespersonCommand = new RelayCommand(() =>
                {
                    AvgPrice = 0;
                    AvgPrice = rest.Get<double?>(SalesId, "NCRealEstate/AvgPriceBySalespersonID");
                });
            }
        }
    }
}
