using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PKURBI_HFT_2023241.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<RealEstate> RealEstates { get; set; }

        private RealEstate selectedRealEstate;

        public RealEstate SelectedRealEstate
        {
            get { return selectedRealEstate; }
            set
            {
                if (value != null)
                {
                    selectedRealEstate = new RealEstate()
                    {
                        RealEstateCity = value.RealEstateCity,
                        RealEstateValue = value.RealEstateValue,
                        BasicArea = value.BasicArea,
                        RealEstateId = value.RealEstateId,
                    };
                    OnPropertyChanged();
                    (DeleteRealEstateCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateRealEstateCommand { get; set; }
        public ICommand DeleteRealEstateCommand { get; set; }
        public ICommand UpdateRealEstateCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                RealEstates = new RestCollection<RealEstate>("http://localhost:35487/", "RealEstate");
                CreateRealEstateCommand = new RelayCommand(() =>
                {
                    RealEstates.Add(new RealEstate() { RealEstateCity = SelectedRealEstate.RealEstateCity, BasicArea=SelectedRealEstate.BasicArea, RealEstateValue=SelectedRealEstate.RealEstateValue });
                });
                UpdateRealEstateCommand = new RelayCommand(() =>
                {
                    try
                    {
                        RealEstates.Update(SelectedRealEstate);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });
                DeleteRealEstateCommand = new RelayCommand(() =>
                {
                    RealEstates.Delete(SelectedRealEstate.RealEstateId);
                },
                () =>
                {
                    return SelectedRealEstate != null;
                });
                selectedRealEstate = new RealEstate();
            }
        }


    }
}
