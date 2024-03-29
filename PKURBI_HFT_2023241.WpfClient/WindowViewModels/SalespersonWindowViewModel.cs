using Microsoft.Toolkit.Mvvm.Input;
using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PKURBI_HFT_2023241.WpfClient.WindowViewModels
{
    class SalespersonWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Salesperson> Salespeople { get; set; }

        private Salesperson selectedSalesperson;

        public Salesperson SelectedSalesperson
        {
            get { return selectedSalesperson; }
            set
            {
                if (value != null)
                {
                    selectedSalesperson = new Salesperson()
                    {
                        SalesId = value.SalesId,
                        Name = value.Name,
                        Age = value.Age,
                    };
                    OnPropertyChanged();
                    (DeleteSalespersonCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateSalespersonCommand { get; set; }
        public ICommand DeleteSalespersonCommand { get; set; }
        public ICommand UpdateSalespersonCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public SalespersonWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Salespeople = new RestCollection<Salesperson>("http://localhost:35487/", "Salesperson", "hub");
                CreateSalespersonCommand = new RelayCommand(() =>
                {
                    Salespeople.Add(new Salesperson() { Name = SelectedSalesperson.Name, Age = SelectedSalesperson.Age });
                });
                UpdateSalespersonCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Salespeople.Update(SelectedSalesperson);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });
                DeleteSalespersonCommand = new RelayCommand(() =>
                {
                    Salespeople.Delete(SelectedSalesperson.SalesId);
                },
                () =>
                {
                    return SelectedSalesperson != null;
                });
                selectedSalesperson = new Salesperson();
            }
        }
    }
}
