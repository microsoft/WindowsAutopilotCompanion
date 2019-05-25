using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CompanionApp.Model
{
    public class Device : INotifyPropertyChanged
    {
        // Static values
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AzureActiveDirectoryDeviceId { get; set; }
        public string AzureActiveDirectoryDeviceName { get; set; }
        public string DeploymentProfile { get; set; }
        public string ManagedDeviceId { get; set; }
        public string ManagedDeviceName { get; set; }
        public string ZtdId { get; set; }

        // Changeable through the UI
        public string GroupTag { get; set; }

        // Changeable through external methods (hence the need for OnPropertyChanged)
        public string localCategory;
        public string ManagedDeviceCategory
        {
            get
            {
                return localCategory;
            }
            set
            {
                SetProperty(ref localCategory, value);
            }
        }
        public string ManagedDeviceCategoryId { get; set; }

        private string localAUN;
        public string AddressibleUserName
        {
            get
            {
                return localAUN;
            }
            set
            {
                SetProperty(ref localAUN, value);
            }
        }

        private string localUPN;
        public string UserPrincipalName
        {
            get
            {
                return localUPN;
            }
            set
            {
                SetProperty(ref localUPN, value);
            }
        }

        #region Property change stuff
        protected bool SetProperty<T>(ref T backingStore, T value,
        [CallerMemberName]string propertyName = "",
        Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public IEnumerable<DeviceCategory> CategoryList
        { get; set; }
    }
}
