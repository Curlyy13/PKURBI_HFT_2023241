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

namespace PKURBI_HFT_2023241.WpfClient.WindowViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ICommand OpenRealEstateWindowCommand { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                OpenRealEstateWindowCommand = new RelayCommand(() =>
                {
                    RealEstateWindow realEstateWindow = new RealEstateWindow();
                    realEstateWindow.ShowDialog();
                });
            }
        }
    }
}
