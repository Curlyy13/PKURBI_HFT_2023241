using Microsoft.Toolkit.Mvvm.ComponentModel;
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
using System.Runtime.CompilerServices;

namespace PKURBI_HFT_2023241.WpfClient.WindowViewModels
{
    class TenantWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Tenant> Tenants { get; set; }

        private Tenant selectedTenant;

        public Tenant SelectedTenant
        {
            get { return selectedTenant; }
            set
            {
                if (value != null)
                {
                    selectedTenant = new Tenant()
                    {
                        TenantId = value.TenantId,
                        Name = value.Name,
                        Phone = value.Phone,
                    };
                    OnPropertyChanged();
                    (DeleteTenantCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateTenantCommand { get; set; }
        public ICommand DeleteTenantCommand { get; set; }
        public ICommand UpdateTenantCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public TenantWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Tenants = new RestCollection<Tenant>("http://localhost:35487/", "Tenant", "hub");
                CreateTenantCommand = new RelayCommand(() =>
                {
                    Tenants.Add(new Tenant() { Name = SelectedTenant.Name, Phone = SelectedTenant.Phone });
                });
                UpdateTenantCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Tenants.Update(SelectedTenant);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });
                DeleteTenantCommand = new RelayCommand(() =>
                {
                    Tenants.Delete(SelectedTenant.TenantId);
                },
                () =>
                {
                    return SelectedTenant != null;
                });
                selectedTenant = new Tenant();
            }
        }
    }
}
