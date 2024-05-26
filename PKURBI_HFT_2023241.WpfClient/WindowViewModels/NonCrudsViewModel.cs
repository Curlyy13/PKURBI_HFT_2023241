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
        public ICommand BasicInformationCommand { get; set; }
        public ICommand AvgPriceByCityCommand { get; set; }
        public ICommand MostRealEstatesCommand { get; set; }
        public ICommand TenantsByCityCommand { get; set; }


        //AvgPriceBySalesperson
        private int salesId;
        public int SalesId
        {
            get {return salesId;}
            set {SetProperty(ref salesId, value);}
        }
        private double? avgPrice;
        public double? AvgPrice
        {
            get { return avgPrice; }
            set { SetProperty(ref avgPrice, Math.Round((double)value, 2)); }
        }
        /////////////////////////////////////////

        //Basicinformation
        private int estateId;
        public int EstateId
        {
            get { return estateId; }
            set { SetProperty(ref estateId, value); }
        }
        private BasicInfo info;
        public BasicInfo Info
        {
            get { return info; }
            set { SetProperty(ref info, value); }
        }
        /////////////////////////////////////////


        //AvgPriceByCity
        private List<AvgPrices> prices;
        public List<AvgPrices> Prices
        {
            get { return prices; }
            set { SetProperty(ref prices, value); }
        }
        private bool lbox_visibility;
        public bool LboxVisibility
        {
            get { return lbox_visibility; }
            set { SetProperty(ref lbox_visibility, value); }
        }

        /////////////////////////////////////////


        //MostRealEstate
        private List<string> topsalesperson;
        public List<string> Topsalesperson
        {
            get { return topsalesperson; }
            set { SetProperty (ref topsalesperson, value); }
        }
        private bool lbox_visibility_topsales;
        public bool LboxVisibility_topsales
        {
            get { return lbox_visibility_topsales; }
            set { SetProperty(ref lbox_visibility_topsales, value); }
        }

        /////////////////////////////////////////
        //TenantsByCity
        private List<Tenants> tenants_city;
        public List<Tenants> Tenants_city
        {
            get { return tenants_city; }
            set { SetProperty(ref tenants_city, value); }
        }
        private bool lbox_visibility_tenants;
        public bool LboxVisibility_tenants
        {
            get { return lbox_visibility_tenants; }
            set { SetProperty(ref lbox_visibility_tenants, value); }
        }

        /////////////////////////////////////////
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
                //RealEstates = new RestCollection<RealEstate>("http://localhost:35487/", "RealEstate", "hub");
                //Salespeople = new RestCollection<Salesperson>("http://localhost:35487/", "Salesperson", "hub");
                //Tenants = new RestCollection<Tenant>("http://localhost:35487/", "Tenant", "hub");
                var rest = new RestService("http://localhost:35487/");
                AvgPriceBySalespersonCommand = new RelayCommand(() =>
                {
                    AvgPrice = 0;
                    AvgPrice = rest.Get<double?>(SalesId, "NCRealEstate/AvgPriceBySalespersonID");
                });
                BasicInformationCommand = new RelayCommand(() =>
                {
                    Info = rest.Get<BasicInfo>(EstateId, "NCRealEstate/BasicInformation");
                });
                AvgPriceByCityCommand = new RelayCommand(() =>
                {
                    Prices = rest.Get<AvgPrices>("NCRealEstate/AvgPriceByCity");
                    LboxVisibility = true;
                });
                MostRealEstatesCommand = new RelayCommand(() => {
                    Topsalesperson = rest.Get<string>("NCSalesperson/MostRealEstates");
                    LboxVisibility_topsales = true;
                });
                TenantsByCityCommand = new RelayCommand(() => {
                    Tenants_city = rest.Get<Tenants>("NCTenant/TenantsByCity");
                    LboxVisibility_tenants = true;
                });
            }
        }
    }
}
